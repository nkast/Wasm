using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public enum GPUCompareFunctionType
    {
        Never        = 1,
        Less         = 2,
        Equal        = 3,
        LessEqual    = 4,
        Greater      = 5,
        NotEqual     = 6,
        GreaterEqual = 7,
        Always       = 8,
    }
}