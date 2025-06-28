using System.Numerics;

namespace AudioWorklet.Engine
{
    public struct MouseState
    {
        public Vector2 Position;
        internal bool LeftButton;
        internal float Wheel;
    }
}
