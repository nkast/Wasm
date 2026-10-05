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

        public unsafe void WriteBuffer<TData>(GPUBuffer buffer, long bufferOffset, TData[] data) where TData : unmanaged
        {
            int stride = sizeof(TData);
            fixed (TData* pData = data)
            {
                Invoke(RegisterFunction("nkGPUQueue.WriteBuffer"), buffer.Uid, (int)bufferOffset, (int)pData, data.Length * stride);
            }
        }

        public unsafe void WriteBuffer<TData>(GPUBuffer buffer, long bufferOffset, TData[] data, int dataOffset, int size) where TData : unmanaged
        {
            if (dataOffset < 0 || size < 0 || dataOffset + size > data.Length)
                throw new ArgumentOutOfRangeException();

            int stride = sizeof(TData);
            fixed (TData* pData = data)
            {
                Invoke(RegisterFunction("nkGPUQueue.WriteBuffer"), buffer.Uid, (int)bufferOffset, (int)(pData + dataOffset), size * stride);
            }
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
