using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRAnchor : CachedJSObject<XRAnchor>
    {

        internal XRAnchor(int uid) : base(uid)
        {
        }

        public XRSpace AnchorSpace
        {
            get
            {
                int uid = InvokeRetInt("nkXRAnchor.GetAnchorSpace");
                if (uid == -1)
                    return null;

                XRSpace space = XRSpace.FromUid(uid);
                if (space != null)
                    return space;

                return new XRSpace(uid);
            }
        }

        private void Delete()
        {
            Invoke("nkXRAnchor.Delete");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Delete();
            }

            base.Dispose(disposing);
        }
    }
}