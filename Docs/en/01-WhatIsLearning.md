# What Is Learning?

In this document, we will examine the concept of "learning" from the perspective of artificial intelligence and software development.

Our goal is not to memorize academic definitions.
It is to truly understand when we say that a system is "learning".

## What Do We Actually Mean by Learning?

In daily life, what we usually call learning is this:

- we make a mistake
- we see the result
- we change our behavior next time

On the AI side, the core idea is the same.

A system:

- encounters a situation
- makes a decision
- observes the outcome of that decision
- tries to behave differently in similar situations in the future

If this cycle exists, there is a learning process.

## The Difference Between Learning and Writing Rules

In traditional software development, there is usually the following approach:

> If A happens, do B.

This is a rule.
As the number of rules increases, the system becomes complex and loses flexibility.

In learning systems:

> When A happens, I learn over time what I should do.

So the developer:

- does not write all the rules one by one
- only defines the learning environment and the feedback

This difference is the most critical distinction between AI and classical software.

## Types of Learning in Artificial Intelligence

In AI literature, learning is generally examined under three main categories.

### 1. Supervised Learning

- The system is given both input and the correct output
- Such as "this image is a cat, this one is a dog"
- The model learns to imitate the correct answer

Advantages:

- Easy to control
- Results are predictable

Disadvantages:

- Requires labeled data
- In real life, there is not always a correct answer

### 2. Unsupervised Learning

- The system is given only data
- There is no correct answer
- The model discovers similarities and structures in the data

Examples:

- Clustering
- Anomaly detection

This approach is more exploration-oriented.

### 3. Reinforcement Learning

This is the type of learning we are most interested in for this project.

- The system exists within an environment
- Takes actions
- Receives rewards or punishments
- Its goal is to maximize the total reward in the long run

There is no correct answer here.
There is only feedback from outcomes.

## Why Does Learning Take Time?

The learning process is usually built on:

- trial
- error
- repetition

At the beginning, the system:

- makes poor decisions
- performs meaningless actions
- fails

This is expected and desired.

Learning is not about finding the correct result on the first attempt,
but about making fewer mistakes over time.

## Learning with the Ping-Pong Game

In this project, learning is reduced to the following question:

> How should I move my paddle so that I do not miss the ball?

For the AI side:

- The position of the ball is an input
- The movement of the paddle is an action
- Hitting the ball is a reward
- Missing it is a punishment

By playing thousands of games, the system:

- learns over time which action leads to better results in which situation

No one tells the AI "go here".
Only the outcomes are shown.

## Summary

- Learning is the change of behavior based on outcomes
- In AI, learning means designing environments instead of writing rules
- Reinforcement learning is based on trial and error
- Ping-pong is an ideal example to observe this concept

In the next document, we will move on to the concept of
[**the artificial neuron**](./02-Neuron.md), which is the fundamental building block of this learning process.

## See Also

- [Home Page](./README.md)
- [What is AI, what it is not, its relation to code](00-WhatIsAI.md)
- *The concept of learning, supervised / unsupervised / reinforcement*
- &gt; [Artificial neuron, input/weight/bias, simple example](./02-Neuron.md)
- [Mini neural network, hidden layer, feedforward network](./03-NeuralNetwork.md)
- [Reward and punishment, self-play, basic RL logic](./04-ReinforcementLearning.md)
- [Hassabis approach, self-play, modular architecture](./05-WhyThisArchitecture.md)
- [PingPongAI.App Rationale](./06-PingPongAI.App.md)
