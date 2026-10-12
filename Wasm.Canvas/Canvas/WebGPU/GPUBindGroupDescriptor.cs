using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUBindGroupDescriptor : JSObject
    {
        private GPUBindGroupLayout _layout;

        public GPUBindGroupDescriptor() : base(Register())
        {
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUBindGroupDescriptor.Create"));
        }

        public GPUBindGroupLayout Layout
        {
            get { return _layout; }
            set
            {
                _layout = value;
                Invoke(RegisterFunction("nkGPUBindGroupDescriptor.SetLayout"), value.Uid);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _layout = null;

            }

            base.Dispose(disposing);
        }
    }
}
