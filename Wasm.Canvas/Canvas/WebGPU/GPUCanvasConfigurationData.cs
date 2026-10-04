using System;
using System.Runtime.InteropServices;

namespace nkast.Wasm.Canvas.WebGPU
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct GPUCanvasConfigurationData
    {
        public int DeviceUid;
        public int Format;
        public int AlphaMode;
        public int ToneMappingMode;
        public int ColorSpace;
        public int Usage;
    }
}
