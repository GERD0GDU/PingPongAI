# AIAgent - Supervised Control Approach

This document has been prepared to explain the learning approach of the **`AIAgent`** class, which is the first artificial intelligence agent used in the `PingPongAI` project, and the deliberate boundaries of this approach.

At this stage, the applied method is not reinforcement learning in the classical sense. Instead, a controlled and observable learning model is preferred, where the **expected behavior is defined externally**.

The goal is to make the fundamentals of AI behavior understandable and to provide a solid foundation for more complex learning methods.

## What Is AIAgent?

`AIAgent` is an agent class derived from the `IPongAgent` interface.

This agent:

- Observes the game state
- Produces one of the decisions for the paddle: up - down - idle
- **Does not evaluate whether this decision is correct**

The correct behavior is calculated outside the agent and communicated to it.

Therefore, AIAgent:

- Is not a self-learning structure
- Optimizes its behavior according to the expectation provided to it
- Differs from reinforcement learning in that it tries to produce an externally calculated expected output instead of receiving reward or punishment signals based on its own output

## Calculation of the Expected Behavior

The expected paddle movement is calculated by the `PingPongAI.Core.Simulation.TargetCalculator` class.

During this calculation, the following game state information is used:

- The horizontal direction of the ball relative to the paddle (approaching or moving away)
- The normalized vertical (Y) position of the ball relative to the paddle

The output of `TargetCalculator` represents the following:

> What should have been the correct behavior for the paddle in this game state?

This value serves as a **teacher signal** for AIAgent.

```csharp
using PingPongAI.Core.Math;
using PingPongAI.Core.States;

namespace PingPongAI.Core.Simulation
{
    public static class TargetCalculator
    {
        public static ResultPair Calculate(
            GameState previous,
            GameState current)
        {
            double leftReward = ComputeExpectedForPaddle(current, current.LeftPaddle);
            double rightReward = ComputeExpectedForPaddle(current, current.RightPaddle);

            return new ResultPair(leftReward, rightReward);
        }

        private static double ComputeExpectedForPaddle(GameState state, PaddleState paddle)
        {
            if (paddle.Side == PaddleSide.Left && state.Ball.Velocity.X > 0)
                return 0.0;
            else if (paddle.Side == PaddleSide.Right && state.Ball.Velocity.X < 0)
                return 0.0;

            double expected = 0.0;
            double relativeY = (state.Ball.CenterY - paddle.CenterY) / (paddle.Height / 2);
            relativeY = MathEx.Clamp(relativeY, -1.0, 1.0);

            expected += relativeY;

            return expected;
        }
    }
}
```

## Learning Process (Train Mechanism)

AIAgent is trained using the `Train(expected)` method.

In this process:

- The agent produces its own decision
- The externally calculated `expected` value is provided to the agent
- The difference between the produced output and the expected output is calculated
- The agent updates its internal weights according to this difference

Thanks to this approach:

- The learning process is deterministic
- The reason why a behavior changes can be observed
- Errors can be analyzed easily

This structure provides a control model similar to supervised learning.

## What This Approach Is Not

The method applied at this stage:

- Is not reinforcement learning
- Does not include a reward - punishment mechanism
- Does not evaluate the consequences of the agent's own actions
- Does not develop long-term strategies

AIAgent:

- Does not aim to win
- Does not care about the outcome
- Only tries to learn the behavior communicated to it

These limitations are intentionally chosen.

## Why Is the Architecture Kept Separate?

This learning approach is **intentionally separated** from the reinforcement learning agent to be added in the future.

Planned structure:

- Existing AIAgent - supervised control approach
- A new AIAgent derivative - reinforcement learning approach

The new agent:

- Can use the same `IPongAgent` interface
- Can be identified with a different `AgentType`
- Learns by receiving reward - punishment signals based on its own actions

This topic is within the scope of the next document.

## Application-Side Integration

The PingPongAI.App WPF application currently supports three different player types:

- HumanAgent
- RuleBasedAgent
- AIAgent

These agents:

- Can be matched freely regardless of left or right paddle
- Are treated equally by the game engine

The **Enable AI Training** checkbox in the UI:

- Controls the learning process for both `AIAgent` instances
- Enables or disables training

In future steps:

- Separate training control checkboxes
- For left and right AI agents
are planned.

## Code Versioning and Reference

The `AIAgent` learning model described in this document corresponds to a specific code state.

Therefore:

- The current code will be fixed with a GitHub tag
- The documentation and the code will match one-to-one
- Future changes will not break this stage

This approach allows the learning process to be tracked both theoretically and practically.

## Conclusion

The `AIAgent` developed at this stage:

- Helps to understand the basic mechanics of learning
- Makes AI behavior transparent and controllable
- Provides a solid reference point for transitioning to reinforcement learning

This structure is designed not to make artificial intelligence mysterious, but to **make it understandable**.

You can access the code corresponding to the changes and updates described up to this point via the [v1.2-supervised-control](https://github.com/GERD0GDU/PingPongAI/tree/v1.2-supervised-control) tag.

In the next document, the agent architecture that learns from the consequences of its own actions and uses a reward - punishment mechanism will be discussed.

## See Also

- [Home Page](./README.md)
- [What AI is and is not, its relation to code](./00-WhatIsAI.md)
- [The concept of learning, supervised / unsupervised / reinforcement](./01-WhatIsLearning.md)
- [Artificial neuron, input/weight/bias, simple example](./02-Neuron.md)
- [Mini neural network, hidden layer, feedforward network](./03-NeuralNetwork.md)
- [Reward and punishment, self-play, basic RL logic](./04-ReinforcementLearning.md)
- [Hassabis approach, self-play, modular architecture](./05-WhyThisArchitecture.md)
- [PingPongAI.App Rationale](./06-PingPongAI.App.md)
- [PingPongAI.App Game Rules](./07-PingPongAI.App.Rules.md)
- [Rule-Based Agent Approach](./09-AIAgent-SupervisedControl.md)
- *AIAgent - Supervised Control Approach*
