using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRHand : CachedJSObject<XRHand>
        //, IReadOnlyDictionary<string, XRJointSpace>
    {

        internal XRHand(int uid) : base(uid)
        {
        }

        public int Count
        {
            get { return InvokeRet<int>("nkXRHand.GetSize"); }
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