using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRFrame : CachedJSObject<XRFrame>
    {

        public XRSession Session
        {
            get
            {
                int uid = InvokeRet<int>("nkXRFrame.GetSession");
                if (uid == -1)
                    return null;

                XRSession xrSession = XRSession.FromUid(uid);
                if (xrSession != null)
                    return xrSession;

                return new XRSession(uid);
            }
        }

        internal XRFrame(int uid) : base(uid)
        {
        }

        public XRViewerPose GetViewerPose(XRReferenceSpace referenceSpace)
        {
            int uid = InvokeRet<int, int>("nkXRFrame.GetViewerPose", referenceSpace.Uid);
            if (uid == -1)
                return null;

            return new XRViewerPose(uid);
        }

        public XRPose GetPose(XRSpace space, XRSpace baseSpace)
        {
            int uid = InvokeRet<int,int, int>("nkXRFrame.GetPose", space.Uid, baseSpace.Uid);
            if (uid == -1)
                return null;

            return new XRPose(uid);
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
