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
    }
}
