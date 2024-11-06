using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRRigidTransform : CachedJSObject<XRRigidTransform>
    {

        internal XRRigidTransform(int uid) : base(uid)
        {
        }

        public unsafe Quaternion Orientation
        {
            get
            {
                Quaternion result = default;
                Invoke<IntPtr>("nkXRRigidTransform.GetOrientation", new IntPtr(&result));
                return result;
            }
        }

        public unsafe Vector4 Position
        {
            get
            {
                Vector4 result = default;
                Invoke<IntPtr>("nkXRRigidTransform.GetPosition", new IntPtr(&result));
                return result;
            }
        }

        public unsafe Matrix4x4 Matrix
        {
            get
            {
                Matrix4x4 result = default;
                Invoke<IntPtr>("nkXRRigidTransform.GetMatrix", new IntPtr(&result));
                return result;
            }
        }

        public XRRigidTransform Inverse
        {
            get
            {
                int uid = InvokeRet<int>("nkXRRigidTransform.GetInverse");
                XRRigidTransform invTransform = XRRigidTransform.FromUid(uid);
                if (invTransform != null)
                    return invTransform;

                return new XRRigidTransform(uid);
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