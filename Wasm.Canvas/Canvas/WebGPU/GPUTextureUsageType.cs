using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    [Flags]
    public enum GPUTextureUsageType
    {
        CopySrc             = 0x01,
        CopyDst             = 0x02,
        TextureBinding      = 0x04,
        StorageBinding      = 0x08,
        RenderAttachment    = 0x10,
        TransientAttachment = 0x20,
    }

}
