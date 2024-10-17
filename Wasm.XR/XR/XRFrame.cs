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
        public readonly XRSession Session;

        internal XRFrame(int uid, XRSession session) : base(uid)
        {
            this.Session = session;
        }

        public XRViewerPose GetViewerPose(XRReferenceSpace referenceSpace)
        {
            int uid = InvokeRet<int, int>("nkXRFrame.GetViewerPose", referenceSpace.Uid);

            return new XRViewerPose(uid);
        }

        public XRPose GetPose(XRSpace space, XRSpace baseSpace)
        {
            int uid = InvokeRet<int,int, int>("nkXRFrame.GetPose", space.Uid, baseSpace.Uid);

            return new XRPose(uid);
        }

    }

}
