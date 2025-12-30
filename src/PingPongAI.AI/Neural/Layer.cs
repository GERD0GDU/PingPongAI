using PingPongAI.AI.Neural.Activations;
using System;
using System.Collections.Generic;

namespace PingPongAI.AI.Neural
{
    public class Layer
    {
        private readonly List<Neuron> _neurons;

        public IReadOnlyList<Neuron> Neurons => _neurons;

        public Layer(int neuronCount, int inputCount, IActivationFunction activation)
        {
#if DEBUG
            if (neuronCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(neuronCount),
                    $"'{neuronCount}' must be a positive integer greater than 0.");

            if (inputCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(inputCount),
                    $"'{inputCount}' must be a positive integer greater than 0.");

            if (activation == null)
                throw new ArgumentNullException(nameof(activation));
#endif
            _neurons = new List<Neuron>(neuronCount);

            for (int i = 0; i < neuronCount; i++)
            {
                _neurons.Add(new Neuron(inputCount, activation));
            }
        }

        public void InitializeRandom(double min = -1.0, double max = 1.0)
        {
            foreach (var neuron in _neurons)
            {
                neuron.InitializeRandom(min, max);
            }
        }

        public double[] Compute(double[] inputs)
        {
#if DEBUG
            if (inputs == null)
                throw new ArgumentNullException(nameof(inputs));
#endif
            double[] outputs = new double[_neurons.Count];

            for (int i = 0; i < _neurons.Count; i++)
            {
                outputs[i] = _neurons[i].Compute(inputs);
            }

            return outputs;
        }

        public double[] Train(double[] inputs, double[] expectedOutputs, double learningRate)
        {
#if DEBUG
            if (expectedOutputs == null)
                throw new ArgumentNullException(nameof(expectedOutputs));

            if (expectedOutputs.Length != _neurons.Count)
                throw new ArgumentOutOfRangeException(nameof(expectedOutputs),
                    $"Expected outputs vector length ({inputs.Length}) does not match neurons count ({_neurons.Count}).");

            if (learningRate <= 0)
                throw new ArgumentOutOfRangeException(nameof(learningRate),
                    $"'{learningRate}' must be greater than 0.");
#endif
            double[] errors = new double[_neurons.Count];

            for (int i = 0; i < _neurons.Count; i++)
            {
                errors[i] = _neurons[i].Train(
                    inputs,
                    expectedOutputs[i],
                    learningRate
                );
            }

            return errors;
        }

        public double[] TrainFromOutput(double[] outputErrors, double learningRate)
        {
#if DEBUG
            if (outputErrors == null)
            {
                throw new ArgumentNullException(nameof(outputErrors));
            }

            if (outputErrors.Length != Neurons.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(outputErrors));
            }
#endif
            int inputCount = Neurons[0].Weights.Length;
            double[] propagatedErrors = new double[inputCount];

            for (int n = 0; n < Neurons.Count; n++)
            {
                Neuron neuron = Neurons[n];

                // δ = error * activation'(output)
                double delta = outputErrors[n] * neuron.Activation.Derivative(neuron.LastOutput);

                // propagate error to previous layer
                for (int w = 0; w < neuron.Weights.Length; w++)
                {
                    propagatedErrors[w] += neuron.Weights[w] * delta;
                }

                // update weights and bias
                neuron.UpdateWeights(delta, learningRate);
            }

            return propagatedErrors;
        }
    }
}
