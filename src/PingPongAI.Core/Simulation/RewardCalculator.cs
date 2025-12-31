using PingPongAI.Core.Math;
using PingPongAI.Core.States;
using System.Diagnostics;

namespace PingPongAI.Core.Simulation
{
    public static class RewardCalculator
    {
        //private const double HIT_REWARD = +0.1;
        //private const double SCORE_REWARD = +1.0;
        //private const double CONCEDE_PENALTY = -1.0;

        private const double HIT_REWARD = +0.1;
        private const double EDGE_PENALTY = 0.05;

        public static RewardResult Calculate(
            GameState previous,
            GameState current)
        {
            double leftReward = ComputePaddleReward(current, current.LeftPaddle);
            double rightReward = ComputePaddleReward(current, current.RightPaddle);

            return new RewardResult(leftReward, rightReward);
        }

        //private static double ComputePaddleReward(GameState state, PaddleState paddle)
        //{
        //    bool isLeftPaddle = state.LeftPaddle == paddle;
        //    if ((isLeftPaddle && state.Ball.Velocity.X > 0) ||
        //        (!isLeftPaddle && state.Ball.Velocity.X < 0))
        //        return 0.0;

        //    double retVal = 0.0;

        //    retVal += (state.Ball.CenterY - paddle.CenterY) / paddle.Height;

        //    return retVal;
        //}

        private static double ComputePaddleReward(GameState state, PaddleState paddle)
        {
            double reward = 0.0;
            double timeToReachPaddle = System.Math.Abs((paddle.CenterX - state.Ball.CenterX) / state.Ball.Velocity.X);
            double predictedBallY = state.Ball.CenterY + state.Ball.Velocity.Y * timeToReachPaddle;
            predictedBallY = MathEx.Clamp(predictedBallY, 0, state.GameArea.Height);
            double diffY = predictedBallY - paddle.CenterY;

            reward += System.Math.Tanh(diffY / paddle.Height) * HIT_REWARD;

            // Also consider the distance between the ball's velocity (X) and the paddle.
            double approachFactor = state.Ball.Velocity.X * (paddle.Side == PaddleSide.Left ? 1 : -1);
            reward += System.Math.Tanh(approachFactor) * 0.05;

            // Penalty for approaching the edges
            if (paddle.Top < 0.1)
                reward += EDGE_PENALTY;
            if (paddle.Bottom > state.GameArea.Height - 0.1)
                reward -= EDGE_PENALTY;

            Debug.WriteLine($"ballCenterY={state.Ball.CenterY:0.00} paddleCenterY={paddle.CenterY:0.00} reward={reward:0.00} ({(paddle.CenterY - state.Ball.CenterY):0.00})");

            return reward;
        }
    }
}
