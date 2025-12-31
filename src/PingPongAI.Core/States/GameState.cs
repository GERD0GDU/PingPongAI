using PingPongAI.Core.Math;
using System;
using System.Diagnostics;

namespace PingPongAI.Core.States
{
    [DebuggerDisplay(@"\{GameArea={GameArea} Ball={Ball} LeftPaddle={LeftPaddle} ...\}")]
    public sealed class GameState : ICloneable
    {
        public Rect2 GameArea = new Rect2();
        public BallState Ball { get; internal set; } = new BallState();
        public PaddleState LeftPaddle { get; internal set; } = new PaddleState() { Side = PaddleSide.Left };
        public PaddleState RightPaddle { get; internal set; } = new PaddleState() { Side = PaddleSide.Right };
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
