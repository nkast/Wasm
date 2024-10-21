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
                XRRigidTransform transform = XRRigidTransform.FromUid(uid);
                if (transform != null)
                    return transform;

                return new XRRigidTransform(uid);
            }
        }

        public XRPose(int uid) : base(uid)
        {
        }

    }
}