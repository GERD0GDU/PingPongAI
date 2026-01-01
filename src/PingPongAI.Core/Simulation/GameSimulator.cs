using PingPongAI.Core.Math;
using PingPongAI.Core.Physics;
using PingPongAI.Core.States;
using System;

namespace PingPongAI.Core.Simulation
{
    public class GameSimulator
    {
        private Random _rnd = new Random(Environment.TickCount);
        private GameState _gameState = null;

        //public GameState State => (GameState)_gameState.Clone(); // I'm not sure right now!
        public GameState State => _gameState;

        public void Initialize(GameState gameState)
        {
            _gameState = gameState;
            Reset();
        }

        public void Reset()
        {
            if (_gameState == null)
                throw new ArgumentNullException("_gameState");

            _gameState.LeftPaddle.Y = (_gameState.Bounds.Height - _gameState.LeftPaddle.Height) / 2;
            _gameState.RightPaddle.Y = (_gameState.Bounds.Height - _gameState.RightPaddle.Height) / 2;

            _gameState.LeftScore = 0;
            _gameState.RightScore = 0;

            // select a random player.
            ResetBall(_rnd.Next(2) == 0 ? PaddleSide.Left : PaddleSide.Right);
        }

        public void ResetBall(PaddleSide fromType)
        {
            if (_gameState == null)
                throw new ArgumentNullException("_gameState");

            bool fromLeft = fromType == PaddleSide.Left;
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
            double speedX = Consts.BALL_SPEED;
            double speedY = Consts.BALL_SPEED * impactOffset;
            speedY = System.Math.Sign(speedY)
                    * System.Math.Max(Consts.BALL_SPEED_MIN_Y, System.Math.Abs(speedY));

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

            double maxX = _gameState.Bounds.Width - ball.Width;
            double maxY = _gameState.Bounds.Height - ball.Height;

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

            Ray2 ray = new Ray2
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
            bool hitPaddle = false;

            if (hit1.Hit && hit2.Hit)
                hit = hit1.Time < hit2.Time ? hit1 : hit2;
            else if (hit1.Hit)
                hit = hit1;
            else if (hit2.Hit)
                hit = hit2;

            paddle1.HasHitBall = hit.Equals(hit1) && hit1.Hit;
            paddle2.HasHitBall = hit.Equals(hit2) && hit2.Hit;
            hitPaddle = paddle1.HasHitBall || paddle2.HasHitBall;

            if (hitPaddle)
            {
                Vec2 contact = prevCenter + movement * hit.Time;

                // Paddle'dan kesin ayır
                if (paddle1.HasHitBall)
                    ball.X = paddle1.Right + 0.01;
                else
                    ball.X = paddle2.X - ball.Width - 0.01;

                double paddleY = paddle1.HasHitBall ? paddle1Rect.Y : paddle2Rect.Y;
                double paddleHeight = paddle1Rect.Height;
                double paddleCenterY = paddleY + paddleHeight * 0.5;

                double impactOffset = (contact.Y - paddleCenterY) / (paddleHeight * 0.5);
                impactOffset = MathEx.Clamp(impactOffset, -1.0, 1.0);

                double speed = ball.Velocity.Length;
                double angle = impactOffset * Consts.MAX_BOUNCE_ANGLE;

                double dirX = paddle1.HasHitBall ? System.Math.Cos(angle) : -System.Math.Cos(angle);
                double dirY = System.Math.Sin(angle);

                ball.Velocity = new Vec2(dirX, dirY) * speed;

                // Y hızını güvenli aralıkta tut
                ball.Velocity = new Vec2(
                    ball.Velocity.X,
                    MathEx.Clamp(
                        ball.Velocity.Y,
                        -Consts.BALL_SPEED_MAX_Y,
                        Consts.BALL_SPEED_MAX_Y
                    )
                );

                return; // Aynı frame duvar çarpmasını engelle
            }

            // Duvar çarpışmaları
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

            // Skor
            if (ball.X < 0)
            {
                _gameState.RightScore++;
                ResetBall(fromType: PaddleSide.Right);
            }
            else if (ball.X > maxX)
            {
                _gameState.LeftScore++;
                ResetBall(fromType: PaddleSide.Left);
            }
        }

        private void UpdatePaddles(double deltaTime)
        {
            PaddleState paddle1 = _gameState.LeftPaddle;
            PaddleState paddle2 = _gameState.RightPaddle;

            paddle1.Velocity = paddle1.Direction * Consts.PADDLE_SPEED;
            paddle2.Velocity = paddle2.Direction * Consts.PADDLE_SPEED;

            paddle1.Y += paddle1.Velocity * deltaTime;
            paddle2.Y += paddle2.Velocity * deltaTime;

            // To avoid exceeding the canvas boundaries.
            paddle1.Y = MathEx.Clamp(paddle1.Y, 0, _gameState.Bounds.Height - paddle1.Height);
            paddle2.Y = MathEx.Clamp(paddle2.Y, 0, _gameState.Bounds.Height - paddle2.Height);
        }

        public void Update(double deltaTime)
        {
            if (_gameState == null)
                throw new ArgumentNullException("_gameState");

            UpdateBall(deltaTime);
            UpdatePaddles(deltaTime);
        }

        public void UpdatePaddleDirection(PaddleSide paddleSide, Direction dir)
        {
            if (_gameState == null)
                throw new ArgumentNullException("_gameState");

            PaddleState paddle = paddleSide == PaddleSide.Left
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
