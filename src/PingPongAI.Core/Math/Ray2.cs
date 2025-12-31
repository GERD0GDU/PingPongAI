using System.Diagnostics;

namespace PingPongAI.Core.Math
{
    [DebuggerDisplay(@"\{Origin={Origin} Direction={Direction}\}")]
    public struct Ray2
    {
        public Vec2 Origin;
        public Vec2 Direction;
    }
}
