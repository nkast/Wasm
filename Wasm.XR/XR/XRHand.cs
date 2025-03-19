using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRHand : CachedJSObject<XRHand>
        //, IDictionary<string, XRJointSpace>
    {

        internal XRHand(int uid) : base(uid)
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