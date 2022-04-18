using System;
using System.Numerics;
using nkast.Wasm.Canvas;


namespace Boids.Engine
{
    public class UpdateContext
    {
        public TimeSpan t, dt;
        internal Matrix4x4 tx;
        public MouseState CurrMouseState;
        public MouseState PrevMouseState;
        public TouchState CurrTouchState;
        public TouchState PrevTouchState;

        public UpdateContext(TimeSpan t, TimeSpan dt, MouseState currMouseState, MouseState prevMouseState, TouchState currTouchState, TouchState prevTouchState)
        {
            this.t = t;
            this.dt = dt;
            this.tx = Matrix4x4.Identity;
            this.CurrMouseState = currMouseState;
            this.PrevMouseState = prevMouseState;
            this.CurrTouchState = currTouchState;
            this.PrevTouchState = prevTouchState;
        }

        public Vector2 ToLocal(Vector2 mousePos)
        {
            Matrix4x4 invtx;
            Matrix4x4.Invert(tx, out invtx);
            return Vector2.Transform(mousePos, invtx);
        }
    }
}