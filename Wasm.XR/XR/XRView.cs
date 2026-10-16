using System;
using System.Collections.Generic;
using System.Numerics;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRView : CachedJSObject<XRView>
    {
        private readonly int _fid_GetTransform;
        private readonly int _fid_GetProjectionMatrix;
        private readonly int _fid_GetEye;


        internal XRView(int uid) : base(uid)
        {
            _fid_GetTransform = RegisterFunction("nkXRView.GetTransform");
            _fid_GetProjectionMatrix = RegisterFunction("nkXRView.GetProjectionMatrix");
            _fid_GetEye = RegisterFunction("nkXRView.GetEye");
        }

        public unsafe XRRigidTransform Transform
        {
            get
            {
                XRRigidTransform result = default;
                Invoke<IntPtr>(_fid_GetTransform, new IntPtr(&result));
                return result;
            }
        }

        public unsafe Matrix4x4 ProjectionMatrix
        {
            get
            {
                Matrix4x4 result = default;
                Invoke<IntPtr>(_fid_GetProjectionMatrix, new IntPtr(&result));
                return result;
            }
        }

        public XREye Eye
        {
            get
            {
                int eye = InvokeRetInt(_fid_GetEye);
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