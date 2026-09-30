using System;
using System.Collections.Generic;
using System.Numerics;
using nkast.Wasm.JSInterop;

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
                Invoke<IntPtr>(RegisterFunction("nkXRView.GetTransform"), new IntPtr(&result));
                return result;
            }
        }

        public unsafe Matrix4x4 ProjectionMatrix
        {
            get
            {
                Matrix4x4 result = default;
                Invoke<IntPtr>(RegisterFunction("nkXRView.GetProjectionMatrix"), new IntPtr(&result));
                return result;
            }
        }

        public XREye Eye
        {
            get
            {
                int eye = InvokeRetInt(RegisterFunction("nkXRView.GetEye"));
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