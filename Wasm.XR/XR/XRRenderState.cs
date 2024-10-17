using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRRenderState : JSObject
    {
        static internal Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        public XRRenderState(int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this));
        }

        public XRWebGLLayer BaseLayer
        {
            get
            {
                int uid = InvokeRet<int>("nkXRRenderState.GetBaseLayer");
                if (XRWebGLLayer._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                {
                    if (jsObjRef.TryGetTarget(out JSObject jsObj))
                        return (XRWebGLLayer)jsObj;
                    else
                        XRWebGLLayer._uidMap.Remove(uid);
                }

                throw new NotImplementedException();
                //return new XRWebGLLayer(uid);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _uidMap.Remove(Uid);

            base.Dispose(disposing);
        }
    }
}