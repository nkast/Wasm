using System;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGLFramebuffer : CachedJSObject<WebGLFramebuffer>
    {
        private IWebGLRenderingContext _glContext;

        internal protected WebGLFramebuffer(int uid, IWebGLRenderingContext glContext) : base(uid)
        {
            _glContext = (WebGLRenderingContext)glContext;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            ((WebGLRenderingContext)_glContext).DeleteFramebuffer(this);
            _glContext = null;

            base.Dispose(disposing);
        }
    }
}
