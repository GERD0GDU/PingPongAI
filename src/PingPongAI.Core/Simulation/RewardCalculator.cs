using PingPongAI.Core.States;

namespace PingPongAI.Core.Simulation
{
    public static class RewardCalculator
    {
        private const double HIT_REWARD = +0.1;
        private const double SCORE_REWARD = +1.0;
        private const double CONCEDE_PENALTY = -1.0;

        public static RewardResult Calculate(
            GameState previous,
            GameState current)
        {
            double leftReward = 0.0;
            double rightReward = 0.0;

            // 1) skor değişimi
            if (current.LeftScore > previous.LeftScore)
            {
                leftReward += SCORE_REWARD;
                rightReward += CONCEDE_PENALTY;
            }
            else if (current.RightScore > previous.RightScore)
            {
                rightReward += SCORE_REWARD;
                leftReward += CONCEDE_PENALTY;
            }

            // 2) paddle - ball çarpışması
            if (!previous.LeftPaddle.HasHitBall && current.LeftPaddle.HasHitBall)
            {
                leftReward += HIT_REWARD;
            }

            if (!previous.RightPaddle.HasHitBall && current.RightPaddle.HasHitBall)
            {
                rightReward += HIT_REWARD;
            }

            return new RewardResult(leftReward, rightReward);
        }
    }
}
