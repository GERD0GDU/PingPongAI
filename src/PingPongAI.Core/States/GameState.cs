using PingPongAI.Core.Math;

namespace PingPongAI.Core.States
{
    public sealed class GameState : IGameState
    {
        internal int _leftScore;
        internal int _rightScore;

        public Rect2 GameArea = new Rect2();
        public BallState Ball { get; } = new BallState();
        public PaddleState LeftPaddle { get; } = new PaddleState();
        public PaddleState RightPaddle { get; } = new PaddleState();

        public double Width => GameArea.Width;
        public double Height => GameArea.Height;
        public double BallX => Ball.X;
        public double BallY => Ball.Y;
        public double BallCenterX => Ball.CenterX;
        public double BallCenterY => Ball.CenterY;
        public double LeftPaddleY => LeftPaddle.Y;
        public double RightPaddleY => RightPaddle.Y;
        public double LeftScore => _leftScore;
        public double RightScore => _rightScore;
    }
}
