using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public abstract class XRLayer : CachedJSObject<XRLayer>
    {
        internal XRLayer(int uid) : base(uid)
        {
        }
    }
}