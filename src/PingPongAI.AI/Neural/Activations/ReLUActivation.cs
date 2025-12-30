namespace PingPongAI.AI.Neural.Activations
{
    public sealed class ReLUActivation : IActivationFunction
    {
        public double Activate(double x)
        {
            return x > 0.0 ? x : 0.0;
        }

        public double Derivative(double output)
        {
            return output > 0.0 ? 1.0 : 0.0;
        }
    }
}
