namespace PingPongAI.Core.Math
{
    public struct Vec2
    {
        public double X;
        public double Y;

        public Vec2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vec2 operator +(Vec2 a, Vec2 b)
            => new Vec2(a.X + b.X, a.Y + b.Y);

        public static Vec2 operator -(Vec2 a, Vec2 b)
            => new Vec2(a.X - b.X, a.Y - b.Y);

        public static Vec2 operator *(Vec2 v, double s)
            => new Vec2(v.X * s, v.Y * s);

        public double LengthSquared
            => X * X + Y * Y;

        public double Length
            => System.Math.Sqrt(LengthSquared);

        public Vec2 Normalized()
        {
            double len = Length;
            return len > 0 ? new Vec2(X / len, Y / len) : new Vec2(0, 0);
        }

        public override string ToString()
        {
            return $"{{{nameof(X)}={X} {nameof(Y)}={Y}}}";
        }
    }
}
