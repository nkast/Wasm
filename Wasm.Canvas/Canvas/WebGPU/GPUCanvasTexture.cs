using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    internal class GPUCanvasTexture : CachedJSObject<GPUCanvasTexture>, IGPUTexture
    {
        public GPUDevice _device;

        internal GPUCanvasTexture(int uid, GPUDevice device) : base(uid)
        {
            _device = device;
        }

        public GPUTextureView CreateView()
        {
            int uid = InvokeRetInt(RegisterFunction("nkGPUTexture.CreateView"));
            return new GPUTextureView(uid, _device);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //Invoke(RegisterFunction("nkGPUTexture.Destroy"));

            }

            _device = null;

            base.Dispose(disposing);
        }
    }
}
