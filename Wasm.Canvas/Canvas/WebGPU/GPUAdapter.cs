using System;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUAdapter : JSObject
    {
        private readonly int _fid_GetInfo;
        private readonly int _fid_GetLimits;
        private readonly int _fid_RequestDevice;
        private readonly int _fid_RequestDevice1;

        internal GPUAdapter(int uid) : base(uid)
        {
            _fid_GetInfo = RegisterFunction("nkGPUAdapter.GetInfo");
            _fid_GetLimits = RegisterFunction("nkGPUAdapter.GetLimits");
            _fid_RequestDevice = RegisterFunction("nkGPUAdapter.RequestDevice");
            _fid_RequestDevice1 = RegisterFunction("nkGPUAdapter.RequestDevice1");
        }

        public GPUAdapterInfo GetInfo()
        {
            int uid = InvokeRetInt(_fid_GetInfo);

            GPUAdapterInfo info = GPUAdapterInfo.FromUid(uid);
            if (info != null)
                return info;

            return new GPUAdapterInfo(uid);
        }

        public unsafe GPUSupportedLimits GetLimits()
        {
            GPUSupportedLimits limits = new GPUSupportedLimits();
            Invoke<IntPtr>(_fid_GetLimits, new IntPtr(&limits));
            return limits;
        }

        public Task<GPUDevice> RequestDeviceAsync()
        {
            int uid = InvokeRetInt(_fid_RequestDevice);

            PromiseJSObject<GPUDevice> promise = new PromiseJSObject<GPUDevice>(uid, (int newuid) => new GPUDevice(newuid));
            return promise.GetTask();
        }

        public unsafe Task<GPUDevice> RequestDeviceAsync(GPURequiredLimits requiredLimits)
        {
            GPUSupportedLimits limits = new GPUSupportedLimits();
            limits.MaxTextureDimension1D = requiredLimits.MaxTextureDimension1D ?? -1;
            limits.MaxTextureDimension2D = requiredLimits.MaxTextureDimension2D ?? -1;
            limits.MaxTextureDimension3D = requiredLimits.MaxTextureDimension3D ?? -1;
            limits.MaxTextureArrayLayers = requiredLimits.MaxTextureArrayLayers ?? -1;
            limits.MaxBindGroups = requiredLimits.MaxBindGroups ?? -1;
            limits.MaxBindingsPerBindGroup = requiredLimits.MaxBindingsPerBindGroup ?? -1;
            limits.MaxDynamicUniformBuffersPerPipelineLayout = requiredLimits.MaxDynamicUniformBuffersPerPipelineLayout ?? -1;
            limits.MaxDynamicStorageBuffersPerPipelineLayout = requiredLimits.MaxDynamicStorageBuffersPerPipelineLayout ?? -1;
            limits.MaxSampledTexturesPerShaderStage = requiredLimits.MaxSampledTexturesPerShaderStage ?? -1;
            limits.MaxSamplersPerShaderStage = requiredLimits.MaxSamplersPerShaderStage ?? -1;
            limits.MaxStorageBuffersPerShaderStage = requiredLimits.MaxStorageBuffersPerShaderStage ?? -1;
            limits.MaxStorageTexturesPerShaderStage = requiredLimits.MaxStorageTexturesPerShaderStage ?? -1;
            limits.MaxUniformBuffersPerShaderStage = requiredLimits.MaxUniformBuffersPerShaderStage ?? -1;
            limits.MinUniformBufferOffsetAlignment = requiredLimits.MinUniformBufferOffsetAlignment ?? -1;
            limits.MinStorageBufferOffsetAlignment = requiredLimits.MinStorageBufferOffsetAlignment ?? -1;
            limits.MaxVertexBuffers = requiredLimits.MaxVertexBuffers ?? -1;
            limits.MaxVertexAttributes = requiredLimits.MaxVertexAttributes ?? -1;
            limits.MaxVertexBufferArrayStride = requiredLimits.MaxVertexBufferArrayStride ?? -1;
            limits.MaxInterStageShaderVariables = requiredLimits.MaxInterStageShaderVariables ?? -1;
            limits.MaxColorAttachments = requiredLimits.MaxColorAttachments ?? -1;
            limits.MaxColorAttachmentBytesPerSample = requiredLimits.MaxColorAttachmentBytesPerSample ?? -1;
            limits.MaxComputeWorkgroupStorageSize = requiredLimits.MaxComputeWorkgroupStorageSize ?? -1;
            limits.MaxComputeInvocationsPerWorkgroup = requiredLimits.MaxComputeInvocationsPerWorkgroup ?? -1;
            limits.MaxComputeWorkgroupSizeX = requiredLimits.MaxComputeWorkgroupSizeX ?? -1;
            limits.MaxComputeWorkgroupSizeY = requiredLimits.MaxComputeWorkgroupSizeY ?? -1;
            limits.MaxComputeWorkgroupSizeZ = requiredLimits.MaxComputeWorkgroupSizeZ ?? -1;
            limits.MaxComputeWorkgroupsPerDimension = requiredLimits.MaxComputeWorkgroupsPerDimension ?? -1;
            limits.MaxStorageBuffersInVertexStage = requiredLimits.MaxStorageBuffersInVertexStage ?? -1;
            limits.MaxStorageBuffersInFragmentStage = requiredLimits.MaxStorageBuffersInFragmentStage ?? -1;
            limits.MaxStorageTexturesInVertexStage = requiredLimits.MaxStorageTexturesInVertexStage ?? -1;
            limits.MaxStorageTexturesInFragmentStage = requiredLimits.MaxStorageTexturesInFragmentStage ?? -1;
            limits.MaxUniformBufferBindingSize = requiredLimits.MaxUniformBufferBindingSize ?? -1;
            limits.MaxStorageBufferBindingSize = requiredLimits.MaxStorageBufferBindingSize ?? -1;
            limits.MaxBufferSize = requiredLimits.MaxBufferSize ?? -1;

            int uid = InvokeRetInt<IntPtr>(_fid_RequestDevice1, new IntPtr(&limits));

            PromiseJSObject<GPUDevice> promise = new PromiseJSObject<GPUDevice>(uid, (int newuid) => new GPUDevice(newuid));
            return promise.GetTask();
        }
        
    }
}
