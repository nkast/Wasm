using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRJointPose : XRPose
    {
        private readonly int _fid_GetRadius;

        public float Radius
        {
            get { return InvokeRetFloat(_fid_GetRadius); }
        }

        internal XRJointPose(int uid) : base(uid)
        {
            _fid_GetRadius = RegisterFunction("XRJointPose.GetRadius");
        }
    }
}
