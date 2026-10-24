using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUQueue : CachedJSObject<GPUQueue>
    {
        private readonly int _fid_Submit;
        private readonly int _fid_WriteBuffer;
        private readonly int _fid_WriteTexture;

        public GPUDevice _device;

        internal GPUQueue(int uid, GPUDevice device) : base(uid)
        {
            _fid_Submit = RegisterFunction("nkGPUQueue.Submit");
            _fid_WriteBuffer = RegisterFunction("nkGPUQueue.WriteBuffer");
            _fid_WriteTexture = RegisterFunction("nkGPUQueue.WriteTexture");
            _device = device;
        }

        public void Submit(GPUCommandBuffer commandBuffer)
        {
            Invoke<int>(_fid_Submit, commandBuffer.Uid);
        }

        public unsafe void WriteBuffer<TData>(GPUBuffer buffer, long bufferOffset, TData[] data) where TData : unmanaged
        {
            int stride = sizeof(TData);
            fixed (TData* pData = data)
            {
                Invoke(_fid_WriteBuffer, buffer.Uid, (int)bufferOffset, (int)pData, data.Length * stride);
            }
        }

        public unsafe void WriteBuffer<TData>(GPUBuffer buffer, long bufferOffset, TData[] data, int dataOffset, int size) where TData : unmanaged
        {
            if (dataOffset < 0 || size < 0 || dataOffset + size > data.Length)
                throw new ArgumentOutOfRangeException();

            int stride = sizeof(TData);
            fixed (TData* pData = data)
            {
                Invoke(_fid_WriteBuffer, buffer.Uid, (int)bufferOffset, (int)(pData + dataOffset), size * stride);
            }
        }

        public unsafe void WriteBuffer<TData>(GPUBuffer buffer, long bufferOffset, ReadOnlySpan<TData> data) where TData : unmanaged
        {
            int stride = sizeof(TData);
            fixed (TData* pData = data)
            {
                Invoke(_fid_WriteBuffer, buffer.Uid, (int)bufferOffset, (int)pData, data.Length * stride);
            }
        }

        public unsafe void WriteTexture<TData>(
            GPUTextureCopyLocation destination,
            ReadOnlySpan<TData> data,
            GPUTextureDataLayout dataLayout, 
            GPUTextureSize size) where TData : unmanaged
        {
            GPUTextureOrigin origin = destination.Origin;
            fixed (TData* pData = data)
            {
                Invoke(_fid_WriteTexture,
                    destination.Texture.Uid,
                    destination.MipLevel,
                    (int)destination.Aspect,
                    (int)&origin,
                    (int)pData,
                    data.Length * sizeof(TData),
                    (int)dataLayout.Offset,
                    dataLayout.BytesPerRow ?? -1,
                    dataLayout.RowsPerImage ?? -1,
                    (int)&size);
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
