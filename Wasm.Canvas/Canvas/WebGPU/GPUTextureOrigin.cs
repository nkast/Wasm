using System;
using System.Runtime.InteropServices;

namespace nkast.Wasm.Canvas.WebGPU
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct GPUTextureOrigin
    {
        public int X;
        public int Y;
        public int Z;

        public GPUTextureOrigin(int x)
        {
            this.X = x;
            this.Y = 0;
            this.Z = 0;
        }

        public GPUTextureOrigin(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Z = 0;
        }

        public GPUTextureOrigin(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}
