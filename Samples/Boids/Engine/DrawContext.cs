using System;
using nkast.Wasm.Canvas;

namespace Boids.Engine
{
    public class DrawContext
    {
        public ICanvasRenderingContext CanvasContext;
        public int Layer;
        public TimeSpan t, dt;

        public DrawContext()
        {
        }
    }
}