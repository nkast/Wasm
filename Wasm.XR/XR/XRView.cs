using System;
using System.Collections.Generic;
using System.Numerics;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRView : JSObject
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        public XRView(int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this, true));
        }

        public static XRView FromUid(int uid)
        {
            if (XRView._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (XRView)jsObj;

            return null;
        }

        public XRRigidTransform Transform
        {
            get
            {
                int uid = InvokeRet<int>("nkXRView.GetTransform");
                XRRigidTransform transform = XRRigidTransform.FromUid(uid);
                if (transform != null)
                    return transform;

                return new XRRigidTransform(uid);
            }
        }

        public unsafe Matrix4x4 ProjectionMatrix
        {
            get
            {
                Matrix4x4 result = default;
                Invoke<IntPtr>("nkXRView.GetProjectionMatrix", new IntPtr(&result));
                return result;
            }
        }

        public XREye Eye
        {
            get
            {
                int eye = InvokeRet<int>("nkXRView.GetEye");
                return (XREye)eye;
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