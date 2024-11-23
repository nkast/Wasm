using System;

namespace nkast.Wasm.Canvas.WebGL
{
    public enum WebGLInternalFormat
    {
        ALPHA               = 0x1906,
        RGB                 = 0x1907,
        RGBA                = 0x1908,
        LUMINANCE           = 0x1909,
        LUMINANCE_ALPHA     = 0x190A,

        // WEBGL_compressed_texture_s3tc
        COMPRESSED_RGB_S3TC_DXT1_EXT  = 0x83F0,
        COMPRESSED_RGBA_S3TC_DXT1_EXT = 0x83F1,
        COMPRESSED_RGBA_S3TC_DXT3_EXT = 0x83F2,
        COMPRESSED_RGBA_S3TC_DXT5_EXT = 0x83F3,

        // WEBGL_compressed_texture_s3tc_srgb extension
        COMPRESSED_SRGB_S3TC_DXT1_EXT       = 0x8C4C,
        COMPRESSED_SRGB_ALPHA_S3TC_DXT1_EXT = 0x8C4D,
        COMPRESSED_SRGB_ALPHA_S3TC_DXT3_EXT = 0x8C4E,
        COMPRESSED_SRGB_ALPHA_S3TC_DXT5_EXT = 0x8C4F,


        // WebGL2
        R8      = 0x8229,
        RG8     = 0x822B,
        RGB8    = 0x8051,
        RGBA8   = 0x8058,
        RGBA4   = 0x8056,
        RGB5_A1 = 0x8057,
        RGB565  = 0x8D62,

        R32F    = 0x822E,
        RG32F   = 0x8230,
        RGBA32F = 0x8814,

        R16F    = 0x822D,
        RG16F   = 0x822F,
        RGBA16F = 0x881A,
    }
}
