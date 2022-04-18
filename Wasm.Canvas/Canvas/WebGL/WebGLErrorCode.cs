using System;

namespace nkast.Wasm.Canvas.WebGL
{
    public enum WebGLErrorCode
    {
        NO_ERROR            = 0x0000,
        INVALID_ENUM        = 0x0500, 
        INVALID_VALUE       = 0x0501,
        INVALID_OPERATION   = 0x0502,
        OUT_OF_MEMORY       = 0x0505,
        INVALID_FRAMEBUFFER_OPERATION = 0x0506,
        CONTEXT_LOST_WEBGL  = 0x9242,
    }
}
