using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas.WebGL
{
    internal class WebGL2RenderingContext : WebGLRenderingContext, IWebGL2RenderingContext, IDisposable
    {
        internal WebGL2RenderingContext(Canvas canvas, int uid) : base(canvas, uid)
        {
        }

        public void DrawRangeElements(WebGLPrimitiveType mode, int start, int end, int count, WebGLDataType type, int offset)
        {
            //Invoke("nkCanvasGLContext.DrawElements", (int)mode, count, (int)type, offset);
            Invoke("nkCanvasGL2Context.DrawRangeElements", (int)mode, start, end, count, (int)type, offset);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            //Invoke("nkCanvasGLContext.DisposeObject"); // DisposeWebGLContext
            base.Dispose(disposing);
        }

    }
}
