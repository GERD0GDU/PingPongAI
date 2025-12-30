using PingPongAI.AI.Factory;
using PingPongAI.AI.Neural;
using PingPongAI.AI.Neural.Activations;
using PingPongAI.Core.States;
using System.Diagnostics;

namespace PingPongAI.AI.Agents
{
    public sealed class AIAgent : PongAgent, IPongAgent
    {
        private readonly NeuralNetwork _network;

        public AIAgent(PaddleSide side)
            : base(side)
        {
            // 6 inputs representing the current game state
            _network = new NeuralNetwork(inputCount: 6);

            _network.AddLayer(8, new TanhActivation());
            _network.AddLayer(4, new TanhActivation());
            _network.AddLayer(1, new TanhActivation());
        }

        public override AgentTypes AgentType => AgentTypes.AI;

        public override Direction Decide(GameState state)
        {
            double[] inputs = EncodeState(state);

            double output = _network.Compute(inputs)[0];
            Debug.WriteLine(output);

            if (output < -0.05)
                return Direction.Up;

            if (output > 0.05)
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
            double ballVelocityX = state.Ball.Velocity.X / GameState.BALL_SPEED;
            double ballVelocityY = state.Ball.Velocity.Y / GameState.BALL_SPEED;
            double paddleY = (paddle.CenterY / state.GameArea.Height) * 2 - 1;
            double paddleVelocity = paddle.Velocity / GameState.PADDLE_SPEED;

            // Converts game state into a normalized input vector
            return new double[]
            {
                ballX,
                ballY,
                ballVelocityX,
                ballVelocityY,
                paddleY,
                paddleVelocity
            };
        }

        // Optional hook for supervised or reinforcement learning
        public void Train(double[] inputs, double[] expected, double learningRate)
        {
            _network.Train(inputs, expected, learningRate);
        }
    }
}
