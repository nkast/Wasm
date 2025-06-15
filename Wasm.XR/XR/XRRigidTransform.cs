using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct XRRigidTransform
    {
        public Quaternion Orientation;
        public Vector4    Position;

        public unsafe Matrix4x4 Matrix
        {
            get
            {
                Matrix4x4 result = Matrix4x4.CreateFromQuaternion(Orientation);
                result.Translation = new Vector3(Position.X, Position.Y, Position.Z);
                return result;
            }
        }

        public XRRigidTransform Inverse
        {
            get
            {
                XRRigidTransform invTransform = default;
                invTransform.Orientation = Quaternion.Inverse(Orientation);
                invTransform.Position    = -Vector4.Transform(Position, invTransform.Orientation);
                return invTransform;
            }
        }

        public XRRigidTransform(Quaternion orientation, Vector4 position)
        {
            this.Orientation = orientation;
            this.Position = position;
        }
    }
}