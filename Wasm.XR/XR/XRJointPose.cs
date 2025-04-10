﻿using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRJointPose : XRPose
    {
        public float Radius
        {
            get { return InvokeRetFloat("XRJointPose.GetRadius"); }
        }

        internal XRJointPose(int uid) : base(uid)
        {
        }
    }
}
