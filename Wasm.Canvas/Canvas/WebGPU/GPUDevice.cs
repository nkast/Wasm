using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUDevice : CachedJSObject<GPUDevice>
    {
        internal GPUDevice(int uid) : base(uid)
        {
        }

        public unsafe GPUSupportedLimits GetLimits()
        {
            GPUSupportedLimits limits = new GPUSupportedLimits();
            Invoke<IntPtr>(RegisterFunction("nkGPUDevice.GetLimits"), new IntPtr(&limits));
            return limits;
        }

        public GPUCommandEncoder CreateCommandEncoder()
        {
            int uid = InvokeRetInt(RegisterFunction("nkGPUDevice.CreateCommandEncoder"));
            return new GPUCommandEncoder(uid, this);
        }

        public GPUBuffer CreateBuffer(GPUBufferDescriptor descriptor)
        {
            int uid = InvokeRetInt(
                RegisterFunction("nkGPUDevice.CreateBuffer"),
                (int)descriptor.Size,
                (int)descriptor.Usage,
                descriptor.MappedAtCreation ? 1 : 0);
            return new GPUBuffer(uid, this);
        }

        public GPUShaderModule CreateShaderModule(GPUShaderModuleDescriptor descriptor)
        {
            int uid = InvokeRetInt(RegisterFunction("nkGPUDevice.CreateShaderModule"), descriptor.Code);
            return new GPUShaderModule(uid, this);
        }

        public GPUQueue Queue
        {
            get
            {
                int uid = InvokeRetInt(RegisterFunction("nkGPUDevice.GetQueue"));

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
                Invoke(RegisterFunction("nkGPUDevice.Destroy"));
            }

            base.Dispose(disposing);
        }
    }
}
