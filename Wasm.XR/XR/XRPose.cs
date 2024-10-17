using System;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRPose : JSObject
    {
        public bool EmulatedPosition
        {
            get { return InvokeRet<bool>("nkXRPose.GetEmulatedPosition"); }
        }

        public XRRigidTransform Transform
        {
            get
            {
                int uid = InvokeRet<int>("nkXRPose.GetTransform");
                if (XRRigidTransform._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                {
                    if (jsObjRef.TryGetTarget(out JSObject jsObj))
                        return (XRRigidTransform)jsObj;
                    else
                        XRRigidTransform._uidMap.Remove(uid);
                }

                return new XRRigidTransform(uid);
            }
        }

        public XRPose(int uid) : base(uid)
        {
        }
    }
}