using PingPongAI.Core.States;

namespace PingPongAI.AI
{
    public sealed class RuleBasedAgent : PongAgent, IPongAgent
    {
        public RuleBasedAgent(PaddleSide side)
            : base(side)
        {
        }

        public override AgentTypes AgentType => AgentTypes.RuleBased;

        public override Direction Decide(GameState state)
        {
            switch (Side)
            {
                case PaddleSide.Left:
                    if (state.Ball.Velocity.X >= 0)
                    {
                        return Direction.None;
                    }
                    break;
                default:
                    if (state.Ball.Velocity.X <= 0)
                    {
                        return Direction.None;
                    }
                    break;
            }

            PaddleState paddle = Side == PaddleSide.Left
                ? state.LeftPaddle
                : state.RightPaddle;

            double paddleCenterY = paddle.Y + paddle.Height / 2;
            double ballCenterY = state.Ball.CenterY;

            if (ballCenterY > paddleCenterY + state.Ball.Height)
                return Direction.Down;

            if (ballCenterY < paddleCenterY - state.Ball.Height)
                return Direction.Up;

            return Direction.None;
        }
    }
}
