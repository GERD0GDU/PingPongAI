namespace PingPongAI.Core.States
{
    public interface IGameState
    {
        double Width { get; }
        double Height { get; }

        double BallX { get; }
        double BallY { get; }

        double BallCenterX { get; }
        double BallCenterY { get; }

        double LeftPaddleY { get; }
        double RightPaddleY { get; }

        double LeftScore { get; }
        double RightScore { get; }
    }
}
