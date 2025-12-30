using System;

namespace PingPongAI.AI.Neural.Activations
{
    public sealed class SigmoidActivation : IActivationFunction
    {
        public double Activate(double x)
        {
            return 1.0 / (1.0 + Math.Exp(-x));
        }

        public double Derivative(double output)
        {
            return output * (1.0 - output);
        }
    }
}
