using PingPongAI.AI.Neural.Activations;
using System;

namespace PingPongAI.AI.Neural
{
    public class Neuron
    {
        private static readonly Random _random = new Random(Environment.TickCount);

        private double[] _lastInputs;

        public double[] Weights { get; }
        public double Bias { get; private set; }

        public IActivationFunction Activation { get; }

        // Backpropagation icin son cikti saklanir
        public double LastOutput { get; private set; }

        public Neuron(int inputCount, IActivationFunction activation)
        {
#if DEBUG
            if (inputCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(inputCount),
                    $"'{inputCount}' must be a positive integer greater than 0.");

            if (activation == null)
                throw new ArgumentNullException(nameof(activation));
#endif // DEBUG
            Weights = new double[inputCount];
            Activation = activation;
        }

        public void InitializeRandom(double min = -1.0, double max = 1.0)
        {
            for (int i = 0; i < Weights.Length; i++)
            {
                Weights[i] = min + _random.NextDouble() * (max - min);
            }

            Bias = min + _random.NextDouble() * (max - min);
        }

        public double Compute(double[] inputs)
        {
#if DEBUG
            if (inputs == null)
                throw new ArgumentNullException(nameof(inputs));

            if (Weights.Length != inputs.Length)
                throw new ArgumentOutOfRangeException(nameof(inputs),
                    $"Input vector length ({inputs.Length}) does not match weight count ({Weights.Length}).");
#endif // DEBUG

            _lastInputs = inputs;
            double sum = Bias;

            for (int i = 0; i < inputs.Length; i++)
            {
                sum += inputs[i] * Weights[i];
            }

            LastOutput = Activation.Activate(sum);
            return LastOutput;
        }

        public void UpdateWeights(double delta, double learningRate)
        {
#if DEBUG
            if (learningRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(learningRate),
                    $"'{learningRate}' must be greater than 0.");

            if (_lastInputs == null)
                throw new InvalidOperationException("Compute must be called before UpdateWeights.");
#endif
            for (int i = 0; i < Weights.Length; i++)
            {
                Weights[i] += learningRate * delta * _lastInputs[i];
            }

            Bias += learningRate * delta;
        }

        public double Train(double[] inputs, double expected, double learningRate)
        {
#if DEBUG
            if (learningRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(learningRate),
                    $"'{learningRate}' must be greater than 0.");
#endif
            double output = Compute(inputs);
            double error = expected - output;

            double delta = error * Activation.Derivative(output);
            UpdateWeights(delta, learningRate);

            return error;
        }
    }
}
