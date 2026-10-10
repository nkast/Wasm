using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUFragmentState : JSObject
    {
        private GPUShaderModule _module;
        private string _entryPoint;

        public GPUFragmentState() : base(Register())
        {
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUFragmentState.Create"));
        }

        public GPUShaderModule Module
        {
            get { return _module; }
            set
            {
                _module = value;
                Invoke(RegisterFunction("nkGPUFragmentState.SetModule"), value.Uid);
            }
        }

        public string EntryPoint
        {
            get { return _entryPoint; }
            set
            {
                _entryPoint = value;
                Invoke(RegisterFunction("nkGPUFragmentState.SetEntryPoint"), value ?? "");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _module = null;
                _entryPoint = null;

            }

            base.Dispose(disposing);
        }
    }
}
