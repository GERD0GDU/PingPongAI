using PingPongAI.Core.Math;
using System;

namespace PingPongAI.Core.States
{
    public sealed class PaddleState : ICloneable
    {
        public Rect2 Bounds;
        public int Direction;
        public double Velocity;
        public bool HasHitBall;

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
                Direction = Direction,
                Velocity = Velocity,
                HasHitBall = HasHitBall
            };
        }
    }
}
