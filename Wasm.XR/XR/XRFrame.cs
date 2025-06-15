using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRFrame : CachedJSObject<XRFrame>
    {

        public XRSession Session
        {
            get
            {
                int uid = InvokeRetInt("nkXRFrame.GetSession");
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
            int uid = InvokeRetInt<int>("nkXRFrame.GetViewerPose", referenceSpace.Uid);
            if (uid == -1)
                return null;

            return new XRViewerPose(uid);
        }

        public XRPose GetPose(XRSpace space, XRSpace baseSpace)
        {
            int uid = InvokeRetInt<int, int>("nkXRFrame.GetPose", space.Uid, baseSpace.Uid);
            if (uid == -1)
                return null;

            return new XRPose(uid);
        }

        public XRJointPose GetJointPose(XRJointSpace space, XRSpace baseSpace)
        {
            int uid = InvokeRetInt<int, int>("nkXRFrame.GetJointPose", space.Uid, baseSpace.Uid);
            if (uid == -1)
                return null;

            return new XRJointPose(uid);
        }

        public Task<XRAnchor> CreateAnchor(XRRigidTransform pose, XRSpace baseSpace)
        {
            throw new NotImplementedException();
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
