using System;

namespace nkast.Wasm.Canvas.WebGL
{
    public enum WebGLFramebufferStatus
    {
        FRAMEBUFFER_COMPLETE                        = 0x8CD5,
        FRAMEBUFFER_INCOMPLETE_ATTACHMENT           = 0x8CD6,
        FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT   = 0x8CD7,
        FRAMEBUFFER_INCOMPLETE_DIMENSIONS           = 0x8CD9,
        FRAMEBUFFER_UNSUPPORTED                     = 0x8CDD,
    }
}
