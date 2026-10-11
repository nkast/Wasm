using System;
using System.Runtime.InteropServices;

namespace nkast.Wasm.Canvas.WebGPU
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct GPUVertexAttribute
    {
        public GPUVertexFormatType Format;
        public long Offset;
        public int ShaderLocation;
    }
}
