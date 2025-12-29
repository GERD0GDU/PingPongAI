using PingPongAI.Core.Math;
using System;

namespace PingPongAI.Core.States
{
    public sealed class GameState : ICloneable
    {
        public Rect2 GameArea = new Rect2();
        public BallState Ball { get; internal set; } = new BallState();
        public PaddleState LeftPaddle { get; internal set; } = new PaddleState();
        public PaddleState RightPaddle { get; internal set; } = new PaddleState();
        public int LeftScore { get; internal set; }
        public int RightScore { get; internal set; }

        // ICloneable implements
        public object Clone()
        {
            return new GameState()
            {
                GameArea = GameArea,
                Ball = (BallState)Ball.Clone(),
                LeftPaddle = (PaddleState)LeftPaddle.Clone(),
                RightPaddle = (PaddleState)RightPaddle.Clone(),
                LeftScore = LeftScore,
                RightScore = RightScore
            };
        }
    }
}
