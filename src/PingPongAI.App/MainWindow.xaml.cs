using PingPongAI.Core;
using System.Diagnostics;
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

        private int _player1Score = 0;
        private int _player2Score = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResetGame();

            _gameTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1000 / 60.0) // ~60 FPS
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

        private void ResetGame()
        {
            _player1Score = 0;
            _player2Score = 0;

            // first position for paddles
            _paddle1Y = (GameCanvas.Height - Paddle1.Height) / 2;
            _paddle2Y = (GameCanvas.Height - Paddle2.Height) / 2;

            // select a random player.
            ResetBall(_rnd.Next(2) == 0 ? true : false);
        }

        private void ResetBall(bool fromLeft)
        {
            _ballX = fromLeft
                ? Canvas.GetLeft(Paddle1) + Paddle1.Width + 1
                : Canvas.GetLeft(Paddle2) - Ball.Width - 1;

            _ballY = fromLeft
                ? Canvas.GetTop(Paddle1) + (Paddle1.Height - Ball.Height) / 2
                : Canvas.GetTop(Paddle2) + (Paddle2.Height - Ball.Height) / 2;

            // Yeni rastgele yön
            int directionX = fromLeft ? 1 : -1;
            int directionY = _rnd.Next(0, 2) == 0 ? -1 : 1;

            _velocityX = directionX * GameCanvas.Width * SPEED_FACTOR;
            _velocityY = directionY * GameCanvas.Height * SPEED_FACTOR;
        }

        private void GameLoop(object? sender, EventArgs e)
        {
            var now = DateTime.Now;
            var deltaTime = (now - _lastUpdateTime).TotalSeconds;
            _lastUpdateTime = now;

            UpdatePaddles(deltaTime);
            UpdateBall(deltaTime);
            UpdateScores();

            RenderPaddles();
            RenderBall();
        }

        private void UpdateBall(double deltaTime)
        {
            double radius = Ball.Width / 2;

            double maxX = GameCanvas.Width - Ball.Width;
            double maxY = GameCanvas.Height - Ball.Height;

            Point prevCenter = new(
                _ballX + radius,
                _ballY + radius
            );

            _ballX += _velocityX * deltaTime;
            _ballY += _velocityY * deltaTime;

            Point nextCenter = new(
                _ballX +  radius,
                _ballY +  radius
            );

            Ray ray = new()
            {
                Origin = prevCenter,
                Direction = nextCenter - prevCenter
            };

            if (ray.Direction.LengthSquared < 0.000001)
                return;

            Rect paddle1Rect = new(
                Canvas.GetLeft(Paddle1) + Paddle1.Width - radius,
                _paddle1Y - radius,
                radius * 2,
                Paddle1.Height + radius * 2
            );

            Rect paddle2Rect = new(
                Canvas.GetLeft(Paddle2) - radius,
                _paddle2Y - radius,
                radius * 2,
                Paddle2.Height + radius * 2
            );

            HitInfo hit1 = Utils.RayVsRect(ray, paddle1Rect);
            HitInfo hit2 = Utils.RayVsRect(ray, paddle2Rect);
            HitInfo hit = default;
            bool hasHit = false;

            if (hit1.Hit && hit2.Hit)
            {
                hit = hit1.Time < hit2.Time ? hit1 : hit2;
                hasHit = true;
            }
            else if (hit1.Hit)
            {
                hit = hit1;
                hasHit = true;
            }
            else if (hit2.Hit)
            {
                hit = hit2;
                hasHit = true;
            }

            if (hasHit)
            {
                Point contact = prevCenter + ray.Direction * hit.Time;
                contact += hit.Normal * 0.001;

                _ballX = contact.X - radius;
                _ballY = contact.Y - radius;

                if (hit.Normal.X != 0)
                    _velocityX *= -1;
                if (hit.Normal.Y != 0)
                    _velocityY *= -1;
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
                _player2Score++;
            }
            else if (_ballX > maxX) // It surpassed Paddle2.
            {
                ResetBall(fromLeft: true);
                _player1Score++;
            }
        }

        private void UpdatePaddles(double deltaTime)
        {
            _paddle1Y += _paddle1Direction * PADDLE_SPEED * deltaTime;
            _paddle2Y += _paddle2Direction * PADDLE_SPEED * deltaTime;

            // To avoid exceeding the canvas boundaries.
            _paddle1Y = Math.Max(0, Math.Min(GameCanvas.Height - Paddle1.Height, _paddle1Y));
            _paddle2Y = Math.Max(0, Math.Min(GameCanvas.Height - Paddle2.Height, _paddle2Y));
        }

        private void UpdateScores()
        {
            txtPlayer1Score.Text = _player1Score.ToString();
            txtPlayer2Score.Text = _player2Score.ToString();
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