using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public interface IGPUCanvasContext : IRenderingContext
    {
        void Configure(GPUCanvasConfiguration configuration);
        GPUCanvasConfiguration GetConfiguration();

    }
}
