using PingPongAI.AI.Neural;
using PingPongAI.AI.Neural.Activations;

namespace PingPongAI.UnitTest.Neural
{
    public class NeuralNetworkTests
    {
        [Fact]
        public void Compute_ShouldReturnOutputMatchingLastLayerNeuronCount()
        {
            // Ensures that the network output size matches the neuron count
            // of the last added layer, validating correct layer chaining.
            NeuralNetwork network = new(inputCount: 2);

            network.AddLayer(neuronCount: 3, activation: new SigmoidActivation());
            network.AddLayer(neuronCount: 1, activation: new SigmoidActivation());

            double[] output = network.Compute([1.0, 0.5]);

            Assert.NotNull(output);
            Assert.Single(output);
        }

        [Fact]
        public void Compute_ShouldBeDeterministic_ForSameInput()
        {
            // Verifies that the network produces identical outputs
            // for the same input when no training occurs between calls.
            NeuralNetwork network = new(inputCount: 2);

            network.AddLayer(2, new SigmoidActivation());
            network.AddLayer(1, new SigmoidActivation());

            double[] input = { 0.3, 0.7 };

            double[] o1 = network.Compute(input);
            double[] o2 = network.Compute(input);

            Assert.Equal(o1.Length, o2.Length);
            Assert.Equal(o1[0], o2[0], precision: 10);
        }

        [Fact]
        public void Training_ShouldReduceTotalError()
        {
            // Ensures that repeated training iterations reduce the
            // total prediction error of the network for a simple case.
            NeuralNetwork network = new(inputCount: 1);

            network.AddLayer(1, new SigmoidActivation());

            double[] input = { 1.0 };
            double[] expected = { 1.0 };

            double initialError = System.Math.Abs(
                network.Train(input, expected, learningRate: 0.1)[0]
            );

            double lastError = initialError;

            for (int i = 0; i < 200; i++)
            {
                lastError = System.Math.Abs(
                    network.Train(input, expected, learningRate: 0.1)[0]
                );
            }

            Assert.True(lastError < initialError);
        }
    }
}
