using System;

namespace nkast.Wasm.Canvas.WebGL
{
    public enum WebGL2RenderbufferInternalFormat
    {
        DEPTH_COMPONENT16   = 0x81A5,
        STENCIL_INDEX8      = 0x8D48,
        DEPTH_STENCIL       = 0x84F9,

        RGBA4   = 0x8056,
        RGB5_A1 = 0x8057,
        RGB565  = 0x8D62,
        /// <remarks>WebGL1 Extension: EXT_sRGB</remarks>
        SRGB8_ALPHA8_EXT = 0x8C43,


        // WEBGL2
        DEPTH_COMPONENT24 = 0x81A6,
        DEPTH24_STENCIL8 = 0x88F0,
        DEPTH_COMPONENT32F = 0x8CAC,
        DEPTH32F_STENCIL8 = 0x8CAD,

        R8 = 0x8229,
        RG8 = 0x822B,
        RGB8 = 0x8051,
        RGBA8 = 0x8058,
        SRGB8_ALPHA8 = 0x8C43,

        R32F = 0x822E,
        RG32F = 0x8230,
        RGBA32F = 0x8814,

        R16F = 0x822D,
        RG16F = 0x822F,
        RGBA16F = 0x881A,
    }
}
