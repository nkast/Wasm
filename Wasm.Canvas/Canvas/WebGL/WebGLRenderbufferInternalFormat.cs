using System;

namespace nkast.Wasm.Canvas.WebGL
{
    public enum WebGLRenderbufferInternalFormat
    {
        DEPTH_COMPONENT16   = 0x81A5,
        STENCIL_INDEX8      = 0x8D48,
        DEPTH_STENCIL       = 0x84F9,

        RGBA4   = 0x8056,
        RGB5_A1 = 0x8057,
        RGB565  = 0x8D62,
        /// <remarks>WebGL1 Extension: EXT_sRGB</remarks>
        SRGB8_ALPHA8_EXT = 0x8C43,
    }
}
