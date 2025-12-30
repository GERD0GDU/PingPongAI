using PingPongAI.AI.Factory;
using PingPongAI.Core.States;

namespace PingPongAI.AI.Agents
{
    public interface IPongAgent
    {
        AgentTypes AgentType { get; }
        PaddleSide Side { get; }
        void SetInput(Direction dir);
        Direction Decide(GameState state);
    }
}
