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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Invoke(RegisterFunction("nkGPUDevice.Destroy"));

            base.Dispose(disposing);
        }
    }
}
