using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRJointSpace : XRSpace
    {
        public string JointName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        internal XRJointSpace(int uid) : base(uid)
        {
        }
    }
}
