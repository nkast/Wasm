using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUBuffer : JSObject
    {
        public GPUDevice _device;

        internal GPUBuffer(int uid, GPUDevice device) : base(uid)
        {
            _device = device;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Invoke(RegisterFunction("nkGPUBuffer.Destroy"));
            }

            _device = null;

            base.Dispose(disposing);
        }
    }
}
