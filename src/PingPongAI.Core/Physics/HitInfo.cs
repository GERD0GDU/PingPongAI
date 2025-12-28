using PingPongAI.Core.Math;

namespace PingPongAI.Core.Physics
{
    public struct HitInfo
    {
        public bool Hit;
        public double Time;   // 0..1
        public Vec2 Normal;
    }
}
