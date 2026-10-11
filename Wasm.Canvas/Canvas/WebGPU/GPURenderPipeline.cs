using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPURenderPipeline : JSObject
    {
        public GPUDevice _device;

        internal GPURenderPipeline(int uid, GPUDevice device) : base(uid)
        {
            _device = device;
        }

        public GPUBindGroupLayout GetBindGroupLayout(int index)
        {
            int uid = InvokeRetInt(RegisterFunction("nkGPURenderPipeline.GetBindGroupLayout"), index);

            GPUBindGroupLayout layout = GPUBindGroupLayout.FromUid(uid);
            if (layout != null)
                return layout;

            return new GPUBindGroupLayout(uid, _device);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _device = null;

            base.Dispose(disposing);
        }
    }
}
