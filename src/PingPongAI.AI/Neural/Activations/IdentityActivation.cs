namespace PingPongAI.AI.Neural.Activations
{
    // Linear pass-through. Used as the last layer of a policy network so that
    // raw logits reach the softmax stage unsquashed.
    public sealed class IdentityActivation : IActivationFunction
    {
        public double Activate(double x)
        {
            return x;
        }

        public double Derivative(double output)
        {
            return 1.0;
        }
    }
}
