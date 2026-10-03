using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRAnchor : CachedJSObject<XRAnchor>
    {
        private readonly int _fid_GetAnchorSpace;
        private readonly int _fid_Delete;


        internal XRAnchor(int uid) : base(uid)
        {
            _fid_GetAnchorSpace = RegisterFunction("nkXRAnchor.GetAnchorSpace");
            _fid_Delete = RegisterFunction("nkXRAnchor.Delete");
        }

        public XRSpace AnchorSpace
        {
            get
            {
                int uid = InvokeRetInt(_fid_GetAnchorSpace);
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
            Invoke(_fid_Delete);
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