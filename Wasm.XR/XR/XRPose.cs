using System;
using System.Numerics;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRPose : JSObject
    {
        private readonly int _fid_GetEmulatedPosition;
        private readonly int _fid_GetAngularVelocity;
        private readonly int _fid_GetLinearVelocity;
        private readonly int _fid_GetTransform;

        public bool EmulatedPosition
        {
            get { return InvokeRetBool(_fid_GetEmulatedPosition); }
        }

        public unsafe Vector4? AngularVelocity
        {
            get
            {
                Vector4 result = default;
                bool valid = InvokeRetBool<IntPtr>(_fid_GetAngularVelocity, new IntPtr(&result));

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
                bool valid = InvokeRetBool<IntPtr>(_fid_GetLinearVelocity, new IntPtr(&result));

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
                Invoke<IntPtr>(_fid_GetTransform, new IntPtr(&result));
                return result;
            }
        }

        internal XRPose(int uid) : base(uid)
        {
            _fid_GetEmulatedPosition = RegisterFunction("nkXRPose.GetEmulatedPosition");
            _fid_GetAngularVelocity = RegisterFunction("nkXRPose.GetAngularVelocity");
            _fid_GetLinearVelocity = RegisterFunction("nkXRPose.GetLinearVelocity");
            _fid_GetTransform = RegisterFunction("nkXRPose.GetTransform");
        }

    }
}