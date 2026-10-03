using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUDevice : JSObject
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
