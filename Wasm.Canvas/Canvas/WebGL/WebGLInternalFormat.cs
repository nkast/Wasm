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
        R32F    = 0x822E,
        RG32F   = 0x8230,
        RGBA32F = 0x8814,
    }
}
