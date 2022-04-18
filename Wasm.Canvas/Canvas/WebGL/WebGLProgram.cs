using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGLProgram : JSObject
    {
        WebGLRenderingContext _glContext;

        internal WebGLProgram(int uid, WebGLRenderingContext glContext) : base(uid)
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

                _glContext.DeleteProgram(this);
                _glContext = null;
            }

            base.Dispose(disposing);
        }
    }
}