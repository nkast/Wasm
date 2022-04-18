using System;

namespace nkast.Wasm.Canvas.WebGL
{
    public enum WebGLStencilOpFunc
    {
        ZERO        = 0x0000,
        INVERT      = 0x150A,
        KEEP        = 0x1E00,
        REPLACE     = 0x1E01,
        INCR        = 0x1E02,
        DECR        = 0x1E03,
        INCR_WRAP   = 0x8507,
        DECR_WRAP   = 0x8508,
    }
}
