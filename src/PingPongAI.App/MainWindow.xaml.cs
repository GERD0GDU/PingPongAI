using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace PingPongAI.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const double SPEED_FACTOR = 0.3;
        private const double PADDLE_SPEED = 300; // piksel/saniye

        private Random _rnd = new(Environment.TickCount);
        private DispatcherTimer? _gameTimer = null;

        private double _ballX = 0;
        private double _ballY = 0;

        private double _velocityX = 0;
        private double _velocityY = 0;

        private DateTime _lastUpdateTime;

        private int _paddle1Direction = 0;
        private int _paddle2Direction = 0;

        private double _paddle1Y = 0;
        private double _paddle2Y = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // first position for paddles
            _paddle1Y = _paddle2Y = (GameCanvas.ActualHeight - Paddle1.ActualHeight) / 2;
            // select a random player.
            ResetBall(_rnd.Next(2) == 0 ? true : false);

            _gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16) // ~60 FPS
            };

            _gameTimer.Tick += GameLoop;
            _lastUpdateTime = DateTime.Now;

            _gameTimer.Start();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.W)
                _paddle1Direction = -1;
            else if (e.Key == Key.S)
                _paddle1Direction = 1;

            if (e.Key == Key.Up)
                _paddle2Direction = -1;
            else if (e.Key == Key.Down)
                _paddle2Direction = 1;
        }

        private void Window_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.W || e.Key == Key.S)
                _paddle1Direction = 0;

            if (e.Key == Key.Up || e.Key == Key.Down)
                _paddle2Direction = 0;
        }

        private void ResetBall(bool fromLeft)
        {
            _ballX = fromLeft
                ? Canvas.GetLeft(Paddle1) + Paddle1.Width + 1
                : Canvas.GetLeft(Paddle2) - Ball.Width - 1;

            _ballY = fromLeft
                ? Canvas.GetTop(Paddle1) + (Paddle1.ActualHeight - Ball.ActualHeight) / 2
                : Canvas.GetTop(Paddle2) + (Paddle2.ActualHeight - Ball.ActualHeight) / 2;

            // Yeni rastgele yön
            int directionX = fromLeft ? 1 : -1;
            int directionY = _rnd.Next(0, 2) == 0 ? -1 : 1;

            _velocityX = directionX * GameCanvas.ActualWidth * SPEED_FACTOR;
            _velocityY = directionY * GameCanvas.ActualHeight * SPEED_FACTOR;
        }

        private void GameLoop(object? sender, EventArgs e)
        {
            var now = DateTime.Now;
            var deltaTime = (now - _lastUpdateTime).TotalSeconds;
            _lastUpdateTime = now;

            UpdateBall(deltaTime);
            RenderBall();

            UpdatePaddles(deltaTime);
            RenderPaddles();
        }

        private void UpdateBall(double deltaTime)
        {
            _ballX += _velocityX * deltaTime;
            _ballY += _velocityY * deltaTime;

            double maxX = GameCanvas.ActualWidth - Ball.Width;
            double maxY = GameCanvas.ActualHeight - Ball.Height;

            // If the ball collides with paddle1
            if (_ballX <= Canvas.GetLeft(Paddle1) + Paddle1.Width &&
                _ballX >= Canvas.GetLeft(Paddle1) &&
                _ballY + Ball.Height >= _paddle1Y &&
                _ballY <= _paddle1Y + Paddle1.Height)
            {
                _ballX = Canvas.GetLeft(Paddle1) + Paddle1.Width;
                _velocityX *= -1;
            }

            // If the ball collides with paddle2
            else if (_ballX + Ball.Width >= Canvas.GetLeft(Paddle2) &&
                     _ballX + Ball.Width <= Canvas.GetLeft(Paddle2) + Paddle2.Width &&
                     _ballY + Ball.Height >= _paddle2Y &&
                     _ballY <= _paddle2Y + Paddle2.Height)
            {
                _ballX = Canvas.GetLeft(Paddle2) - Ball.Width;
                _velocityX *= -1;
            }

            // Is Top hitting the canvas limits?
            if (_ballY < 0)
            {
                _ballY = 0;
                _velocityY *= -1;
            }
            else if (_ballY > maxY)
            {
                _ballY = maxY;
                _velocityY *= -1;
            }

            // If the ball has passed the paddles (score)
            if (_ballX < 0) // It surpassed Paddle1.
            {
                ResetBall(fromLeft: false);
            }
            else if (_ballX > maxX) // It surpassed Paddle2.
            {
                ResetBall(fromLeft: true);
            }
        }


        private void UpdatePaddles(double deltaTime)
        {
            _paddle1Y += _paddle1Direction * PADDLE_SPEED * deltaTime;
            _paddle2Y += _paddle2Direction * PADDLE_SPEED * deltaTime;

            // To avoid exceeding the canvas boundaries.
            _paddle1Y = Math.Max(0, Math.Min(GameCanvas.ActualHeight - Paddle1.Height, _paddle1Y));
            _paddle2Y = Math.Max(0, Math.Min(GameCanvas.ActualHeight - Paddle2.Height, _paddle2Y));
        }

        private void RenderBall()
        {
            Canvas.SetLeft(Ball, _ballX);
            Canvas.SetTop(Ball, _ballY);
        }

        private void RenderPaddles()
        {
            Canvas.SetTop(Paddle1, _paddle1Y);
            Canvas.SetTop(Paddle2, _paddle2Y);
        }
    }
}