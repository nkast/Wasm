using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUTexture : JSObject
    {
        public GPUDevice _device;

        internal GPUTexture(int uid, GPUDevice device) : base(uid)
        {
            _device = device;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Invoke(RegisterFunction("nkGPUTexture.Destroy"));

            }

            _device = null;

            base.Dispose(disposing);
        }
    }
}
