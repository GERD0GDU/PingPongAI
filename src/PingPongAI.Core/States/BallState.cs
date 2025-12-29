using PingPongAI.Core.Math;
using System;

namespace PingPongAI.Core.States
{
    public sealed class BallState : ICloneable
    {
        public Rect2 Bounds;
        public Vec2 Velocity;
        public double Radius => Bounds.Width / 2;

        public Vec2 Position
        {
            get { return new Vec2(Bounds.X, Bounds.Y); }
            set
            {
                Bounds.X = value.X;
                Bounds.Y = value.Y;
            }
        }

        public Vec2 Center
        {
            get
            {
                return new Vec2(
                    Bounds.X + Bounds.Width / 2,
                    Bounds.Y + Bounds.Height / 2
                );
            }
            set
            {
                Bounds.X = value.X - Bounds.Width / 2;
                Bounds.Y = value.Y - Bounds.Height / 2;
            }
        }

        public double CenterX
        {
            get { return Bounds.X + Bounds.Width / 2; }
            set { Bounds.X = value - Bounds.Width / 2; }
        }

        public double CenterY
        {
            get { return Bounds.Y + Bounds.Height / 2; }
            set { Bounds.Y = value - Bounds.Height / 2; }
        }

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

        public double Width
        {
            get { return Bounds.Width; }
            set { Bounds.Width = value; }
        }

        public double Height
        {
            get { return Bounds.Height; }
            set { Bounds.Height = value; }
        }

        // ICloneable implements
        public object Clone()
        {
            return new BallState()
            {
                Bounds = Bounds,
                Velocity = Velocity
            };
        }
    }
}
