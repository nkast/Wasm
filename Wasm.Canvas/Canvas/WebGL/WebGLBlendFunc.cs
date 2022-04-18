using System;

namespace nkast.Wasm.Canvas.WebGL
{
    public enum WebGLBlendFunc
    {
        ZERO                        = 0x0000,
        ONE                         = 0x0001,

        SRC_COLOR                   = 0x0300,
        ONE_MINUS_SRC_COLOR         = 0x0301,
        SRC_ALPHA                   = 0x0302,
        ONE_MINUS_SRC_ALPHA         = 0x0303,
        DST_ALPHA                   = 0x0304,
        ONE_MINUS_DST_ALPHA         = 0x0305,
        DST_COLOR                   = 0x0306,
        ONE_MINUS_DST_COLOR         = 0x0307,
        SRC_ALPHA_SATURATE          = 0x0308,

        CONSTANT_COLOR              = 0x8001,
        ONE_MINUS_CONSTANT_COLOR    = 0x8002,
        CONSTANT_ALPHA              = 0x8003,
        ONE_MINUS_CONSTANT_ALPHA    = 0x8004,
    }
}
