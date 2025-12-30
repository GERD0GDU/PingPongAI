using PingPongAI.Core.States;
using PingPongAI.AI.Agents;
using System;

namespace PingPongAI.AI.Factory
{
    public sealed class AgentFactory
    {
        public static IPongAgent CreateAgent(AgentTypes agentType, PaddleSide side)
        {
            switch (agentType)
            {
                case AgentTypes.Human:
                    return new HumanAgent(side);
                case AgentTypes.RuleBased:
                    return new RuleBasedAgent(side);
                case AgentTypes.AI:
                    return new AIAgent(side);
                default:
                    throw new ArgumentOutOfRangeException(nameof(agentType), agentType, null);
            }
        }
    }
}
