using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRFrame : CachedJSObject<XRFrame>
    {
        private readonly int _fid_GetSession;
        private readonly int _fid_GetViewerPose;
        private readonly int _fid_GetPose;
        private readonly int _fid_GetJointPose;
        private readonly int _fid_CreateAnchor;


        public XRSession Session
        {
            get
            {
                int uid = InvokeRetInt(_fid_GetSession);
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
            _fid_GetSession = RegisterFunction("nkXRFrame.GetSession");
            _fid_GetViewerPose = RegisterFunction("nkXRFrame.GetViewerPose");
            _fid_GetPose = RegisterFunction("nkXRFrame.GetPose");
            _fid_GetJointPose = RegisterFunction("nkXRFrame.GetJointPose");
            _fid_CreateAnchor = RegisterFunction("nkXRFrame.CreateAnchor");
        }

        public XRViewerPose GetViewerPose(XRReferenceSpace referenceSpace)
        {
            int uid = InvokeRetInt<int>(_fid_GetViewerPose, referenceSpace.Uid);
            if (uid == -1)
                return null;

            return new XRViewerPose(uid);
        }

        public XRPose GetPose(XRSpace space, XRSpace baseSpace)
        {
            int uid = InvokeRetInt<int, int>(_fid_GetPose, space.Uid, baseSpace.Uid);
            if (uid == -1)
                return null;

            return new XRPose(uid);
        }

        public XRJointPose GetJointPose(XRJointSpace space, XRSpace baseSpace)
        {
            int uid = InvokeRetInt<int, int>(_fid_GetJointPose, space.Uid, baseSpace.Uid);
            if (uid == -1)
                return null;

            return new XRJointPose(uid);
        }

        public unsafe Task<XRAnchor> CreateAnchorAsync(XRRigidTransform pose, XRSpace baseSpace)
        {
            int uid = InvokeRetInt<IntPtr, int>(_fid_CreateAnchor, new IntPtr(&pose), baseSpace.Uid);

            PromiseJSObject<XRAnchor> promise = new PromiseJSObject<XRAnchor>(uid,
                (int newuid) =>
                {
                    return new XRAnchor(newuid);
                });
            return promise.GetTask();
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
