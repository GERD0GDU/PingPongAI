using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PingPongAI.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const double SPEED_FACTOR = 0.3;

        private Random _rnd = new(Environment.TickCount);
        private DispatcherTimer? _gameTimer = null;

        private double _ballX = 0;
        private double _ballY = 0;

        private double _velocityX = 0;
        private double _velocityY = 0;

        private DateTime _lastUpdateTime;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int directionX = _rnd.Next(0, 2) == 0 ? -1 : 1;
            int directionY = _rnd.Next(0, 2) == 0 ? -1 : 1;

            _velocityX = directionX * GameCanvas.ActualWidth * SPEED_FACTOR;
            _velocityY = directionY * GameCanvas.ActualHeight * SPEED_FACTOR;

            _ballX = (GameCanvas.ActualWidth - Ball.Width) / 2;
            _ballY = _rnd.NextDouble() * (GameCanvas.ActualHeight - Ball.Height);

            _gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16) // ~60 FPS
            };

            _gameTimer.Tick += GameLoop;
            _lastUpdateTime = DateTime.Now;

            _gameTimer.Start();
        }

        private void GameLoop(object? sender, EventArgs e)
        {
            var now = DateTime.Now;
            var deltaTime = (now - _lastUpdateTime).TotalSeconds;
            _lastUpdateTime = now;

            UpdateBall(deltaTime);
            Render();
        }

        private void UpdateBall(double deltaTime)
        {
            _ballX += _velocityX * deltaTime;
            _ballY += _velocityY * deltaTime;

            double maxX = GameCanvas.ActualWidth - Ball.Width;
            double maxY = GameCanvas.ActualHeight - Ball.Height;

            // Left wall
            if (_ballX < 0)
            {
                _ballX = 0;
                _velocityX *= -1;
            }
            // Right wall
            else if (_ballX > maxX)
            {
                _ballX = maxX;
                _velocityX *= -1;
            }

            // Top wall
            if (_ballY < 0)
            {
                _ballY = 0;
                _velocityY *= -1;
            }
            // Bottom wall
            else if (_ballY > maxY)
            {
                _ballY = maxY;
                _velocityY *= -1;
            }
        }

        private void Render()
        {
            Canvas.SetLeft(Ball, _ballX);
            Canvas.SetTop(Ball, _ballY);
        }
    }
}