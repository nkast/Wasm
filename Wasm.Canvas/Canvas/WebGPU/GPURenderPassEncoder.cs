using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPURenderPassEncoder : JSObject
    {
        private readonly int _fid_SetVertexBuffer;
        private readonly int _fid_SetVertexBuffer1;
        private readonly int _fid_End;
        private readonly int _fid_SetPipeline;
        private readonly int _fid_SetBindGroup;
        private readonly int _fid_Draw;

        public GPUDevice _device;

        internal GPURenderPassEncoder(int uid, GPUDevice device) : base(uid)
        {
            _fid_SetVertexBuffer = RegisterFunction("nkGPURenderPassEncoder.SetVertexBuffer");
            _fid_SetVertexBuffer1 = RegisterFunction("nkGPURenderPassEncoder.SetVertexBuffer1");
            _fid_End = RegisterFunction("nkGPURenderPassEncoder.End");
            _fid_SetPipeline = RegisterFunction("nkGPURenderPassEncoder.SetPipeline");
            _fid_SetBindGroup = RegisterFunction("nkGPURenderPassEncoder.SetBindGroup");
            _fid_Draw = RegisterFunction("nkGPURenderPassEncoder.Draw");
            _device = device;
        }

        public void SetVertexBuffer(int slot, GPUBuffer buffer)
        {
            Invoke(_fid_SetVertexBuffer, slot, buffer.Uid);
        }

        public void SetVertexBuffer(int slot, GPUBuffer buffer, long offset, long size)
        {
            Invoke(_fid_SetVertexBuffer1, slot, buffer.Uid, (int)offset, (int)size);
        }

        public void End()
        {
            Invoke(_fid_End);
        }

        public void SetPipeline(GPURenderPipeline pipeline)
        {
            Invoke<int>(_fid_SetPipeline, pipeline.Uid);
        }

        public void SetBindGroup(int index, GPUBindGroup bindGroup)
        {
            Invoke(_fid_SetBindGroup, index, bindGroup.Uid);
        }

        public void Draw(int vertexCount, int instanceCount = 1, int firstVertex = 0, int firstInstance = 0)
        {
            Invoke(_fid_Draw, vertexCount, instanceCount, firstVertex, firstInstance);
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
