# Reinforcement Learning (Reinforcement Learning)

This document explains **how a neural network learns**.

The learning here:

- is not supervised
- no correct answer is given
- no "do this" instruction exists

Instead, there is:

- trial
- outcome
- feedback

## Where Does the Learning Problem Begin?

A neural network:

- receives input
- produces output

But it does not know:

- is this output good?
- bad?
- did it work?

For learning to begin, the network must get an answer to this question:

> Was what I did correct?

This answer is given through a **reward - punishment** mechanism.

## What Is Reinforcement Learning?

Reinforcement Learning (RL):

- is an agent
- inside an environment
- taking actions
- and learning from the outcomes

The key concepts here are:

- agent
- environment
- action
- reward

## Agent

The agent:

- is the decision-making side
- in our project, it is the neural network

The agent:

- observes the world
- selects an action
- waits for the result

The agent:

- does not know what to do at the beginning
- learns over time

## Environment

The environment:

- is the world in which the agent exists

For ping-pong, the environment:

- game field
- ball
- paddle
- physical rules

The environment:

- provides state information to the agent
- reacts to the agent's action

## Action

An action:

- is the movement chosen by the agent

Example actions for ping-pong:

- move the paddle up
- move the paddle down
- do nothing

The agent:

- selects an action at every step
- bears the consequence of that choice

## Reward - Punishment (Reward)

This is the heart of learning.

Reward:

- is a numerical feedback
- the mathematical form of saying "well done"

Example:

- you hit the ball -> +1
- you missed the ball -> -1
- unnecessary movement -> small negative reward

Important point:

- reward is not moral
- it has no meaning
- it is just a number

## How Does Learning Occur?

The process is cyclical:

1. the agent observes the state
2. selects an action
3. the environment responds
4. reward is calculated
5. the network slightly adjusts itself

This loop:

- thousands
- millions
- sometimes billions of times

repeats.

Learning:

- does not happen in a single step
- requires patience

## The Concept of Self-Play

Self-play:

- is the agent playing against itself

Here:

- there is no teacher
- no examples
- no imitation

The agent:

- learns from its own mistakes
- reinforces its own successes

This approach:

- [AlphaGo](https://en.wikipedia.org/wiki/AlphaGo)
- [AlphaZero](https://en.wikipedia.org/wiki/AlphaZero)

is the foundation of such systems.

## Consciousness - Intuition - Magic

At this point, confusion often arises.

The agent:

- 🔥 is not conscious
- does not "understand" what it is doing
- does not want to win

But:

- ⚡ it exhibits intuitive behaviors

This is:

- ✨ not magic
- statistics
- the result of repetition

## PingPongAI Context

In this project, reinforcement learning:

- does not teach rules
- does not tell the correct move
- does not say "hit here"

It only does this:

> This was the result of what you did.

Over time:

- behaviors that produce good results increase
- behaviors that produce bad results decrease

That's all.

> Goal: To statistically normalize the system toward behaviors that maximize the expected total reward.

Now we have:

- a neural network
- reinforcement learning

So the question is:

> Why do we combine these in this kind of architecture?

In the next document, [**Demis Hassabis' approach**](./05-WhyThisArchitecture.md) will be discussed.

## See Also

- [Home](.//README.md)
- [What AI is and is not, its relation to code](./00-WhatIsAI.md)
- [The concept of learning, supervised / unsupervised / reinforcement](./01-WhatIsLearning.md)
- [Artificial neuron, input/weight/bias, simple example](./02-Neuron.md)
- [Mini neural network, hidden layer, feedforward network](./03-NeuralNetwork.md)
- *Reward and punishment, self-play, basic RL logic*
- [&gt; Hassabis approach, self-play, modular architecture](./05-WhyThisArchitecture.md)
- [PingPongAI.App Rationale](./06-PingPongAI.App.md)
- [PingPongAI.App Game Rules](./07-PingPongAI.App.Rules.md)
- [Rule-Based Agent Approach](./08-RuleBased.md)
