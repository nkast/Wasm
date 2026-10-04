using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public struct GPURenderPassDescriptor
    {
        public int? MaxDrawCount;
        public GPUColorAttachment[] ColorAttachments;
    }
}
