using PingPongAI.AI.Neural;
using PingPongAI.AI.Neural.Activations;

namespace PingPongAI.UnitTest.Neural
{
    public class LayerTests
    {
        // Verifies that Layer.Compute returns one output value per neuron.
        // This test guarantees that the output array size always matches
        // the number of neurons defined in the layer and that the method
        // does not return null or an invalid result structure.
        [Fact]
        public void Compute_ShouldReturnOutputForEachNeuron()
        {
            Layer layer = new(
                neuronCount: 3,
                inputCount: 2,
                activation: new SigmoidActivation()
            );

            layer.InitializeRandom();

            double[] inputs = [1.0, 0.5];

            double[] outputs = layer.Compute(inputs);

            Assert.NotNull(outputs);
            Assert.Equal(3, outputs.Length);
        }

        // Ensures that Layer.Compute is deterministic.
        // When called multiple times with the same input and unchanged
        // internal state (weights and biases), the method must return
        // identical outputs. This guarantees that Compute does not modify
        // internal state or rely on randomness during forward propagation.
        [Fact]
        public void Compute_ShouldBeDeterministic_ForSameInput()
        {
            Layer layer = new(
                neuronCount: 2,
                inputCount: 2,
                activation: new SigmoidActivation()
            );

            layer.InitializeRandom();

            double[] inputs = [0.2, 0.8];

            double[] o1 = layer.Compute(inputs);
            double[] o2 = layer.Compute(inputs);

            Assert.Equal(o1.Length, o2.Length);

            for (int i = 0; i < o1.Length; i++)
            {
                Assert.Equal(o1[i], o2[i], precision: 10);
            }
        }

        // Verifies that the training process effectively reduces the error.
        // By repeatedly training the layer with the same input and expected
        // output, this test ensures that weight and bias updates move in the
        // correct direction and that the learning mechanism is functional.
        [Fact]
        public void Training_ShouldReduceLayerError()
        {
            Layer layer = new(
                neuronCount: 1,
                inputCount: 1,
                activation: new SigmoidActivation()
            );

            layer.InitializeRandom();

            double[] inputs = [1.0];
            double[] expected = [1.0];

            double[] initialErrors = layer.Train(inputs, expected, learningRate: 0.1);
            double initialError = Math.Abs(initialErrors[0]);

            double lastError = initialError;

            for (int i = 0; i < 200; i++)
            {
                double[] errors = layer.Train(inputs, expected, learningRate: 0.1);
                lastError = Math.Abs(errors[0]);
            }

            Assert.True(lastError < initialError);
        }
    }
}
