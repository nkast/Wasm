using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGLBuffer : JSObject
    {
        WebGLRenderingContext _glContext;

        internal WebGLBuffer(int uid, WebGLRenderingContext glContext) : base(uid)
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

                _glContext.DeleteBuffer(this);
                _glContext = null;
            }

            base.Dispose(disposing);
        }
    }
}