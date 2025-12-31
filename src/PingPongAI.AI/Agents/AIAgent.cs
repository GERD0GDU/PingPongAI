using PingPongAI.AI.Factory;
using PingPongAI.AI.Neural;
using PingPongAI.AI.Neural.Activations;
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
            _network = new NeuralNetwork(inputCount: 8);

            _network.AddLayer(8, new TanhActivation());
            _network.AddLayer(4, new TanhActivation());
            _network.AddLayer(1, new TanhActivation());
        }

        public override AgentTypes AgentType => AgentTypes.AI;

        public override Direction Decide(GameState state)
        {
            double[] inputs = EncodeState(state);

            double output = _network.Compute(inputs)[0];

            if (output < -0.1)
                return Direction.Up;

            if (output > 0.1)
                return Direction.Down;

            return Direction.None;
        }

        public double[] EncodeState(GameState state)
        {
            PaddleState paddle = Side == PaddleSide.Left
                ? state.LeftPaddle
                : state.RightPaddle;

            // state normalization (-1 ... +1)
            double ballX = (state.Ball.CenterX / state.GameArea.Width) * 2 - 1;
            double ballY = (state.Ball.CenterY / state.GameArea.Height) * 2 - 1;
            double ballVelocityX = state.Ball.Velocity.X / Consts.BALL_SPEED;
            double ballVelocityY = state.Ball.Velocity.Y / Consts.BALL_SPEED;
            double paddleY = (paddle.CenterY / state.GameArea.Height) * 2 - 1;
            double paddleVelocity = paddle.Velocity / Consts.PADDLE_SPEED;
            double distanceToPaddle = (state.Ball.X - (paddle == state.LeftPaddle ? paddle.Right : paddle.X)) / state.GameArea.Width;
            double timeToReachPaddle = Math.Abs((paddle.CenterX - state.Ball.CenterX) / state.Ball.Velocity.X);
            double predictedBallY = state.Ball.CenterY + state.Ball.Velocity.Y * timeToReachPaddle;
            predictedBallY = (Math.Max(0, Math.Min(state.GameArea.Height, predictedBallY)) - (state.GameArea.Height / 2)) / state.GameArea.Height;

            // Converts game state into a normalized input vector
            return new double[]
            {
                ballX,
                ballY,
                ballVelocityX,
                ballVelocityY,
                paddleY,
                paddleVelocity,
                distanceToPaddle,
                predictedBallY
            };
        }

        // Optional hook for supervised or reinforcement learning
        public void Train(double[] inputs, double[] expected, double learningRate)
        {
            _network.Train(inputs, expected, learningRate);
        }
    }
}
