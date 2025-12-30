using PingPongAI.AI.Factory;
using PingPongAI.Core.States;

namespace PingPongAI.AI.Agents
{
    public sealed class HumanAgent : PongAgent, IPongAgent
    {
        private Direction _current = Direction.None;

        public HumanAgent(PaddleSide side)
            : base(side)
        {
        }

        public override AgentTypes AgentType => AgentTypes.Human;

        public override void SetInput(Direction dir)
        {
            _current = dir;
        }

        public override Direction Decide(GameState state)
        {
            return _current;
        }
    }
}
