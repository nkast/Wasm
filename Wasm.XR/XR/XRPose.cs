using System;
using System.Numerics;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRPose : JSObject
    {
        public bool EmulatedPosition
        {
            get { return InvokeRet<bool>("nkXRPose.GetEmulatedPosition"); }
        }

        public unsafe Vector4? AngularVelocity
        {
            get
            {
                Vector4 result = default;
                bool valid = InvokeRet<IntPtr, bool>("nkXRPose.GetAngularVelocity", new IntPtr(&result));

                if (valid)
                    return result;
                else
                    return null;
            }
        }

        public unsafe Vector4? LinearVelocity
        {
            get
            {
                Vector4 result = default;
                bool valid = InvokeRet<IntPtr, bool>("nkXRPose.GetLinearVelocity", new IntPtr(&result));

                if (valid)
                    return result;
                else
                    return null;
            }
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

        internal XRPose(int uid) : base(uid)
        {
        }

    }
}