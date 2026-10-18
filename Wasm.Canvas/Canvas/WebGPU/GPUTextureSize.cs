using System;
using System.Runtime.InteropServices;

namespace nkast.Wasm.Canvas.WebGPU
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct GPUTextureSize
    {
        public int Width;
        public int Height;
        public int DepthOrArrayLayers;

        public GPUTextureSize(int width)
        {
            this.Width = width;
            this.Height = 1;
            this.DepthOrArrayLayers = 1;
        }

        public GPUTextureSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.DepthOrArrayLayers = 1;
        }

        public GPUTextureSize(int width, int height, int depthOrArrayLayers)
        {
            this.Width = width;
            this.Height = height;
            this.DepthOrArrayLayers = depthOrArrayLayers;
        }
    }
}
