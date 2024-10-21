using System;
using System.Numerics;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRView : JSObject
    {
        public XRView(int uid) : base(uid)
        {
        }

        public XRRigidTransform Transform
        {
            get
            {
                int uid = InvokeRet<int>("nkXRView.GetTransform");
                XRRigidTransform transform = XRRigidTransform.FromUid(uid);
                if (transform != null)
                    return transform;

                return new XRRigidTransform(uid);
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
    }
}