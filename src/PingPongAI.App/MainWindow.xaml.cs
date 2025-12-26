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
        private const double PADDLE_SPEED = 250; // piksel/saniye

        private Random _rnd = new(Environment.TickCount);
        private DispatcherTimer? _gameTimer = null;

        private DateTime _lastUpdateTime;

        private Point _ptBall = default;
        private Point _ptVelocity = default;

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
            double radius = Ball.Width / 2;

            // Paddle bilgileri
            double paddleX = fromLeft
                ? Canvas.GetLeft(Paddle1)
                : Canvas.GetLeft(Paddle2);

            double paddleY = fromLeft
                ? _paddle1Y
                : _paddle2Y;

            double paddleHeight = Paddle1.Height;

            // Paddle üzerinde -1 .. +1 arası rastgele temas noktası
            double impactOffset = _rnd.NextDouble() * 2.0 - 1.0;

            // Topun Y konumu (merkez bazlı hesap)
            double paddleCenterY = paddleY + paddleHeight / 2;
            double ballCenterY = paddleCenterY + impactOffset * (paddleHeight / 2);

            // X konumu (paddle dışına yerleştir)
            _ptBall.X = fromLeft
                ? paddleX + Paddle1.Width + 1
                : paddleX - Ball.Width - 1;

            _ptBall.Y = ballCenterY - radius;

            // Hız yönleri
            double dirX = fromLeft ? 1 : -1;

            // Başlangıç hız büyüklüğü
            double speedX = GameCanvas.Width * SPEED_FACTOR;
            double speedY = GameCanvas.Height * SPEED_FACTOR * impactOffset;

            _ptVelocity = new(
                dirX * speedX,
                speedY
            );
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
                _ptBall.X + radius,
                _ptBall.Y + radius
            );

            _ptBall += new Vector(
                _ptVelocity.X * deltaTime,
                _ptVelocity.Y * deltaTime
             );

            Point nextCenter = new(
                _ptBall.X + radius,
                _ptBall.Y + radius
            );

            Vector movement = nextCenter - prevCenter;
            if (movement.LengthSquared < 0.000001)
                return;

            Ray ray = new()
            {
                Origin = prevCenter,
                Direction = movement
            };

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
            bool hitPaddle1 = false;

            if (hit1.Hit && hit2.Hit)
            {
                hit = hit1.Time < hit2.Time ? hit1 : hit2;
                hitPaddle1 = hit.Equals(hit1);
                hasHit = true;
            }
            else if (hit1.Hit)
            {
                hit = hit1;
                hitPaddle1 = true;
                hasHit = true;
            }
            else if (hit2.Hit)
            {
                hit = hit2;
                hitPaddle1 = false;
                hasHit = true;
            }

            if (hasHit)
            {
                Point contact = prevCenter + movement * hit.Time;
                contact += hit.Normal * 0.001;

                _ptBall.X = contact.X - radius;
                _ptBall.Y = contact.Y - radius;

                double paddleY = hitPaddle1 ? _paddle1Y : _paddle2Y;
                double paddleHeight = Paddle1.Height;

                double paddleCenterY = paddleY + paddleHeight / 2;
                double impactOffset = (contact.Y - paddleCenterY) / (paddleHeight / 2);
                impactOffset = Math.Max(-1.0, Math.Min(1.0, impactOffset));

                // Turn X completely in the opposite direction.
                _ptVelocity.X = Math.Abs(_ptVelocity.X) * (hitPaddle1 ? 1 : -1);

                // Adjust the Y speed according to the point of contact.
                _ptVelocity.Y += impactOffset * Math.Abs(_ptVelocity.X) * 0.75;
            }

            if (_ptBall.Y < 0)
            {
                _ptBall.Y = 0;
                _ptVelocity.Y *= -1;
            }
            else if (_ptBall.Y > maxY)
            {
                _ptBall.Y = maxY;
                _ptVelocity.Y *= -1;
            }

            if (_ptBall.X < 0)
            {
                _player2Score++;
                ResetBall(fromLeft: false);
            }
            else if (_ptBall.X > maxX)
            {
                _player1Score++;
                ResetBall(fromLeft: true);
            }
        }


        private void UpdatePaddles(double deltaTime)
        {
            Debug.WriteLine(deltaTime);
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
            Canvas.SetLeft(Ball, _ptBall.X);
            Canvas.SetTop(Ball, _ptBall.Y);
        }

        private void RenderPaddles()
        {
            Canvas.SetTop(Paddle1, _paddle1Y);
            Canvas.SetTop(Paddle2, _paddle2Y);
        }
    }
}