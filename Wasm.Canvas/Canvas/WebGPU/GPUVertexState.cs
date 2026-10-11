using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUVertexState : JSObject
    {
        private GPUShaderModule _module;
        private string _entryPoint;
        private GPUVertexBufferLayoutCollection _buffers;

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

        public string EntryPoint
        {
            get { return _entryPoint; }
            set
            {
                _entryPoint = value;
                Invoke(RegisterFunction("nkGPUVertexState.SetEntryPoint"), value ?? "");
            }
        }

        public GPUVertexBufferLayoutCollection Buffers
        {
            get { return _buffers; }
            set
            {
                _buffers = value;
                Invoke(RegisterFunction("nkGPUVertexState.SetBuffers"), (value != null) ? value.Uid : -1);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _module = null;
                _entryPoint = null;
                _buffers = null;
            }

            base.Dispose(disposing);
        }
    }
}
