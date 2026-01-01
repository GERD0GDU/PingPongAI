using PingPongAI.Core.Math;
using PingPongAI.Core.States;

namespace PingPongAI.Core.Simulation
{
    public static class RewardCalculator
    {
        private const double HIT_REWARD = 0.1;
        private const double EDGE_PENALTY = 0.05;

        public static RewardResult Calculate(
            GameState previous,
            GameState current)
        {
            double leftReward = ComputePaddleReward(current, current.LeftPaddle);
            double rightReward = ComputePaddleReward(current, current.RightPaddle);

            return new RewardResult(leftReward, rightReward);
        }

        private static double ComputePaddleReward(GameState state, PaddleState paddle)
        {
            if (paddle.Side == PaddleSide.Right) return 0.0;
            if (paddle.Side == PaddleSide.Left && state.Ball.Velocity.X > 0)
                return 0.0;
            else if (paddle.Side == PaddleSide.Right && state.Ball.Velocity.X < 0)
                return 0.0;

            double reward = 0.0;
            double predictedBallY = PredictBallY(
                state.Ball.CenterX,
                state.Ball.CenterY,
                state.Ball.Velocity.X,
                state.Ball.Velocity.Y,
                paddle.Right + state.Ball.Radius,
                state.Bounds.Height
            );

            double relativeY = (state.Ball.CenterY - paddle.CenterY) / (state.Bounds.Height / 2);
            relativeY = MathEx.Clamp(relativeY, -1.0, 1.0);

            reward += relativeY;

            return reward;
        }

        private static double PredictBallY(
            double x0,
            double y0,
            double vx,
            double vy,
            double xtarget,
            double height)
        {
            double t = (xtarget - x0) / vx;
            double yRaw = y0 + vy * t;

            double period = 2 * height;

            double yMod = yRaw % period;
            if (yMod < 0)
                yMod += period;

            if (yMod <= height)
                return yMod;

            return period - yMod;
        }
    }
}
