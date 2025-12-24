using System.Windows;

namespace PingPongAI.Core
{
    public static class Utils
    {
        public static HitInfo RayVsRect(Ray ray, Rect rect)
        {
            HitInfo hit = new() { Hit = false };

            if (ray.Direction.X == 0 && ray.Direction.Y == 0)
                return hit;

            Vector invDir = new(
                ray.Direction.X != 0 ? 1.0 / ray.Direction.X : double.PositiveInfinity,
                ray.Direction.Y != 0 ? 1.0 / ray.Direction.Y : double.PositiveInfinity
            );

            Vector tNear = new(
                (rect.Left - ray.Origin.X) * invDir.X,
                (rect.Top - ray.Origin.Y) * invDir.Y
            );

            Vector tFar = new(
                (rect.Right - ray.Origin.X) * invDir.X,
                (rect.Bottom - ray.Origin.Y) * invDir.Y
            );

            if (double.IsNaN(tNear.X) || double.IsNaN(tNear.Y))
                return hit;

            if (tNear.X > tFar.X) (tNear.X, tFar.X) = (tFar.X, tNear.X);
            if (tNear.Y > tFar.Y) (tNear.Y, tFar.Y) = (tFar.Y, tNear.Y);

            if (tNear.X > tFar.Y || tNear.Y > tFar.X)
                return hit;

            double tHitNear = Math.Max(tNear.X, tNear.Y);
            double tHitFar = Math.Min(tFar.X, tFar.Y);

            if (tHitFar < 0 || tHitNear < 0 || tHitNear > 1)
                return hit;

            hit.Hit = true;
            hit.Time = tHitNear;

            if (tNear.X > tNear.Y)
                hit.Normal = new Vector(invDir.X < 0 ? 1 : -1, 0);
            else
                hit.Normal = new Vector(0, invDir.Y < 0 ? 1 : -1);

            return hit;
        }

    }
}
