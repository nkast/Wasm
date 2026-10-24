using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUFragmentState : JSObject
    {
        private readonly int _fid_SetModule;
        private readonly int _fid_SetEntryPoint;
        private readonly int _fid_SetTargets;

        private GPUShaderModule _module;
        private string _entryPoint;

        public GPUFragmentState() : base(Register())
        {
            _fid_SetModule = RegisterFunction("nkGPUFragmentState.SetModule");
            _fid_SetEntryPoint = RegisterFunction("nkGPUFragmentState.SetEntryPoint");
            _fid_SetTargets = RegisterFunction("nkGPUFragmentState.SetTargets");
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

        public GPUColorTargetStateCollection Targets
        {
            get { throw new NotImplementedException(); }
            set
            {
                Invoke(_fid_SetTargets, value.Uid);
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
