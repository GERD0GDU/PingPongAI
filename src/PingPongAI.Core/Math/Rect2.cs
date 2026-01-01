namespace PingPongAI.Core.Math
{
    public struct Rect2
    {
        public double X;
        public double Y;
        public double Width;
        public double Height;

        public Rect2(double x, double y, double w, double h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }

        public double Left => X;
        public double Right => X + Width;
        public double Top => Y;
        public double Bottom => Y + Height;

        public override string ToString()
        {
            return $"{{{nameof(X)}={X} {nameof(Y)}={Y} {nameof(Width)}={Width} {nameof(Height)}={Height}}}";
        }
    }
}
