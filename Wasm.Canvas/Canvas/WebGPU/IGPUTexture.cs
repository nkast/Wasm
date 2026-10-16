using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public interface IGPUTexture : IDisposable
    {
        GPUTextureView CreateView();
        GPUTextureView CreateView(GPUTextureViewDescriptor descriptor);
    }
}
