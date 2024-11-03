using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public abstract class XRLayer : JSObject
    {
        internal XRLayer(int uid) : base(uid)
        {
        }
    }
}