using PingPongAI.Core.Math;
using PingPongAI.Core.States;

namespace PingPongAI.Core.Physics
{
    public static class Collision
    {
        public static HitInfo RayVsRect(Ray2 ray, Rect2 rect)
        {
            HitInfo hit = new HitInfo() { Hit = false };

            if (ray.Direction.X == 0 && ray.Direction.Y == 0)
                return hit;

            Vec2 invDir = new Vec2(
                ray.Direction.X != 0 ? 1.0 / ray.Direction.X : double.PositiveInfinity,
                ray.Direction.Y != 0 ? 1.0 / ray.Direction.Y : double.PositiveInfinity
            );

            Vec2 tNear = new Vec2(
                (rect.Left - ray.Origin.X) * invDir.X,
                (rect.Top - ray.Origin.Y) * invDir.Y
            );

            Vec2 tFar = new Vec2(
                (rect.Right - ray.Origin.X) * invDir.X,
                (rect.Bottom - ray.Origin.Y) * invDir.Y
            );

            if (double.IsNaN(tNear.X) || double.IsNaN(tNear.Y))
                return hit;

            if (tNear.X > tFar.X) (tNear.X, tFar.X) = (tFar.X, tNear.X);
            if (tNear.Y > tFar.Y) (tNear.Y, tFar.Y) = (tFar.Y, tNear.Y);

            if (tNear.X > tFar.Y || tNear.Y > tFar.X)
                return hit;

            double tHitNear = System.Math.Max(tNear.X, tNear.Y);
            double tHitFar = System.Math.Min(tFar.X, tFar.Y);

            if (tHitFar < 0 || tHitNear < 0 || tHitNear > 1)
                return hit;

            hit.Hit = true;
            hit.Time = tHitNear;

            if (tNear.X > tNear.Y)
                hit.Normal = new Vec2(invDir.X < 0 ? 1 : -1, 0);
            else
                hit.Normal = new Vec2(0, invDir.Y < 0 ? 1 : -1);

            return hit;
        }

        public static double PredictBallY(GameState state)
        {
            double x = state.Ball.X;
            double y = state.Ball.Y;
            double vx = state.Ball.Velocity.X;
            double vy = state.Ball.Velocity.Y;
            double xtarget = state.Ball.Velocity.X < 0
                ? state.LeftPaddle.X
                : state.RightPaddle.X - state.Ball.Radius;
            double height = state.Bounds.Height - state.Ball.Height;

            double t = (xtarget - x) / vx;
            double yRaw = y + vy * t;

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
