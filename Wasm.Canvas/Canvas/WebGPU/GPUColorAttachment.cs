using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public struct GPUColorAttachment
    {
        public GPUTextureView View;
        public GPULoadOpType LoadOp;
        public GPUStoreOpType StoreOp;
        public GPUColor ClearValue;
        public int? DepthSlice;
    }
}
