using PingPongAI.Core.Math;
using System;

namespace PingPongAI.Core.States
{
    public sealed class PaddleState : ICloneable
    {
        public Rect2 Bounds;
        public int Direction;

        public double X
        {
            get { return Bounds.X; }
            set { Bounds.X = value; }
        }

        public double Y
        {
            get { return Bounds.Y; }
            set { Bounds.Y = value; }
        }

        public double Width => Bounds.Width;
        public double Height => Bounds.Height;
        public double Right => Bounds.Right;
        public double Bottom => Bounds.Bottom;

        public Vec2 Position
        {
            get { return new Vec2(Bounds.X, Bounds.Y); }
            set
            {
                Bounds.X = value.X;
                Bounds.Y = value.Y;
            }
        }

        // ICloneable implements
        public object Clone()
        {
            return new PaddleState()
            {
                Bounds = Bounds,
                Direction = Direction
            };
        }
    }
}
