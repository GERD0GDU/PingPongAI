namespace PingPongAI.Core.States
{
    public static class Consts
    {
        public const double BALL_SPEED = 300; // pixel/sec
        public const double BALL_SPEED_MIN_Y = 16; // pixel/sec
        public const double BALL_SPEED_MAX_Y = BALL_SPEED * 1.2; // pixel/sec
        // It should be slightly slower than the ball's speed.
        public const double PADDLE_SPEED = BALL_SPEED * 0.5; // pixel/sec
        public const double MAX_BOUNCE_ANGLE = System.Math.PI / 3.0;
    }
}
