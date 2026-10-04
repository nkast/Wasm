using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public struct GPUColor
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public GPUColor(float r, float g, float b, float a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public static GPUColor FromByte(byte r, byte g, byte b, byte a)
        {
            return new GPUColor(r / 255f, g / 255f, b / 255f, a / 255f);
        }
    }
}
