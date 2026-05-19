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
            // The teacher signal is computed from the observed (pre-update)
            // state so it matches the input the agent decided on.
            double left = ComputeExpectedForPaddle(previous, previous.LeftPaddle);
            double right = ComputeExpectedForPaddle(previous, previous.RightPaddle);

            return new ResultPair(left, right);
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
