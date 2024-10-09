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

        //public XRPose
        public object GetPose(XRReferenceSpace referenceSpace)
        {
            int uid = InvokeRet<int, int>("nkXRFrame.GetPose", referenceSpace.Uid);


            return null;
        }

        //public XRViewerPose
        public object  GetViewerPose(XRReferenceSpace referenceSpace)
        {
            int uid = InvokeRet<int, int>("nkXRFrame.GetViewerPose", referenceSpace.Uid);


            return null;
        }
    }

}
