using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public struct GPUTextureCopyLocation
    {
        public GPUTexture Texture;
        public GPUTextureAspectType Aspect;
        public int MipLevel;
        public GPUTextureOrigin Origin;
    }
}
