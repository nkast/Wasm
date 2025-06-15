using System;
using System.Numerics;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRPose : JSObject
    {
        public bool EmulatedPosition
        {
            get { return InvokeRetBool("nkXRPose.GetEmulatedPosition"); }
        }

        public unsafe Vector4? AngularVelocity
        {
            get
            {
                Vector4 result = default;
                bool valid = InvokeRetBool<IntPtr>("nkXRPose.GetAngularVelocity", new IntPtr(&result));

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
                bool valid = InvokeRetBool<IntPtr>("nkXRPose.GetLinearVelocity", new IntPtr(&result));

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