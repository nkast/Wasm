using System;
using System.Runtime.InteropServices;

namespace nkast.Wasm.Canvas.WebGPU
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    internal struct GPUBindGroupEntryData
    {
        public int Binding;
        public int ResourceUid;
    }
}