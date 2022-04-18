using System;

namespace nkast.Wasm.Canvas.WebGL
{
    public enum WebGLTextureTarget
    {
        TEXTURE_2D                  = 0x0DE1,
        TEXTURE_CUBE_MAP            = 0x8513,

        TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515,
        TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516,
        TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517,
        TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518,
        TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519,
        TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A,
    }
}
