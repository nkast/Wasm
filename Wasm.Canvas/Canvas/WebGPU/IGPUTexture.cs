using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public interface IGPUTexture : IDisposable
    {
        int Width { get; }
        int Height { get; }
        int DepthOrArrayLayers { get; }
        int MipLevelCount { get; }
        int SampleCount { get; }
        GPUTextureDimensionType Dimension { get; }
        GPUTextureFormat Format { get; }
        GPUTextureUsageType Usage { get; }

        GPUTextureView CreateView();
        GPUTextureView CreateView(GPUTextureViewDescriptor descriptor);
    }
}
