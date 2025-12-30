using PingPongAI.AI.Factory;
using PingPongAI.Core.States;

namespace PingPongAI.AI.Agents
{
    public sealed class AIAgent : PongAgent, IPongAgent
    {
        public AIAgent(PaddleSide side)
            : base(side)
        {
        }

        public override AgentTypes AgentType => AgentTypes.AI;

        public override Direction Decide(GameState state)
        {
            throw new System.NotImplementedException();
        }
    }
}
