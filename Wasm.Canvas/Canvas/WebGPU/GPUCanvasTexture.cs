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

        public GPUTextureView CreateView(GPUTextureViewDescriptor descriptor)
        {
            int uid = InvokeRetInt<int, int, int, int, int, int, int, int, int>(
                RegisterFunction("nkGPUTexture.CreateView1"),
                descriptor.BaseArrayLayer,
                descriptor.BaseMipLevel,
                (int)descriptor.Aspect,
                descriptor.Swizzle?.Pack() ?? -1,
                descriptor.ArrayLayerCount ?? -1,
                (int?)descriptor.Dimension ?? -1,
                (int?)descriptor.Format ?? -1,
                descriptor.MipLevelCount ?? -1,
                (int?)descriptor.Usage ?? -1);
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
