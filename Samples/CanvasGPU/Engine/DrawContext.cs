using System;
using System.Numerics;
using nkast.Wasm.Canvas;

namespace CanvasGPU.Engine
{
    public class DrawContext
    {
        public ICanvasRenderingContext CanvasContext;
        public int Layer;
        public TimeSpan t, dt;

        public Matrix4x4 world;
        public Matrix4x4 view;
        public Matrix4x4 proj;

        public DrawContext()
        {
        }
    }
}