using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPURenderPassEncoder : JSObject
    {
        public GPUDevice _device;

        internal GPURenderPassEncoder(int uid, GPUDevice device) : base(uid)
        {
            _device = device;
        }

        public void SetVertexBuffer(int slot, GPUBuffer buffer)
        {
            Invoke(RegisterFunction("nkGPURenderPassEncoder.SetVertexBuffer"), slot, buffer.Uid);
        }

        public void SetVertexBuffer(int slot, GPUBuffer buffer, long offset, long size)
        {
            Invoke(RegisterFunction("nkGPURenderPassEncoder.SetVertexBuffer1"), slot, buffer.Uid, (int)offset, (int)size);
        }

        public void End()
        {
            Invoke(RegisterFunction("nkGPURenderPassEncoder.End"));
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
