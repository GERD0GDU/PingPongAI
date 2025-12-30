using PingPongAI.AI.Factory;
using PingPongAI.Core.States;

namespace PingPongAI.AI.Agents
{
    public abstract class PongAgent : IPongAgent
    {
        private PaddleSide _side;

        public PongAgent(PaddleSide side)
        {
            _side = side;
        }

        public abstract AgentTypes AgentType {  get; }

        public PaddleSide Side => _side;

        public virtual void SetInput(Direction dir)
        {
            // default: ignore input
        }

        public abstract Direction Decide(GameState state);
    }
}
