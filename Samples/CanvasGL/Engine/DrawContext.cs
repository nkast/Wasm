using System;
using nkast.Wasm.Canvas;
using nkast.Wasm.Canvas.WebGL;

namespace CanvasGL.Engine
{
    public class DrawContext
    {
        public ICanvasRenderingContext CanvasContext;
        public IWebGLRenderingContext GLContext;
        public int Layer;
        public TimeSpan t, dt;

        public DrawContext()
        {
        }
    }
}