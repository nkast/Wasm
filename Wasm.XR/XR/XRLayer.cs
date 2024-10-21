using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public abstract class XRLayer : JSObject
    {
        public XRLayer(int uid) : base(uid)
        {
        }
    }
}