using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUVertexBufferLayout : CachedJSObject<GPUVertexBufferLayout>
    {

        public GPUVertexBufferLayout() : base(Register())
        {
        }

        internal GPUVertexBufferLayout(int uid) : base(uid)
        {
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUVertexBufferLayout.Create"));
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
