using PingPongAI.Core.Math;
using System.Diagnostics;

namespace PingPongAI.Core.Physics
{
    [DebuggerDisplay(@"\{Hit={Hit} Time={Time} Normal={Normal}\}")]
    public struct HitInfo
    {
        public bool Hit;
        public double Time;   // 0..1
        public Vec2 Normal;
    }
}
