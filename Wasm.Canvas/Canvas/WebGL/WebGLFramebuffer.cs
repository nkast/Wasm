using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGLFramebuffer : JSObject
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        private IWebGLRenderingContext _glContext;

        internal protected WebGLFramebuffer(int uid, IWebGLRenderingContext glContext) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this, true));

            _glContext = (WebGLRenderingContext)glContext;
        }

        public static WebGLFramebuffer FromUid(int uid)
        {
            if (WebGLFramebuffer._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (WebGLFramebuffer)jsObj;

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _uidMap.Remove(Uid);

            ((WebGLRenderingContext)_glContext).DeleteFramebuffer(this);
            _glContext = null;

            base.Dispose(disposing);
        }
    }
}
