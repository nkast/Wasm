using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRInputSource : JSObject
    {
        static internal Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        public XRSpace GripSpace
        {
            get
            {
                int uid = InvokeRet<int>("nkXRInputSource.GetGripSpace");
                if (XRSpace._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                {
                    if (jsObjRef.TryGetTarget(out JSObject jsObj))
                        return (XRSpace)jsObj;
                    else
                        XRSpace._uidMap.Remove(uid);
                }
                return new XRSpace(uid);
            }
        }

        public XRSpace TargetRaySpace
        {
            get
            {
                int uid = InvokeRet<int>("nkXRInputSource.GetTargetRaySpace");
                if (XRSpace._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                {
                    if (jsObjRef.TryGetTarget(out JSObject jsObj))
                        return (XRSpace)jsObj;
                    else
                        XRSpace._uidMap.Remove(uid);
                }
                return new XRSpace(uid);
            }
        }

        public XRHandedness Handedness
        {
            get
            {
                int hand = InvokeRet<int>("nkXRInputSource.GetHandedness");
                return (XRHandedness)hand;
            }
        }

        public XRInputSource(int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this));
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