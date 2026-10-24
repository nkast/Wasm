using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUBuffer : CachedJSObject<GPUBuffer>, IGPUBindingResource
    {
        private readonly int _fid_Destroy;

        public GPUDevice _device;

        internal GPUBuffer(int uid, GPUDevice device) : base(uid)
        {
            _fid_Destroy = RegisterFunction("nkGPUBuffer.Destroy");
            _device = device;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Invoke(_fid_Destroy);
            }

            _device = null;

            base.Dispose(disposing);
        }
    }
}
