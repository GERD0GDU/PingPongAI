using PingPongAI.Core.States;

namespace PingPongAI.AI
{
    public interface IPongAgent
    {
        AgentTypes AgentType { get; }
        PaddleSide Side { get; }
        void SetInput(Direction dir);
        Direction Decide(GameState state);
    }
}
