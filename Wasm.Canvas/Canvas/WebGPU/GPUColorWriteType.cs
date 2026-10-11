using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    [Flags]
    public enum GPUColorWriteType
    {
        Red   = 0x1,
        Green = 0x2,
        Blue  = 0x4,
        Alpha = 0x8,
        All   = 0xF,
    }
}
