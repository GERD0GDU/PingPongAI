using PingPongAI.AI.Agents;
using PingPongAI.AI.Factory;
using PingPongAI.AI.Neural;
using PingPongAI.App.Helpers;
using PingPongAI.Core.Simulation;
using PingPongAI.Core.States;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace PingPongAI.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PowerManager? _powerManager = null;

        private MainViewModel _vm;
        private DispatcherTimer? _gameTimer = null;

        private DateTime _lastUpdateTime;
        private GameSimulator _gameSimulator = new();
        private IPongAgent? _leftPlayer = null;
        private IPongAgent? _rightPlayer = null;

        private readonly string _weightsDir;

        public MainWindow()
        {
            InitializeComponent();

            // Field olarak sakla, pencere açık olduğu sürece aktif kalsın
            _powerManager = new();

            _vm = new MainViewModel();
            DataContext = _vm;

            _weightsDir = ResolveWeightsDir();
        }

        protected override void OnClosed(EventArgs e)
        {
            _gameTimer?.Stop();
            SaveRLWeights();

            _powerManager?.Dispose();
            _powerManager = null;
            base.OnClosed(e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GameState state = new();
            state.Bounds.Width = GameCanvas.Width;
            state.Bounds.Height = GameCanvas.Height;
            state.Ball.Width = Ball.Width;
            state.Ball.Height = Ball.Height;
            state.LeftPaddle.Bounds = new(
                Canvas.GetLeft(Paddle1),
                Canvas.GetTop(Paddle1),
                Paddle1.Width,
                Paddle1.Height
            );
            state.RightPaddle.Bounds = new(
                Canvas.GetLeft(Paddle2),
                Canvas.GetTop(Paddle2),
                Paddle2.Width,
                Paddle2.Height
            );
            _gameSimulator.Initialize(state);

            _gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000 / 60.0) // ~60 FPS
            };

            _gameTimer.Tick += GameLoop;
            Render();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
                _leftPlayer?.SetInput(Direction.Up);
            else if (e.Key == Key.S)
                _leftPlayer?.SetInput(Direction.Down);

            if (e.Key == Key.Up)
                _rightPlayer?.SetInput(Direction.Up);
            else if (e.Key == Key.Down)
                _rightPlayer?.SetInput(Direction.Down);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W || e.Key == Key.S)
                _leftPlayer?.SetInput(Direction.None);  

            if (e.Key == Key.Up || e.Key == Key.Down)
                _rightPlayer?.SetInput(Direction.None);
        }

        private void btnStartStop_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (!_vm.IsRunning)
            {
                _vm.IsRunning = true;

                _gameSimulator.Reset();

                _leftPlayer = CreatePlayer(_vm.LeftAgentType, PaddleSide.Left);
                _rightPlayer = CreatePlayer(_vm.RightAgentType, PaddleSide.Right);

                _lastUpdateTime = DateTime.Now;
                _gameTimer?.Start();
            }
            else
            {
                _vm.IsRunning = false;
                _gameTimer?.Stop();

                SaveRLWeights();

                _leftPlayer = null;
                _rightPlayer = null;

                _gameSimulator.Reset();
                Render();
            }
        }

        private void Decide()
        {
            Direction leftDirection = _leftPlayer!.Decide(_gameSimulator.State);
            switch (leftDirection)
            {
                case Direction.Up:
                    txtPlayer1UpKey.Foreground = Brushes.White;
                    _gameSimulator.UpdatePaddleDirection(PaddleSide.Left, Direction.Up);
                    break;
                case Direction.Down:
                    txtPlayer1DownKey.Foreground = Brushes.White;
                    _gameSimulator.UpdatePaddleDirection(PaddleSide.Left, Direction.Down);
                    break;
                default:
                    txtPlayer1UpKey.Foreground = Brushes.Gray;
                    txtPlayer1DownKey.Foreground = Brushes.Gray;
                    _gameSimulator.UpdatePaddleDirection(PaddleSide.Left, Direction.None);
                    break;
            }

            Direction rightDirection = _rightPlayer!.Decide(_gameSimulator.State);
            switch (rightDirection)
            {
                case Direction.Up:
                    txtPlayer2UpKey.Foreground = Brushes.White;
                    _gameSimulator.UpdatePaddleDirection(PaddleSide.Right, Direction.Up);
                    break;
                case Direction.Down:
                    txtPlayer2DownKey.Foreground = Brushes.White;
                    _gameSimulator.UpdatePaddleDirection(PaddleSide.Right, Direction.Down);
                    break;
                default:
                    txtPlayer2UpKey.Foreground = Brushes.Gray;
                    txtPlayer2DownKey.Foreground = Brushes.Gray;
                    _gameSimulator.UpdatePaddleDirection(PaddleSide.Right, Direction.None);
                    break;
            }
        }

        private void GameLoop(object? sender, EventArgs e)
        {
            var now = DateTime.Now;
            var deltaTime = (now - _lastUpdateTime).TotalSeconds;
            _lastUpdateTime = now;

            GameState previous = (GameState)_gameSimulator.State.Clone();

            Decide();

            _gameSimulator.Update(deltaTime);

            Render();

            ResultPair target = TargetCalculator.Calculate(previous, _gameSimulator.State);

            UpdateAI(_leftPlayer!, previous, target.Left);
            UpdateAI(_rightPlayer!, previous, target.Right);

            UpdateRL(_leftPlayer!, previous, _gameSimulator.State);
            UpdateRL(_rightPlayer!, previous, _gameSimulator.State);
        }

        private void Render()
        {
            GameState state = _gameSimulator.State;

            txtPlayer1Score.Text = state.LeftScore.ToString();
            txtPlayer2Score.Text = state.RightScore.ToString();

            Canvas.SetLeft(Ball, state.Ball.X);
            Canvas.SetTop(Ball, state.Ball.Y);

            Canvas.SetTop(Paddle1, state.LeftPaddle.Y);
            Canvas.SetTop(Paddle2, state.RightPaddle.Y);
        }

        private void UpdateAI(IPongAgent agent, GameState observedState, double expected)
        {
            if (agent.AgentType != AgentTypes.AI_Supervised || !_vm.IsTrainingEnabled)
                return;

            AISupervisedAgent ai = (AISupervisedAgent)agent;

            // Train on the same state the decision was made on,
            // not the post-update state.
            double[] inputs = ai.EncodeState(observedState);

            ai.Train(inputs, [expected], learningRate: 0.01);
        }

        private void UpdateRL(IPongAgent agent, GameState previousState, GameState currentState)
        {
            if (agent.AgentType != AgentTypes.AI_Reinforcement)
                return;

            AIReinforcementAgent rl = (AIReinforcementAgent)agent;
            rl.IsTrainingEnabled = _vm.IsTrainingEnabled;

            PaddleState paddle = rl.Side == PaddleSide.Left
                ? currentState.LeftPaddle
                : currentState.RightPaddle;

            // Step reward: +1 every frame the paddle hit the ball.
            if (paddle.HasHitBall)
                rl.RegisterReward(+1.0);

            // Ralli end: score changed between the snapshot and the current state.
            bool leftScored = currentState.LeftScore > previousState.LeftScore;
            bool rightScored = currentState.RightScore > previousState.RightScore;

            if (leftScored || rightScored)
            {
                bool agentLost = (rl.Side == PaddleSide.Left && rightScored)
                              || (rl.Side == PaddleSide.Right && leftScored);

                // Terminal reward: -1 to the side that missed, 0 to the winner
                // (the winner already accumulated +1 hit rewards during the ralli).
                double terminal = agentLost ? -1.0 : 0.0;
                rl.EndEpisode(terminal);
            }
        }

        private IPongAgent CreatePlayer(AgentTypes type, PaddleSide side)
        {
            // For RL agents, try loading a previously saved network so the
            // weights persist across runs.
            if (type == AgentTypes.AI_Reinforcement)
            {
                string path = GetWeightsPath(side);
                if (File.Exists(path))
                {
                    try
                    {
                        NeuralNetwork loaded = NetworkPersistence.Load(path);
                        return new AIReinforcementAgent(side, loaded);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            $"Failed to load RL weights from '{path}':\n{ex.Message}\n\n" +
                            "Falling back to a fresh network.",
                            "PingPongAI",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                }
            }

            return AgentFactory.CreateAgent(type, side);
        }

        private void SaveRLWeights()
        {
            // Skip saving if training was disabled — otherwise an idle session
            // with random initial weights would overwrite a good checkpoint.
            if (!_vm.IsTrainingEnabled)
                return;

            TrySaveOne(_leftPlayer, PaddleSide.Left);
            TrySaveOne(_rightPlayer, PaddleSide.Right);
        }

        private void TrySaveOne(IPongAgent? agent, PaddleSide side)
        {
            if (agent is AIReinforcementAgent rl)
            {
                try
                {
                    rl.SaveWeights(GetWeightsPath(side));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(
                        $"Failed to save RL weights ({side}): {ex.Message}");
                }
            }
        }

        private string GetWeightsPath(PaddleSide side)
        {
            string fileName = side == PaddleSide.Left ? "left.json" : "right.json";
            return Path.Combine(_weightsDir, fileName);
        }

        // Walks up from the executable directory looking for a `weights/`
        // folder. When running from Visual Studio's bin output, this finds
        // the repo-level folder so trained networks land where the user can
        // commit them. Falls back to `<exe>/weights/` otherwise.
        private static string ResolveWeightsDir()
        {
            string? current = AppDomain.CurrentDomain.BaseDirectory;
            for (int i = 0; i < 8 && current != null; i++)
            {
                string candidate = Path.Combine(current, "weights");
                if (Directory.Exists(candidate))
                    return candidate;
                current = Directory.GetParent(current)?.FullName;
            }
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "weights");
        }
    }
}