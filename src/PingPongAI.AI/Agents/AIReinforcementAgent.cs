using PingPongAI.AI.Factory;
using PingPongAI.AI.Neural;
using PingPongAI.AI.Neural.Activations;
using PingPongAI.Core.Math;
using PingPongAI.Core.States;
using System;
using System.Collections.Generic;

namespace PingPongAI.AI.Agents
{
    public sealed class AIReinforcementAgent : PongAgent, IPongAgent
    {
        private const int INPUT_COUNT = 6;
        private const int ACTION_COUNT = 3;

        private readonly NeuralNetwork _network;
        private readonly Random _random;
        private readonly List<EpisodeStep> _episodeBuffer;

        public double Gamma { get; set; }
        public double LearningRate { get; set; }
        public bool IsTrainingEnabled { get; set; }

        public AIReinforcementAgent(PaddleSide side)
            : this(side, null)
        {
        }

        public AIReinforcementAgent(PaddleSide side, NeuralNetwork network)
            : base(side)
        {
            if (network == null)
            {
                _network = BuildDefaultNetwork();
            }
            else
            {
#if DEBUG
                ValidateNetworkShape(network);
#endif
                _network = network;
            }

            _random = new Random();
            _episodeBuffer = new List<EpisodeStep>();

            // γ=0.99 at 60 fps gives an effective credit-assignment horizon
            // of ~100 frames (~1.7s), enough to span a typical ralli.
            Gamma = 0.99;
            // Lower than supervised because EndEpisode applies one gradient
            // step per buffered frame; 600 sequential micro-updates per
            // ralli would diverge at 0.01.
            LearningRate = 0.001;
            IsTrainingEnabled = true;
        }

        public override AgentTypes AgentType => AgentTypes.AI_Reinforcement;

        public NeuralNetwork Network => _network;

        public int PendingEpisodeStepCount => _episodeBuffer.Count;

        public override Direction Decide(GameState state)
        {
            double[] inputs = EncodeState(state);
            double[] logits = _network.Compute(inputs);
            double[] probs = NeuralNetwork.Softmax(logits);

            int actionIndex = SampleAction(probs);

            if (IsTrainingEnabled)
            {
                _episodeBuffer.Add(new EpisodeStep(inputs, actionIndex));
            }

            return IndexToDirection(actionIndex);
        }

        public double[] EncodeState(GameState state)
        {
            // Kept identical to AISupervisedAgent.EncodeState so a single
            // state representation can be compared across agent types.
            PaddleState paddle = Side == PaddleSide.Left
                ? state.LeftPaddle
                : state.RightPaddle;

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

            return new double[]
            {
                ballX,
                relativeY,
                ballVelocityX,
                ballVelocityY,
                paddleVelocity,
                distanceToPaddle
            };
        }

        // Adds a reward signal to the most recently decided step.
        // Reward arrives between two Decide() calls: it is the consequence
        // of the last action and therefore attaches to the last buffer entry.
        public void RegisterReward(double reward)
        {
            if (_episodeBuffer.Count == 0)
                return;

            _episodeBuffer[_episodeBuffer.Count - 1].Reward += reward;
        }

        // Attaches a reward to the action recorded one tick earlier rather
        // than the most recent one. Required when the simulator's update
        // order makes a consequence observable only on the next tick:
        // GameSimulator.Update runs UpdateBall (collision detection, sets
        // paddle.HasHitBall) BEFORE UpdatePaddles applies the current
        // frame's action. So when HasHitBall is true in frame N, the
        // collision was caused by the paddle position established by
        // action N-1, not the action just buffered in Decide() this frame.
        // Crediting +1 to Count-1 would attribute the only positive signal
        // to a random unrelated action — the policy never learns.
        public void RegisterRewardForPreviousAction(double reward)
        {
            if (_episodeBuffer.Count < 2)
                return;

            _episodeBuffer[_episodeBuffer.Count - 2].Reward += reward;
        }

        // Closes the current ralli: attaches the terminal reward to the
        // last step, computes discounted returns backward, and runs one
        // policy-gradient pass per step.
        public void EndEpisode(double terminalReward)
        {
            RegisterReward(terminalReward);

            if (!IsTrainingEnabled || _episodeBuffer.Count == 0)
            {
                _episodeBuffer.Clear();
                return;
            }

            double[] returns = new double[_episodeBuffer.Count];
            double g = 0.0;
            for (int t = _episodeBuffer.Count - 1; t >= 0; t--)
            {
                g = _episodeBuffer[t].Reward + Gamma * g;
                returns[t] = g;
            }

            // Subtract the episode mean as a baseline. Without this every
            // return in a losing ralli is negative and the gradient only
            // says "everything I did was bad" instead of "this was worse
            // than that". Standard variance-reduction trick for REINFORCE.
            double mean = 0.0;
            for (int t = 0; t < returns.Length; t++)
                mean += returns[t];
            mean /= returns.Length;
            for (int t = 0; t < returns.Length; t++)
                returns[t] -= mean;

            for (int t = 0; t < _episodeBuffer.Count; t++)
            {
                EpisodeStep step = _episodeBuffer[t];
                _network.TrainPolicyGradient(
                    step.Inputs,
                    step.ActionIndex,
                    returns[t],
                    LearningRate
                );
            }

            _episodeBuffer.Clear();
        }

        public void SaveWeights(string path)
        {
            NetworkPersistence.Save(_network, path);
        }

        private static NeuralNetwork BuildDefaultNetwork()
        {
            NeuralNetwork network = new NeuralNetwork(INPUT_COUNT);
            network.AddLayer(8, new TanhActivation());
            network.AddLayer(4, new TanhActivation());
            // Last layer must be linear so raw logits reach Softmax
            // and the policy-gradient backprop sign convention holds.
            network.AddLayer(ACTION_COUNT, new IdentityActivation());
            // Xavier init keeps tanh hidden layers out of saturation so
            // gradient signal can actually reach them during training.
            network.InitializeXavier();
            return network;
        }

#if DEBUG
        private static void ValidateNetworkShape(NeuralNetwork network)
        {
            if (network.InputCount != INPUT_COUNT)
                throw new ArgumentException(
                    $"Network input count ({network.InputCount}) does not match agent ({INPUT_COUNT}).",
                    nameof(network));

            int lastLayerSize = network.Layers[network.Layers.Count - 1].Neurons.Count;
            if (lastLayerSize != ACTION_COUNT)
                throw new ArgumentException(
                    $"Network output count ({lastLayerSize}) does not match action count ({ACTION_COUNT}).",
                    nameof(network));
        }
#endif

        private int SampleAction(double[] probs)
        {
            double r = _random.NextDouble();
            double cumulative = 0.0;
            for (int i = 0; i < probs.Length; i++)
            {
                cumulative += probs[i];
                if (r < cumulative)
                    return i;
            }
            return probs.Length - 1;
        }

        private static Direction IndexToDirection(int index)
        {
            switch (index)
            {
                case 0: return Direction.Up;
                case 1: return Direction.Down;
                case 2: return Direction.None;
                default:
                    throw new ArgumentOutOfRangeException(nameof(index));
            }
        }

        private sealed class EpisodeStep
        {
            public double[] Inputs;
            public int ActionIndex;
            public double Reward;

            public EpisodeStep(double[] inputs, int actionIndex)
            {
                Inputs = inputs;
                ActionIndex = actionIndex;
                Reward = 0.0;
            }
        }
    }
}
