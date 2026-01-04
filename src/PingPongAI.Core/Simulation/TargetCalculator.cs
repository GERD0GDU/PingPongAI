using PingPongAI.Core.Math;
using PingPongAI.Core.Physics;
using PingPongAI.Core.States;
using System;

namespace PingPongAI.Core.Simulation
{
    public static class TargetCalculator
    {
        public static ResultPair Calculate(
            GameState previous,
            GameState current)
        {
            double leftReward = ComputeExpectedForPaddle(current, current.LeftPaddle);
            double rightReward = ComputeExpectedForPaddle(current, current.RightPaddle);

            return new ResultPair(leftReward, rightReward);
        }

        private static double ComputeExpectedForPaddle(GameState state, PaddleState paddle)
        {
            if (paddle.Side == PaddleSide.Left && state.Ball.Velocity.X > 0)
                return 0.0;
            else if (paddle.Side == PaddleSide.Right && state.Ball.Velocity.X < 0)
                return 0.0;

            double expected = 0.0;
            double predictBallCenterY = Collision.PredictBallY(state) + state.Ball.Radius;
            double relativeY = (predictBallCenterY - paddle.CenterY) / (paddle.Height / 2);
            if (System.Math.Abs(relativeY) < 1.0)
                relativeY *= 0.5;
            relativeY = MathEx.Clamp(relativeY, -1.0, 1.0);

            expected += relativeY;

            return expected;
        }
    }
}
