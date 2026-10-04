using System;
using System.Runtime.InteropServices;

namespace nkast.Wasm.Canvas.WebGPU
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct GPUColorAttachmentData
    {
        public int ViewUid;
        public GPULoadOpType LoadOp;
        public GPUStoreOpType StoreOp;
        public GPUColor ClearValue;
        public int DepthSlice;
    }
}
