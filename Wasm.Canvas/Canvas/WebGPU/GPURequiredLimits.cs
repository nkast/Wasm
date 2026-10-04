using System;

namespace nkast.Wasm.Canvas.WebGPU
{
    public struct GPURequiredLimits
    {
        public int? MaxTextureDimension1D;
        public int? MaxTextureDimension2D;
        public int? MaxTextureDimension3D;
        public int? MaxTextureArrayLayers;
        public int? MaxBindGroups;
        public int? MaxBindingsPerBindGroup;
        public int? MaxDynamicUniformBuffersPerPipelineLayout;
        public int? MaxDynamicStorageBuffersPerPipelineLayout;
        public int? MaxSampledTexturesPerShaderStage;
        public int? MaxSamplersPerShaderStage;
        public int? MaxStorageBuffersPerShaderStage;
        public int? MaxStorageTexturesPerShaderStage;
        public int? MaxUniformBuffersPerShaderStage;
        public int? MinUniformBufferOffsetAlignment;
        public int? MinStorageBufferOffsetAlignment;
        public int? MaxVertexBuffers;
        public int? MaxVertexAttributes;
        public int? MaxVertexBufferArrayStride;
        public int? MaxInterStageShaderVariables;
        public int? MaxColorAttachments;
        public int? MaxColorAttachmentBytesPerSample;
        public int? MaxComputeWorkgroupStorageSize;
        public int? MaxComputeInvocationsPerWorkgroup;
        public int? MaxComputeWorkgroupSizeX;
        public int? MaxComputeWorkgroupSizeY;
        public int? MaxComputeWorkgroupSizeZ;
        public int? MaxComputeWorkgroupsPerDimension;
        public int? MaxStorageBuffersInVertexStage;
        public int? MaxStorageBuffersInFragmentStage;
        public int? MaxStorageTexturesInVertexStage;
        public int? MaxStorageTexturesInFragmentStage;

        public long? MaxUniformBufferBindingSize;
        public long? MaxStorageBufferBindingSize;
        public long? MaxBufferSize;
    }
}
