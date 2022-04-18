using System;

namespace nkast.Wasm.Canvas.WebGL
{
    public enum WebGLTexParamName
    {
        TEXTURE_MAG_FILTER      = 0x2800,
        TEXTURE_MIN_FILTER      = 0x2801,
        TEXTURE_WRAP_S          = 0x2802,
        TEXTURE_WRAP_T          = 0x2803,
    }

    public enum WebGLTexParam
    {
        // filter
        NEAREST                 = 0x2600,
        LINEAR                  = 0x2601,
        NEAREST_MIPMAP_NEAREST  = 0x2700,
        LINEAR_MIPMAP_NEAREST   = 0x2701, 
        NEAREST_MIPMAP_LINEAR   = 0x2702, 
        LINEAR_MIPMAP_LINEAR    = 0x2703,

        // Wrap
        CLAMP_TO_EDGE           = 0x812F,
        MIRRORED_REPEAT         = 0x8370,
        REPEAT                  = 0x2901,
    }

}
