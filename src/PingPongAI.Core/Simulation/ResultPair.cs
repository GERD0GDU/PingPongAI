using System.Diagnostics;

namespace PingPongAI.Core.Simulation
{
    [DebuggerDisplay(@"\{Left={Left} Right={Right}\}")]
    public sealed class ResultPair
    {
        public double Left { get; }
        public double Right { get; }

        public ResultPair(double left, double right)
        {
            Left = left;
            Right = right;
        }

        public static ResultPair Zero =>
            new ResultPair(0.0, 0.0);

        public override string ToString()
        {
            return $"{Left:0.##} | {Right:0.##}";
        }
    }
}
