# Why This Architecture?

The architecture used in this project is not accidental.

But at the same time:

- it is not an academic reference architecture
- it is not optimized at an industrial scale
- it does not claim to be the "best way"

This architecture was chosen to **make learning visible**.

## Inspiration: The Demis Hassabis Approach

In the conceptual background of this project lies the following idea from Demis Hassabis, one of the founders of DeepMind:

> Instead of writing complex rules to solve complex problems\
build systems that can learn

In this approach:

- the system does not know what to do
- rules are kept to a minimum
- the learning process is placed at the center

This is why the ping-pong game was chosen.

## Why Self-Play?

Self-play means:

> The system plays with itself - and learns

Thanks to this:

- no external definition of "good behavior" is given
- human strategies are not copied
- the only reference is success - failure

When an AI plays against another AI:

- it sees its mistakes repeatedly
- it pushes its own limits
- it develops unexpected but effective behaviors

This is the purest form of learning.

## Why a Modular Structure?

The following parts are deliberately separated in the project:

- game engine
- physics calculations
- AI agent
- learning mechanism
- visualization layer

The reason is this:

> If learning code and game code get mixed together\
you cannot understand what happened and why

Thanks to the modular structure:

- the game runs without AI
- the AI can be tested without the game
- each part can be understood on its own

This project is not a "product" but an **exploration laboratory**.

## Why No Ready-Made Libraries?

In this project:

- there is no TensorFlow
- there is no PyTorch
- there is no ready-made RL framework

Because the goal is:

- not to get results
- but to understand the process

Ready-made libraries:

- are very powerful
- but they hide the learning process

Here, every weight change is deliberately made visible.

## What Does the Architecture Provide?

Thanks to this architecture:

- how the AI makes decisions can be observed
- the source of errors can be understood
- the question "why did it behave like this here?" can be asked

A system in which these questions can be asked:

> is truly open to learning

## What Does This Architecture Not Do?

This architecture:

- 🔥 does not think like a human
- 🧠 does not produce intuition (but exhibits intuitive behaviors)
- ✨ does not perform magic

The only thing it does is this:

> Experience - feedback - adjustment

But when these three come together, a very powerful structure emerges.

## Conclusion

This architecture:

- looks simple
- learns slowly
- is modest

But precisely because of this:

- it is educational
- it is transparent
- it is reliable

The PingPongAI architecture exists not to glorify artificial intelligence, but to **make it understandable**.

In the next document, the rationale behind the [**PingPongAI.App**](./06-PingPongAI.App.md) application developed as a WPF desktop will be discussed.

## See Also

- [Home](./README.md)
- [What AI is and is not, its relation to code](./00-WhatIsAI.md)
- [The concept of learning, supervised / unsupervised / reinforcement](./01-WhatIsLearning.md)
- [Artificial neuron, input/weight/bias, simple example](./02-Neuron.md)
- [Mini neural network, hidden layer, feedforward network](./03-NeuralNetwork.md)
- [Reward and punishment, self-play, basic RL logic](./04-ReinforcementLearning.md)
- *Hassabis approach, self-play, modular architecture*
- &gt; [PingPongAI.App Rationale](./06-PingPongAI.App.md)
- [PingPongAI.App Game Rules](./07-PingPongAI.App.Rules.md)
- [Rule-Based Agent Approach](./08-RuleBased.md)
