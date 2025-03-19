using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Dom;

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
