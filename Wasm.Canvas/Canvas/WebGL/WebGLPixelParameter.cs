using System;

namespace nkast.Wasm.Canvas.WebGL
{
    public enum WebGLPixelParameter
    {
        // ES 2.0
        UNPACK_ALIGNMENT                    = 0x0CF5,
        PACK_ALIGNMENT                      = 0x0D05,

        // WebGL
        UNPACK_FLIP_Y_WEBGL                 = 0x9240,
        UNPACK_PREMULTIPLY_ALPHA_WEBGL      = 0x9241,
        UNPACK_COLORSPACE_CONVERSION_WEBGL  = 0x9243,
    }
}
