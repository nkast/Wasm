using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;
using nkast.Wasm.Canvas.WebGL;

namespace nkast.Wasm.XR
{
    internal class XRWebGLFramebuffer : WebGLFramebuffer
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        internal XRWebGLFramebuffer(int uid, IWebGLRenderingContext glContext)
            : base(uid, glContext)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this, true));

        }

        public static XRWebGLFramebuffer FromUid(int uid)
        {
            if (XRWebGLFramebuffer._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (XRWebGLFramebuffer)jsObj;

            return null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _uidMap.Remove(Uid);

            //base.Dispose(disposing);
        }
    }
}