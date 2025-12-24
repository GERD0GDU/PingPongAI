using System.Windows;

namespace PingPongAI.Core
{
    public struct HitInfo
    {
        public bool Hit;
        public double Time;   // 0..1
        public Vector Normal; // sekme yönü
    }
}
