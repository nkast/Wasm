using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGLUniformLocation : JSObject
    {
        WebGLRenderingContext _glContext;

        internal WebGLUniformLocation(int uid, WebGLRenderingContext glContext) : base(uid)
        {
            _glContext = glContext;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _glContext = null;

            base.Dispose(disposing);
        }
    }
}
