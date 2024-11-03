using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRFrame : JSObject
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        private XRSession _session;

        public XRSession Session
        {
            get { return _session; }
        }

        internal XRFrame(int uid, XRSession session) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this, true));

            this._session = session;
        }

        public static XRFrame FromUid(int uid)
        {
            if (XRFrame._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (XRFrame)jsObj;

            return null;
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

            _uidMap.Remove(Uid);

            base.Dispose(disposing);
        }
    }

}
