using System;

namespace nkast.Wasm.Canvas.WebGL
{
    public enum WebGLCapability
    {
        BLEND           = 0x0BE2,

        CULL_FACE       = 0x0B44,
        DEPTH_TEST      = 0x0B71,
        STENCIL_TEST    = 0x0B90,
        SCISSOR_TEST    = 0x0C11,

        DITHER          = 0x0BD0,

        POLYGON_OFFSET_FILL = 0x8037,
    }
}
