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

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {

                }

                _glContext.DeleteFramebuffer(this);
                _glContext = null;
            }

            base.Dispose(disposing);
        }
    }
}
