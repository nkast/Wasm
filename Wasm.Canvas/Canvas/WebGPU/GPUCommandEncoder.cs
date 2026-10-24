using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUCommandEncoder : JSObject
    {
        private readonly int _fid_BeginRenderPass;
        private readonly int _fid_Finish;

        public GPUDevice _device;

        internal GPUCommandEncoder(int uid, GPUDevice device) : base(uid)
        {
            _fid_BeginRenderPass = RegisterFunction("nkGPUCommandEncoder.BeginRenderPass");
            _fid_Finish = RegisterFunction("nkGPUCommandEncoder.Finish");
            _device = device;
        }

        public GPURenderPassEncoder BeginRenderPass(GPURenderPassDescriptor descriptor)
        {
            int uid = InvokeRetInt(
                _fid_BeginRenderPass,
                descriptor.Uid);
            return new GPURenderPassEncoder(uid, _device);
        }

        public GPUCommandBuffer Finish()
        {
            int uid = InvokeRetInt(_fid_Finish);
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
