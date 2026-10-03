using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    internal class GPUCanvasContext : RenderingContext, IGPUCanvasContext
    {
        internal GPUCanvasContext(Canvas canvas, int uid) : base(canvas, uid)
        {
        }

    }
}
