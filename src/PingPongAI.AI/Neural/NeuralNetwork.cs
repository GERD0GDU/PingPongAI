using PingPongAI.AI.Neural.Activations;
using System;
using System.Collections.Generic;

namespace PingPongAI.AI.Neural
{
    public class NeuralNetwork
    {
        private readonly List<Layer> _layers;

        public IReadOnlyList<Layer> Layers
        {
            get { return _layers; }
        }

        public int InputCount { get; }

        public NeuralNetwork(int inputCount)
        {
#if DEBUG
            if (inputCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(inputCount),
                    $"'{inputCount}' must be a positive integer greater than 0.");
#endif
            InputCount = inputCount;
            _layers = new List<Layer>();
        }

        public void AddLayer(int neuronCount, IActivationFunction activation)
        {
#if DEBUG
            if (neuronCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(neuronCount),
                    $"'{neuronCount}' must be a positive integer greater than 0.");

            if (activation == null)
                throw new ArgumentNullException(nameof(activation));
#endif
            int layerInputCount;

            if (_layers.Count == 0)
            {
                layerInputCount = InputCount;
            }
            else
            {
                layerInputCount = _layers[_layers.Count - 1].Neurons.Count;
            }

            Layer layer = new Layer(
                neuronCount,
                layerInputCount,
                activation
            );

            layer.InitializeRandom();
            _layers.Add(layer);
        }

        public double[] Compute(double[] inputs)
        {
#if DEBUG
            if (inputs == null)
                throw new ArgumentNullException(nameof(inputs));

            if (inputs.Length != InputCount)
                throw new ArgumentOutOfRangeException(nameof(inputs),
                    $"Input vector length ({inputs.Length}) does not match weight count ({InputCount}).");
#endif
            double[] output = inputs;

            for (int i = 0; i < _layers.Count; i++)
            {
                output = _layers[i].Compute(output);
            }

            return output;
        }

        public double[] Train(double[] inputs, double[] expected, double learningRate)
        {
#if DEBUG
            if (expected == null)
                throw new ArgumentNullException(nameof(expected));
#endif
            double[] output = Compute(inputs);
            double[] errors = new double[expected.Length];

            for (int i = 0; i < expected.Length; i++)
            {
                errors[i] = expected[i] - output[i];
            }

            for (int i = _layers.Count - 1; i >= 0; i--)
            {
                errors = _layers[i].TrainFromOutput(errors, learningRate);
            }

            return errors;
        }

        // REINFORCE update. The network's output is treated as policy logits,
        // softmax is applied here, and the gradient pushes the chosen action's
        // log-probability up (or down) in proportion to `advantage`.
        // The last layer must use IdentityActivation: the derivative-of-output
        // factor inside TrainFromOutput is the right shape only when the final
        // activation is linear.
        public double[] TrainPolicyGradient(double[] inputs, int actionIndex, double advantage, double learningRate)
        {
#if DEBUG
            if (inputs == null)
                throw new ArgumentNullException(nameof(inputs));

            if (_layers.Count == 0)
                throw new InvalidOperationException("Network has no layers.");

            if (learningRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(learningRate),
                    $"'{learningRate}' must be greater than 0.");
#endif
            double[] logits = Compute(inputs);

#if DEBUG
            if (actionIndex < 0 || actionIndex >= logits.Length)
                throw new ArgumentOutOfRangeException(nameof(actionIndex),
                    $"'{actionIndex}' is out of range for {logits.Length} output(s).");
#endif
            double[] probs = Softmax(logits);

            // ∂(-G · log π(a|s))/∂logit_i = G · (1_{i=a} − π_i)
            // The existing backprop adds `learningRate · error · input` to weights,
            // so passing this expression as the output-layer error matches the
            // sign convention used by Train(...).
            double[] errors = new double[logits.Length];
            for (int i = 0; i < logits.Length; i++)
            {
                double oneHot = i == actionIndex ? 1.0 : 0.0;
                errors[i] = advantage * (oneHot - probs[i]);
            }

            for (int i = _layers.Count - 1; i >= 0; i--)
            {
                errors = _layers[i].TrainFromOutput(errors, learningRate);
            }

            return errors;
        }

        public static double[] Softmax(double[] logits)
        {
#if DEBUG
            if (logits == null)
                throw new ArgumentNullException(nameof(logits));

            if (logits.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(logits),
                    "Logits vector must contain at least one element.");
#endif
            // Subtract max for numerical stability.
            double max = logits[0];
            for (int i = 1; i < logits.Length; i++)
            {
                if (logits[i] > max) max = logits[i];
            }

            double[] result = new double[logits.Length];
            double sum = 0.0;
            for (int i = 0; i < logits.Length; i++)
            {
                result[i] = Math.Exp(logits[i] - max);
                sum += result[i];
            }

            for (int i = 0; i < logits.Length; i++)
            {
                result[i] /= sum;
            }

            return result;
        }
    }
}
