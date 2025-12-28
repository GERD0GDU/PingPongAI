using PingPongAI.Core.Math;
using PingPongAI.Core.Physics;
using PingPongAI.Core.States;
using System;

namespace PingPongAI.Core.Simulation
{
    public class GameSimulator
    {
        private const double SPEED_FACTOR = 0.3;
        private const double PADDLE_SPEED = 250; // piksel/saniye

        private Random _rnd = new Random(Environment.TickCount);
        private GameState _gameState = null;

        public IGameState State => _gameState;

        public void Initialize(GameState gameState)
        {
            _gameState = gameState;

            // select a random player.
            ResetBall(_rnd.Next(2) == 0 ? PaddleTypes.Left : PaddleTypes.Right);
        }

        public void ResetBall(PaddleTypes fromType)
        {
            if (_gameState == null)
                throw new ArgumentNullException("_gameState");

            bool fromLeft = fromType == PaddleTypes.Left;
            // active paddle
            PaddleState paddle = fromLeft
                ? _gameState.LeftPaddle
                : _gameState.RightPaddle;

            // Random contact point on the paddle between -1 and +1.
            double impactOffset = _rnd.NextDouble() * 2.0 - 1.0;

            // Topun Y konumu (merkez bazlı hesap)
            double paddleCenterY = paddle.Y + paddle.Height / 2;
            double ballCenterY = paddleCenterY + impactOffset * (paddle.Height / 2);

            // X position (place outside the paddle)
            _gameState.Ball.Position = new Vec2(
                fromLeft ? paddle.Right + 1 : paddle.X - _gameState.Ball.Width - 1,
                ballCenterY - _gameState.Ball.Radius
            );

            // Speed ​​directions
            double dirX = fromLeft ? 1 : -1;

            // Initial velocity magnitude
            double speedX = _gameState.GameArea.Width * SPEED_FACTOR;
            double speedY = _gameState.GameArea.Height * SPEED_FACTOR * impactOffset;

            _gameState.Ball.Velocity = new Vec2(
                dirX * speedX,
                speedY
            );
        }

        private void UpdateBall(double deltaTime)
        {
            BallState ball = _gameState.Ball;
            PaddleState paddle1 = _gameState.LeftPaddle;
            PaddleState paddle2 = _gameState.RightPaddle;
            double radius = ball.Radius;

            double maxX = _gameState.GameArea.Width - ball.Width;
            double maxY = _gameState.GameArea.Height - ball.Height;

            Vec2 prevCenter = new Vec2(
                ball.X + radius,
                ball.Y + radius
            );

            ball.Position += new Vec2(
                ball.Velocity.X * deltaTime,
                ball.Velocity.Y * deltaTime
            );

            Vec2 nextCenter = new Vec2(
                ball.X + radius,
                ball.Y + radius
            );

            Vec2 movement = nextCenter - prevCenter;
            if (movement.LengthSquared < 0.000001)
                return;

            Ray2 ray = new Ray2()
            {
                Origin = prevCenter,
                Direction = movement
            };

            Rect2 paddle1Rect = new Rect2(
                paddle1.Right - radius,
                paddle1.Y - radius,
                radius * 2,
                paddle1.Height + radius * 2
            );

            Rect2 paddle2Rect = new Rect2(
                paddle2.X - radius,
                paddle2.Y - radius,
                radius * 2,
                paddle2.Height + radius * 2
            );

            HitInfo hit1 = Collision.RayVsRect(ray, paddle1Rect);
            HitInfo hit2 = Collision.RayVsRect(ray, paddle2Rect);

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
                Vec2 contact = prevCenter + movement * hit.Time;
                contact += hit.Normal * 0.001;

                ball.Position = new Vec2(
                    contact.X - radius,
                    contact.Y - radius
                );

                double paddleY = hitPaddle1 ? paddle1Rect.Y : paddle2Rect.Y;
                double paddleHeight = paddle1Rect.Height;

                double paddleCenterY = paddleY + paddleHeight / 2;
                double impactOffset = (contact.Y - paddleCenterY) / (paddleHeight / 2);
                impactOffset = System.Math.Max(-1.0, System.Math.Min(1.0, impactOffset));

                // Turn X completely in the opposite direction.
                ball.Velocity.X = System.Math.Abs(ball.Velocity.X) * (hitPaddle1 ? 1 : -1);

                // Adjust the Y speed according to the point of contact.
                ball.Velocity.Y += impactOffset * System.Math.Abs(ball.Velocity.X) * 0.75;
            }

            if (ball.Y < 0)
            {
                ball.Y = 0;
                ball.Velocity.Y *= -1;
            }
            else if (ball.Y > maxY)
            {
                ball.Y = maxY;
                ball.Velocity.Y *= -1;
            }

            if (ball.X < 0)
            {
                _gameState._rightScore++;
                ResetBall(fromType: PaddleTypes.Right);
            }
            else if (ball.X > maxX)
            {
                _gameState._leftScore++;
                ResetBall(fromType: PaddleTypes.Left);
            }
        }

        private void UpdatePaddles(double deltaTime)
        {
            PaddleState paddle1 = _gameState.LeftPaddle;
            PaddleState paddle2 = _gameState.RightPaddle;

            paddle1.Y += paddle1.Direction * PADDLE_SPEED * deltaTime;
            paddle2.Y += paddle2.Direction * PADDLE_SPEED * deltaTime;

            // To avoid exceeding the canvas boundaries.
            paddle1.Y = System.Math.Max(0, System.Math.Min(_gameState.GameArea.Height - paddle1.Height, paddle1.Y));
            paddle2.Y = System.Math.Max(0, System.Math.Min(_gameState.GameArea.Height - paddle2.Height, paddle2.Y));
        }

        public void Update(double deltaTime)
        {
            if (_gameState == null)
                throw new ArgumentNullException("_gameState");

            UpdateBall(deltaTime);
            UpdatePaddles(deltaTime);
        }

        public void UpdatePaddleDirection(PaddleTypes type, Direction dir)
        {
            if (_gameState == null)
                throw new ArgumentNullException("_gameState");

            PaddleState paddle = type == PaddleTypes.Left
                ? _gameState.LeftPaddle
                : _gameState.RightPaddle;
            paddle.Direction = dir == Direction.Up
                ? -1
                : dir == Direction.Down
                    ? 1
                    : 0;
        }
    }
}
