using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUDevice : CachedJSObject<GPUDevice>
    {
        private readonly int _fid_GetLimits;
        private readonly int _fid_CreateCommandEncoder;
        private readonly int _fid_CreateBuffer;
        private readonly int _fid_CreateTexture;
        private readonly int _fid_CreateSampler;
        private readonly int _fid_CreateSampler1;
        private readonly int _fid_CreateShaderModule;
        private readonly int _fid_CreateRenderPipeline;
        private readonly int _fid_CreateBindGroup;
        private readonly int _fid_GetQueue;
        private readonly int _fid_Destroy;

        internal GPUDevice(int uid) : base(uid)
        {
            _fid_GetLimits = RegisterFunction("nkGPUDevice.GetLimits");
            _fid_CreateCommandEncoder = RegisterFunction("nkGPUDevice.CreateCommandEncoder");
            _fid_CreateBuffer = RegisterFunction("nkGPUDevice.CreateBuffer");
            _fid_CreateTexture = RegisterFunction("nkGPUDevice.CreateTexture");
            _fid_CreateSampler = RegisterFunction("nkGPUDevice.CreateSampler");
            _fid_CreateSampler1 = RegisterFunction("nkGPUDevice.CreateSampler1");
            _fid_CreateShaderModule = RegisterFunction("nkGPUDevice.CreateShaderModule");
            _fid_CreateRenderPipeline = RegisterFunction("nkGPUDevice.CreateRenderPipeline");
            _fid_CreateBindGroup = RegisterFunction("nkGPUDevice.CreateBindGroup");
            _fid_GetQueue = RegisterFunction("nkGPUDevice.GetQueue");
            _fid_Destroy = RegisterFunction("nkGPUDevice.Destroy");
        }

        public unsafe GPUSupportedLimits GetLimits()
        {
            GPUSupportedLimits limits = new GPUSupportedLimits();
            Invoke<IntPtr>(_fid_GetLimits, new IntPtr(&limits));
            return limits;
        }

        public GPUCommandEncoder CreateCommandEncoder()
        {
            int uid = InvokeRetInt(_fid_CreateCommandEncoder);
            return new GPUCommandEncoder(uid, this);
        }

        public GPUBuffer CreateBuffer(GPUBufferDescriptor descriptor)
        {
            int uid = InvokeRetInt(
                _fid_CreateBuffer,
                (int)descriptor.Size,
                (int)descriptor.Usage,
                descriptor.MappedAtCreation ? 1 : 0);
            return new GPUBuffer(uid, this);
        }

        public unsafe GPUTexture CreateTexture(GPUTextureDescriptor descriptor)
        {
            GPUTextureSize size = descriptor.Size;
            int uid = InvokeRetInt(
                _fid_CreateTexture,
                (int)descriptor.Format,
                (int)descriptor.Usage,
                (int)&size,
                descriptor.MipLevelCount,
                descriptor.SampleCount,
                (int)descriptor.Dimension);
            return new GPUTexture(uid, this);
        }

        public GPUSampler CreateSampler()
        {
            int uid = InvokeRetInt(_fid_CreateSampler);
            return new GPUSampler(uid, this);
        }

        public GPUSampler CreateSampler(GPUSamplerDescriptor descriptor)
        {
            int uid = InvokeRetInt(
                _fid_CreateSampler1,
                (int)descriptor.AddressModeU,
                (int)descriptor.AddressModeV,
                (int)descriptor.AddressModeW,
                (int)descriptor.MagFilter,
                (int)descriptor.MinFilter,
                (int)descriptor.MipmapFilter,
                descriptor.LodMinClamp,
                descriptor.Compare.HasValue ? (int)descriptor.Compare.Value : -1,
                descriptor.LodMaxClamp ?? -1f,
                descriptor.MaxAnisotropy ?? -1);
            return new GPUSampler(uid, this);
        }

        public GPUShaderModule CreateShaderModule(GPUShaderModuleDescriptor descriptor)
        {
            int uid = InvokeRetInt(_fid_CreateShaderModule, descriptor.Code);
            return new GPUShaderModule(uid, this);
        }

        public GPURenderPipeline CreateRenderPipeline(GPURenderPipelineDescriptor descriptor)
        {
            int uid = InvokeRetInt(_fid_CreateRenderPipeline, descriptor.Uid);
            return new GPURenderPipeline(uid, this);
        }

        public GPUBindGroup CreateBindGroup(GPUBindGroupDescriptor descriptor)
        {
            int uid = InvokeRetInt(_fid_CreateBindGroup, descriptor.Uid);
            return new GPUBindGroup(uid, this);
        }

        public GPUQueue Queue
        {
            get
            {
                int uid = InvokeRetInt(_fid_GetQueue);

                GPUQueue queue = GPUQueue.FromUid(uid);
                if (queue != null)
                    return queue;

                return new GPUQueue(uid, this);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Invoke(_fid_Destroy);
            }

            base.Dispose(disposing);
        }
    }
}
