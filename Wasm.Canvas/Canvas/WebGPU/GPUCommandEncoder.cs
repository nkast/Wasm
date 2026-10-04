using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUCommandEncoder : JSObject
    {
        public GPUDevice _device;

        internal GPUCommandEncoder(int uid, GPUDevice device) : base(uid)
        {
            _device = device;
        }

        public GPUCommandBuffer Finish()
        {
            int uid = InvokeRetInt(RegisterFunction("nkGPUCommandEncoder.Finish"));
            return new GPUCommandBuffer(uid, _device);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _device = null;

            base.Dispose(disposing);
        }
    }
}
