using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGL2Query : CachedJSObject<WebGL2Query>
    {
        IWebGL2RenderingContext _glContext;

        internal WebGL2Query(int uid, WebGL2RenderingContext glContext) : base(uid)
        {
            _glContext = glContext;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            if (_glContext != null)
            {
                ((WebGL2RenderingContext)_glContext).DeleteQuery(this);
                _glContext = null;
            }

            base.Dispose(disposing);
        }
    }
}
