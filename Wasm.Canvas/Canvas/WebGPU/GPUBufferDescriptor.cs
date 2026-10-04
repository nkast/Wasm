using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public struct GPUBufferDescriptor
    {
        public long Size;
        public GPUBufferUsageType Usage;
        public bool MappedAtCreation;
    }
}
