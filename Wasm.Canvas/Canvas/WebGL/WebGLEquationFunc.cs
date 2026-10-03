using System;

namespace nkast.Wasm.Canvas.WebGL
{
    public enum WebGLEquationFunc
    {
        ADD                 = 0x8006,
        SUBTRACT            = 0x800A,
        REVERSE_SUBTRACT    = 0x800B,

        // WebGL2
        MIN                 = 0x8007,
        MAX                 = 0x8008
    }
}
