using System.Runtime.InteropServices;

namespace nkast.Wasm.XR
{
    [StructLayout(LayoutKind.Sequential, Size = 40)]
    public struct XRJointLocation
    {
        public XRRigidTransform Transform;
        public float Radius;
        public bool IsValid;
    }
}
