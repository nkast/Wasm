using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public struct GPUSamplerDescriptor
    {
        public GPUAddressType AddressModeU;
        public GPUAddressType AddressModeV;
        public GPUAddressType AddressModeW;
        public GPUFilterType MagFilter;
        public GPUFilterType MinFilter;
        public GPUFilterType MipmapFilter;
        public float LodMinClamp;

        public GPUCompareFunctionType? Compare;
        public float? LodMaxClamp;
        public int? MaxAnisotropy;
    }
}