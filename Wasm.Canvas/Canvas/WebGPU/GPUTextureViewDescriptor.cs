using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public struct GPUTextureViewDescriptor
    {
        public int BaseArrayLayer;
        public int BaseMipLevel;
        public GPUTextureAspectType Aspect;
        public GPUTextureSwizzle? Swizzle;
        public int? ArrayLayerCount;
        public GPUTextureViewDimensionType? Dimension;
        public GPUTextureFormat? Format;
        public int? MipLevelCount;
        public GPUTextureUsageType? Usage;
    }
}
