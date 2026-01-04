using PingPongAI.AI.Factory;
using PingPongAI.AI.Neural;
using PingPongAI.AI.Neural.Activations;
using PingPongAI.Core.Math;
using PingPongAI.Core.States;
using System;

namespace PingPongAI.AI.Agents
{
    public sealed class AIAgent : PongAgent, IPongAgent
    {
        private readonly NeuralNetwork _network;

        public AIAgent(PaddleSide side)
            : base(side)
        {
            // (n) inputs representing the current game state
            _network = new NeuralNetwork(inputCount: 9);

            _network.AddLayer(16, new TanhActivation());
            _network.AddLayer(16, new TanhActivation());
            _network.AddLayer(1, new TanhActivation());
        }

        public override AgentTypes AgentType => AgentTypes.AI;

        public override Direction Decide(GameState state)
        {
            double[] inputs = EncodeState(state);

            double output = _network.Compute(inputs)[0];

            if (output < -0.6)
                return Direction.Up;

            if (output > 0.6)
                return Direction.Down;

            return Direction.None;
        }

        public double[] EncodeState(GameState state)
        {
            PaddleState paddle = Side == PaddleSide.Left
                ? state.LeftPaddle
                : state.RightPaddle;

            // state normalization (-1 ... +1)
            double ballX = (state.Ball.CenterX / state.Bounds.Width) * 2 - 1;
            double relativeY = MathEx.Clamp((state.Ball.CenterY - paddle.CenterY) / (state.Bounds.Height / 2), -1, +1);
            double ballVelocityX = MathEx.Clamp(state.Ball.Velocity.X / Consts.BALL_SPEED, -1, +1);
            double ballVelocityY = MathEx.Clamp(state.Ball.Velocity.Y / Consts.BALL_SPEED_MAX_Y, -1, +1);
            double paddleVelocity = MathEx.Clamp(paddle.Velocity / Consts.PADDLE_SPEED, -1, +1);
            double distanceToPaddle = (paddle.Side == PaddleSide.Left
                    ? state.Ball.CenterX - paddle.Right
                    : paddle.Left - state.Ball.CenterX) 
                / state.Bounds.Width;
            distanceToPaddle = MathEx.Clamp(distanceToPaddle, 0, 1);
            double topProximity = state.Ball.Y / (state.Bounds.Height / 2);
            double bottomProximity = (state.Bounds.Height - state.Ball.Y) / (state.Bounds.Height / 2);
            topProximity = MathEx.Clamp(topProximity, 0, 1);
            bottomProximity = MathEx.Clamp(bottomProximity, 0, 1);

            double predictedRelativeY;
            if (Math.Abs(ballVelocityX) < 0.1)
            {
                predictedRelativeY = relativeY;
            }
            else
            {
                double timeToReachPaddle = Math.Abs((paddle.CenterX - state.Ball.CenterX) / state.Ball.Velocity.X); // If state.Ball.Velocity.X gets close to zero!!!
                double predictedBallY = state.Ball.CenterY + state.Ball.Velocity.Y * timeToReachPaddle;
                predictedBallY = MathEx.Clamp(predictedBallY, 0, state.Bounds.Height);
                predictedRelativeY = (predictedBallY - paddle.CenterY) / (state.Bounds.Height / 2);
                predictedRelativeY = MathEx.Clamp(predictedRelativeY, -1.0, 1.0);
            }

            // Converts game state into a normalized input vector
            return new double[]
            {
                ballX,
                relativeY,
                predictedRelativeY,
                ballVelocityX,
                ballVelocityY,
                paddleVelocity,
                distanceToPaddle,
                topProximity,
                bottomProximity
            };
        }

        // Optional hook for supervised or reinforcement learning
        public void Train(double[] inputs, double[] expected, double learningRate)
        {
            _network.Train(inputs, expected, learningRate);
        }
    }
}
