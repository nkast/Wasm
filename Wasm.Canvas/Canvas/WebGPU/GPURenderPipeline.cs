using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPURenderPipeline : JSObject
    {
        private readonly int _fid_GetBindGroupLayout;

        public GPUDevice _device;

        internal GPURenderPipeline(int uid, GPUDevice device) : base(uid)
        {
            _fid_GetBindGroupLayout = RegisterFunction("nkGPURenderPipeline.GetBindGroupLayout");
            _device = device;
        }

        public GPUBindGroupLayout GetBindGroupLayout(int index)
        {
            int uid = InvokeRetInt(_fid_GetBindGroupLayout, index);

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
