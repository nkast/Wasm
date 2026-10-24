using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUVertexState : JSObject
    {
        private readonly int _fid_SetModule;
        private readonly int _fid_SetEntryPoint;
        private readonly int _fid_SetBuffers;

        private GPUShaderModule _module;
        private string _entryPoint;
        private GPUVertexBufferLayoutCollection _buffers;

        public GPUVertexState() : base(Register())
        {
            _fid_SetModule = RegisterFunction("nkGPUVertexState.SetModule");
            _fid_SetEntryPoint = RegisterFunction("nkGPUVertexState.SetEntryPoint");
            _fid_SetBuffers = RegisterFunction("nkGPUVertexState.SetBuffers");
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
                Invoke(_fid_SetModule, value.Uid);
            }
        }

        public string EntryPoint
        {
            get { return _entryPoint; }
            set
            {
                _entryPoint = value;
                Invoke(_fid_SetEntryPoint, value ?? "");
            }
        }

        public GPUVertexBufferLayoutCollection Buffers
        {
            get { return _buffers; }
            set
            {
                _buffers = value;
                Invoke(_fid_SetBuffers, (value != null) ? value.Uid : -1);
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
