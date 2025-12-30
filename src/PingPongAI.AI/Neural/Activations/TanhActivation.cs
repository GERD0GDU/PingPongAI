using System;

namespace PingPongAI.AI.Neural.Activations
{
    public sealed class TanhActivation : IActivationFunction
    {
        public double Activate(double x)
        {
            return Math.Tanh(x);
        }

        public double Derivative(double output)
        {
            return 1.0 - (output * output);
        }
    }
}
