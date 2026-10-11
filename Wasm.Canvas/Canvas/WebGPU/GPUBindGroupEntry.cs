using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public struct GPUBindGroupEntry
    {
        public int Binding;
        public IGPUBindingResource Resource;
    }
}
