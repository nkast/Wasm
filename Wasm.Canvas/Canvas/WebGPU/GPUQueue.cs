using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUQueue : CachedJSObject<GPUQueue>
    {
        public GPUDevice _device;

        internal GPUQueue(int uid, GPUDevice device) : base(uid)
        {
            _device = device;
        }

        public void Submit(GPUCommandBuffer commandBuffer)
        {
            Invoke<int>(RegisterFunction("nkGPUQueue.Submit"), commandBuffer.Uid);
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
