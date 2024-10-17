using System;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGLFramebuffer : JSObject
    {
        WebGLRenderingContext _glContext;

        internal WebGLFramebuffer(int uid, WebGLRenderingContext glContext) : base(uid)
        {
            _glContext = glContext;
        }

        public static WebGLFramebuffer FromUid(int uid, IWebGLRenderingContext glContext)
        {
            return new WebGLFramebuffer(uid, (WebGLRenderingContext)glContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _glContext.DeleteFramebuffer(this);
            _glContext = null;

            base.Dispose(disposing);
        }
    }
}
