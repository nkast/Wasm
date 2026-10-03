using System;
using System.Threading.Tasks;
using nkast.Wasm.Dom;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPU : CachedJSObject<GPU>
    {
        public static GPU FromNavigator(Navigator navigator)
        {
            int uid = JSObject.StaticInvokeRetInt(RegisterFunction("nkGPU.Create"), navigator.Uid);
            if (uid == -1)
                return null;

            GPU gpu = GPU.FromUid(uid);
            if (gpu != null)
                return gpu;

            return new GPU(navigator, uid);
        }

        internal GPU(Navigator navigator, int uid) : base(uid)
        {
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
