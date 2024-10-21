using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRSpace : JSObject
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        public XRSpace(int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this));
        }

        public static XRSpace FromUid(int uid)
        {
            if (XRSpace._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
            {
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (XRSpace)jsObj;
                else
                    XRSpace._uidMap.Remove(uid);
            }

            return null;
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