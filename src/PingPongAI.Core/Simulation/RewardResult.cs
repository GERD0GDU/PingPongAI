namespace PingPongAI.Core.Simulation
{
    public sealed class RewardResult
    {
        public double Left { get; }
        public double Right { get; }

        public RewardResult(double left, double right)
        {
            Left = left;
            Right = right;
        }

        public static RewardResult Zero =>
            new RewardResult(0.0, 0.0);

        public override string ToString()
        {
            return $"{Left:0.##} | {Right:0.##}";
        }
    }
}
