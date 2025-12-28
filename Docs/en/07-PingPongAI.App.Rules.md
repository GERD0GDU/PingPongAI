# PingPongAI.App Game Rules

This document defines the core game rules used in the PingPongAI.App desktop application.

The defined rules aim to ensure deterministic gameplay, consistent behavior during AI training, and a fully observable and controllable simulation environment.

## Game Initialization

- When the application is launched, the game starts automatically
- The player who will start the game is selected randomly
- Random selection is used to maintain neutrality at each new start

## Ball Initialization

- The ball is launched from a random point on the paddle of the starting player
- The ball is sent from the selected player toward the opposing player
- The initial direction and speed of the ball are determined by the game’s starting parameters

## Collision and Bounce Behavior

- When the opposing player hits the ball with the paddle, the collision point is calculated
- Based on the point where the ball contacts the paddle:

- the bounce angle changes
- the ball speed may be adjusted differently

- Hits closer to the center of the paddle produce more stable bounces
- Hits closer to the edges of the paddle produce sharper angles

This mechanism is intentionally added to create gameplay variety and a learnable behavior space.

## Scoring Rules

- If a player successfully returns the ball, the game continues
- If a player misses the ball:

- the opposing player is awarded 1 point

- Scoring occurs only when the ball is missed
- No other situation produces points during gameplay

## Round End and Restart

- A round ends when one of the players misses the ball
- The ball is reset to its initial state
- The new round:

- is started by the player who won the previous round
- the ball is launched from a random point on the winner’s paddle
- the ball is sent toward the opposing player

This approach gives the winning player an advantage without introducing deterministic repetition.

## General Principles

- Game rules are simple and explicit
- Physics and collision calculations are deterministic
- Identical initial conditions produce identical outcomes
- The rules do not differentiate between AI and human players
- From the game engine’s perspective, all players are treated equally

These rules ensure that `PingPongAI.App` functions both as a playable game and as a reliable AI training environment.

## Current Status

You can access the code related to the changes and updates made up to this point in the document via the [v1.0-basic](../../tree/v1.0-basic) tag.

## See Also

- [Home Page](./README.md)
- [What is AI, what it is not, its relation to code](00-WhatIsAI.md)
- [The concept of learning, supervised / unsupervised / reinforcement](./01-WhatIsLearning.md)
- [Artificial neuron, input/weight/bias, simple example](./02-Neuron.md)
- [Mini neural network, hidden layer, feed-forward network](./03-NeuralNetwork.md)
- [Reward and punishment, self-play, basic RL logic](./04-ReinforcementLearning.md)
- [Hassabis approach, self-play, modular architecture](./05-WhyThisArchitecture.md)
- [PingPongAI.App Rationale](./06-PingPongAI.App.md)
- *PingPongAI.App Game Rules*
