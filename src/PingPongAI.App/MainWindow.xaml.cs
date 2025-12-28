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
        private DispatcherTimer? _gameTimer = null;

        private DateTime _lastUpdateTime;
        private GameSimulator _gameSimulator = new();

        public MainWindow()
        {
            InitializeComponent();
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
            _lastUpdateTime = DateTime.Now;

            _gameTimer.Start();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                txtPlayer1UpKey.Foreground = Brushes.White;
                _gameSimulator.UpdatePaddleDirection(PaddleTypes.Left, Direction.Up);
            }
            else if (e.Key == Key.S)
            {
                txtPlayer1DownKey.Foreground = Brushes.White;
                _gameSimulator.UpdatePaddleDirection(PaddleTypes.Left, Direction.Down);
            }

            if (e.Key == Key.Up)
            {
                txtPlayer2UpKey.Foreground = Brushes.White;
                _gameSimulator.UpdatePaddleDirection(PaddleTypes.Right, Direction.Up);
            }
            else if (e.Key == Key.Down)
            {
                txtPlayer2DownKey.Foreground = Brushes.White;
                _gameSimulator.UpdatePaddleDirection(PaddleTypes.Right, Direction.Down);
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W || e.Key == Key.S)
            {
                txtPlayer1UpKey.Foreground = Brushes.Gray;
                txtPlayer1DownKey.Foreground = Brushes.Gray;
                _gameSimulator.UpdatePaddleDirection(PaddleTypes.Left, Direction.None);
            }                

            if (e.Key == Key.Up || e.Key == Key.Down)
            {
                txtPlayer2UpKey.Foreground = Brushes.Gray;
                txtPlayer2DownKey.Foreground = Brushes.Gray;
                _gameSimulator.UpdatePaddleDirection(PaddleTypes.Right, Direction.None);
            }
        }

        private void GameLoop(object? sender, EventArgs e)
        {
            var now = DateTime.Now;
            var deltaTime = (now - _lastUpdateTime).TotalSeconds;
            _lastUpdateTime = now;

            _gameSimulator.Update(deltaTime);

            Render();
        }

        private void Render()
        {
            IGameState state = _gameSimulator.State;

            txtPlayer1Score.Text = state.LeftScore.ToString();
            txtPlayer2Score.Text = state.RightScore.ToString();

            Canvas.SetLeft(Ball, state.BallX);
            Canvas.SetTop(Ball, state.BallY);

            Canvas.SetTop(Paddle1, state.LeftPaddleY);
            Canvas.SetTop(Paddle2, state.RightPaddleY);
        }
    }
}