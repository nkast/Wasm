using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRAnchor : CachedJSObject<XRAnchor>
    {

        internal XRAnchor(int uid) : base(uid)
        {
        }

        public unsafe XRSpace AnchorSpace
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private void Delete()
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            this.Delete();

            base.Dispose(disposing);
        }
    }
}