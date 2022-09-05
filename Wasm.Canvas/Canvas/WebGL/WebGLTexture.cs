using System;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGLTexture : JSObject
    {
        WebGLRenderingContext _glContext;


        internal WebGLTexture(int uid, WebGLRenderingContext glContext) : base(uid)
        {
            _glContext = glContext;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _glContext.DeleteTexture(this);
            _glContext = null;

            base.Dispose(disposing);
        }
    }
}
