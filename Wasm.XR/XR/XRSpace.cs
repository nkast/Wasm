using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRSpace : CachedJSObject<XRSpace>
    {

        internal XRSpace(int uid) : base(uid)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            base.Dispose(disposing);
        }
    }
}