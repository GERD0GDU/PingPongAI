# Rule-Based Agent Approach

This document has been prepared to explain the purpose, scope, and architectural role of the **RuleBasedAgent** structure developed within the PingPongAI project.

The rule-based approach is deliberately preferred to validate the playability of the game, its deterministic behavior, and the correctness of the agent abstraction before introducing a learning AI system.

## What Is a Rule-Based Agent?

A RuleBasedAgent is a player agent that makes decisions using **predefined rules** based on observations received from the environment.

This agent:

- Does not learn
- Does not include model training
- Does not contain randomness (except for controlled cases)
- Produces the same outputs for the same input conditions

Thanks to these characteristics, it serves as an ideal reference agent for validating the game engine and agent interfaces.

## Why Start with a Rule-Based Agent?

Before developing an AI or reinforcement learning based agent, the following questions must be clearly answered:

- Is the game environment deterministic?
- Is the agent independent from the game engine?
- Can player behavior be abstracted?
- Do human and AI players use the same interface?

The RuleBasedAgent was developed to answer these questions.

With this approach:

- The game engine and agent layer are decoupled
- There is no architectural difference between human and AI players
- A non-learning but decision-making reference agent is established

## PingPongAI.AI Library

All AI-related agents are developed under a separate library named **PingPongAI.AI**.

This library:

- Targets `netstandard2.0`
- Contains no UI or WPF dependencies
- Communicates with the game engine only through defined interfaces

This structure allows the same AI library to be reused in different UI technologies or simulation environments in the future.

## Existing Agent Types

Currently, the following agent classes are defined:

- **HumanAgent**
  - Controlled via keyboard or user input
  - Represents direct player behavior

- **RuleBasedAgent**
  - Produces paddle movement based on the ball’s position and velocity
  - Uses predefined decision rules
  - Does not include learning or adaptation

- **AIAgent**
  - Reserved for learning agents
  - Only a skeleton structure exists in this version
  - Its content is deliberately left empty

## Application-Side Integration

In the PingPongAI.App WPF application:

- Selection objects have been added to allow choosing player roles
- Start - stop game control has been implemented
- Different agent types can be assigned to each paddle

As a result, the following scenarios are supported:

- Human - Human
- Human - RuleBasedAgent
- RuleBasedAgent - RuleBasedAgent

The HumanAgent <--> RuleBasedAgent matchup has been tested and verified to work correctly.

## Limitations of the Rule-Based Agent

The RuleBasedAgent is deliberately kept limited.

This agent:

- Does not learn
- Does not store error history
- Does not include reward - penalty mechanisms
- Does not develop long-term strategies

These limitations are necessary to clearly compare the behavior of learning agents that will be developed later.

## Conclusion

The rule-based approach in the PingPongAI project is not an intermediate step, but a **deliberate architectural validation layer**.

Thanks to this structure:

- The game engine has been made reliable
- The agent abstraction has been validated
- A solid foundation for AI integration has been established

![Rule-Based Screen](./assets/rule-based-screen.png)

You can access the code corresponding to the changes and updates up to this point via the [v1.1-rule-based](https://github.com/GERD0GDU/PingPongAI/tree/v1.1-rule-based) tag.

[The next step](./09-AIAgent-SupervisedControl.md) is to populate the `AIAgent` class and introduce learning behaviors into the system in a controlled manner.

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
- *Rule-Based Agent Approach*
- &gt; [AIAgent - Supervised Control Approach](./09-AIAgent-SupervisedControl.md)
