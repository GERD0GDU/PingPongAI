using PingPongAI.AI;
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
        private MainViewModel _vm;
        private DispatcherTimer? _gameTimer = null;

        private DateTime _lastUpdateTime;
        private GameSimulator _gameSimulator = new();
        private IPongAgent? _leftPlayer = null;
        private IPongAgent? _rightPlayer = null;

        public MainWindow()
        {
            InitializeComponent();

            _vm = new MainViewModel();
            DataContext = _vm;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GameState state = new();
            state.GameArea.Width = GameCanvas.Width;
            state.GameArea.Height = GameCanvas.Height;
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
            {
                _leftPlayer?.SetInput(Direction.Up);
                //txtPlayer1UpKey.Foreground = Brushes.White;
                //_gameSimulator.UpdatePaddleDirection(PaddleSide.Left, Direction.Up);
            }
            else if (e.Key == Key.S)
            {
                _leftPlayer?.SetInput(Direction.Down);
                //txtPlayer1DownKey.Foreground = Brushes.White;
                //_gameSimulator.UpdatePaddleDirection(PaddleSide.Left, Direction.Down);
            }

            if (e.Key == Key.Up)
            {
                _rightPlayer?.SetInput(Direction.Up);
                //txtPlayer2UpKey.Foreground = Brushes.White;
                //_gameSimulator.UpdatePaddleDirection(PaddleSide.Right, Direction.Up);
            }
            else if (e.Key == Key.Down)
            {
                _rightPlayer?.SetInput(Direction.Down);
                //txtPlayer2DownKey.Foreground = Brushes.White;
                //_gameSimulator.UpdatePaddleDirection(PaddleSide.Right, Direction.Down);
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W || e.Key == Key.S)
            {
                _leftPlayer?.SetInput(Direction.None);
                //txtPlayer1UpKey.Foreground = Brushes.Gray;
                //txtPlayer1DownKey.Foreground = Brushes.Gray;
                //_gameSimulator.UpdatePaddleDirection(PaddleSide.Left, Direction.None);
            }                

            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                _rightPlayer?.SetInput(Direction.None);
                //txtPlayer2UpKey.Foreground = Brushes.Gray;
                //txtPlayer2DownKey.Foreground = Brushes.Gray;
                //_gameSimulator.UpdatePaddleDirection(PaddleSide.Right, Direction.None);
            }
        }

        private void btnStartStop_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (!_vm.IsRunning)
            {
                _vm.IsRunning = true;

                _gameSimulator.Reset();

                _leftPlayer = AgentFactory.CreateAgent(_vm.LeftAgentType, PaddleSide.Left);
                _rightPlayer = AgentFactory.CreateAgent(_vm.RightAgentType, PaddleSide.Right);

                _lastUpdateTime = DateTime.Now;
                _gameTimer?.Start();
            }
            else
            {
                _vm.IsRunning = false;
                _gameTimer?.Stop();

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

            Decide();

            _gameSimulator.Update(deltaTime);

            Render();
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

        
    }
}