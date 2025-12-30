using PingPongAI.AI.Neural;
using PingPongAI.AI.Neural.Activations;

namespace PingPongAI.UnitTest.Neural
{
    public class XORTests
    {
        [Fact]
        public void NeuralNetwork_ShouldLearn_XOR_Function()
        {
            // Verifies that a multi-layer neural network can learn
            // the non-linearly separable XOR problem using backpropagation.
            NeuralNetwork network = new NeuralNetwork(inputCount: 2);

            network.AddLayer(2, new TanhActivation());
            network.AddLayer(1, new TanhActivation());

            double[][] inputs =
            {
                [0.0, 0.0],
                [0.0, 1.0],
                [1.0, 0.0],
                [1.0, 1.0]
            };

            double[][] expected =
            {
                [0.0],
                [1.0],
                [1.0],
                [0.0]
            };

            // Train the network
            for (int epoch = 0; epoch < 10000; epoch++)
            {
                for (int i = 0; i < inputs.Length; i++)
                {
                    network.Train(inputs[i], expected[i], learningRate: 0.1);
                }
            }

            // Validate predictions
            for (int i = 0; i < inputs.Length; i++)
            {
                double output = network.Compute(inputs[i])[0];
                double prediction = output >= 0.5 ? 1.0 : 0.0;

                Assert.Equal(expected[i][0], prediction);
            }
        }
    }
}
