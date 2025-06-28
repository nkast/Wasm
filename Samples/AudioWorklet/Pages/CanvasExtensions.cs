using System.Numerics;
using nkast.Wasm.Canvas;

namespace AudioWorklet.Engine
{
    public static class CanvasExtensions
    {
        public static Matrix4x4 GetTransform(this ICanvasRenderingContext cx)
        {
            Matrix4x4 mx = Matrix4x4.Identity;
            cx.GetTransform(ref mx.M11, ref mx.M12, ref mx.M21, ref mx.M22, ref mx.M41, ref mx.M42);
            return mx;
        }

        public static void Translate(this ICanvasRenderingContext cx, Vector2 pos)
        {
            cx.Translate(pos.X, pos.Y);
        }

        public static void MoveTo(this ICanvasRenderingContext cx, Vector2 pos)
        {
            cx.MoveTo(pos.X, pos.Y);
        }

        public static void LineTo(this ICanvasRenderingContext cx, Vector2 pos)
        {
            cx.LineTo(pos.X, pos.Y);
        }
    }
}