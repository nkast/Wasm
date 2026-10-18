using System;
using System.Runtime.InteropServices;

namespace nkast.Wasm.Canvas.WebGPU
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct GPUTextureSwizzle
    {
        public GPUTextureChannelType R;
        public GPUTextureChannelType G;
        public GPUTextureChannelType B;
        public GPUTextureChannelType A;

        internal int Pack()
        {
            return ((int)R << 0) | ((int)G << 4) | ((int)B << 8) | ((int)A << 12);
        }
    }
}
