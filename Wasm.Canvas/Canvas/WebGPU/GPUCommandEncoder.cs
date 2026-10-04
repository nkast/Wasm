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

        public unsafe GPURenderPassEncoder BeginRenderPass(GPURenderPassDescriptor descriptor)
        {
            GPUColorAttachment[] colorAttachments = descriptor.ColorAttachments;
            GPUColorAttachmentData[] data = new GPUColorAttachmentData[colorAttachments.Length];
            for (int i = 0; i < colorAttachments.Length; i++)
            {
                data[i].ViewUid = colorAttachments[i].View.Uid;
                data[i].LoadOp = colorAttachments[i].LoadOp;
                data[i].StoreOp = colorAttachments[i].StoreOp;
                data[i].ClearValue = colorAttachments[i].ClearValue.GetValueOrDefault();
                data[i].DepthSlice = colorAttachments[i].DepthSlice ?? -1;
            }

            int uid;
            fixed (GPUColorAttachmentData* pData = data)
            {
                uid = InvokeRetInt(
                    RegisterFunction("nkGPUCommandEncoder.BeginRenderPass"),
                    (int)(descriptor.MaxDrawCount ?? -1),
                    (int)pData,
                    colorAttachments.Length);
            }
            return new GPURenderPassEncoder(uid, _device);
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
