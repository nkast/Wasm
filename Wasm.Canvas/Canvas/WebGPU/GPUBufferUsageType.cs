using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    [Flags]
    public enum GPUBufferUsageType
    {
        MapRead      =   1,
        MapWrite     =   2,
        CopySrc      =   4,
        CopyDst      =   8,
        Index        =  16,
        Vertex       =  32,
        Uniform      =  64,
        Storage      = 128,
        Indirect     = 256,
        QueryResolve = 512,
    }

}
