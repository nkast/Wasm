using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUVertexState : JSObject
    {
        private GPUShaderModule _module;
        public GPUVertexState() : base(Register())
        {
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUVertexState.Create"));
        }

        public GPUShaderModule Module
        {
            get { return _module; }
            set
            {
                _module = value;
                Invoke(RegisterFunction("nkGPUVertexState.SetModule"), value.Uid);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _module = null;
            }

            base.Dispose(disposing);
        }
    }
}
