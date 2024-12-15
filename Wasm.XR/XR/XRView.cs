using System;
using System.Collections.Generic;
using System.Numerics;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRView : CachedJSObject<XRView>
    {

        internal XRView(int uid) : base(uid)
        {
        }

        public unsafe XRRigidTransform Transform
        {
            get
            {
                XRRigidTransform result = default;
                Invoke<IntPtr>("nkXRView.GetTransform", new IntPtr(&result));
                return result;
            }
        }

        public unsafe Matrix4x4 ProjectionMatrix
        {
            get
            {
                Matrix4x4 result = default;
                Invoke<IntPtr>("nkXRView.GetProjectionMatrix", new IntPtr(&result));
                return result;
            }
        }

        public XREye Eye
        {
            get
            {
                int eye = InvokeRet<int>("nkXRView.GetEye");
                return (XREye)eye;
            }
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