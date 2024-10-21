using System;

namespace nkast.Wasm.Canvas.WebGL
{
    public enum WebGLTexelType
    {
        UNSIGNED_BYTE           = 0x1401,
        UNSIGNED_SHORT_5_6_5    = 0x8363,
        UNSIGNED_SHORT_4_4_4_4  = 0x8033,
        UNSIGNED_SHORT_5_5_5_1  = 0x8034,


        // WebGL2
        FLOAT = 0x1406,
        HALF_FLOAT = 0x140B,
    }
}
