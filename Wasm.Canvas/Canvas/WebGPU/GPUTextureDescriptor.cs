using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUTextureDescriptor
    {
        public GPUTextureFormat Format;
        public GPUTextureUsageType Usage;
        public GPUTextureSize Size;
        public int MipLevelCount;
        public int SampleCount;
        public GPUTextureDimensionType Dimension;

        public GPUTextureDescriptor()
        {
            this.Size.Width = 1;
            this.Size.Height = 1;
            this.Size.DepthOrArrayLayers = 1;
            this.MipLevelCount = 1;
            this.SampleCount = 1;
            this.Dimension = GPUTextureDimensionType.Texture2D;
        }
    }
}
