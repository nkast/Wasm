using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGLRenderbuffer : JSObject
    {
        WebGLRenderingContext _glContext;

        internal WebGLRenderbuffer(int uid, WebGLRenderingContext glContext) : base(uid)
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

                _glContext.DeleteRenderbuffer(this);
                _glContext = null;
            }

            base.Dispose(disposing);
        }
    }
}
