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

        public unsafe XRRigidTransform Transform
        {
            get
            {
                XRRigidTransform result = default;
                Invoke<IntPtr>("nkXRPose.GetTransform", new IntPtr(&result));
                return result;
            }
        }

        internal XRPose(int uid) : base(uid)
        {
        }

    }
}