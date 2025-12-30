using PingPongAI.AI.Neural;
using PingPongAI.AI.Neural.Activations;

namespace PingPongAI.UnitTest.Neural
{
    public class NeuronTests
    {
        // Verifies that training a single neuron reduces prediction error over time.
        // By repeatedly applying the training rule with the same input and expected
        // output, this test ensures that weight and bias updates improve the neuron's
        // output and that the learning mechanism converges in the correct direction.
        [Fact]
        public void Training_ShouldReduceError()
        {
            Neuron neuron = new(1, new SigmoidActivation());
            neuron.InitializeRandom();

            double initialError = Math.Abs(
                neuron.Train([1.0], expected: 1.0, learningRate: 0.1)
            );

            double lastError = initialError;

            for (int i = 0; i < 200; i++)
            {
                lastError = Math.Abs(
                    neuron.Train([1.0], expected: 1.0, learningRate: 0.1)
                );
            }

            Assert.True(lastError < initialError);
        }
    }
}