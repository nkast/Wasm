using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    [Flags]
    public enum GPUTextureUsageType
    {
        CopySrc             =  1,
        CopyDst             =  2,
        TextureBinding      =  4,
        StorageBinding      =  8,
        RenderAttachment    = 16,
        TransientAttachment = 32,
    }

}
