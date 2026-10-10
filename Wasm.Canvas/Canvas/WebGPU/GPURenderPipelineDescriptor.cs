using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPURenderPipelineDescriptor : JSObject
    {
        private GPUVertexState _vertex;
        private GPUFragmentState _fragment;

        public GPURenderPipelineDescriptor() : base(Register())
        {
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPURenderPipelineDescriptor.Create"));
        }

        public GPUVertexState Vertex
        {
            get { return _vertex; }
            set
            {
                _vertex = value;
                Invoke(RegisterFunction("nkGPURenderPipelineDescriptor.SetVertex"), value.Uid);
            }
        }

        public GPUFragmentState Fragment
        {
            get { return _fragment; }
            set
            {
                _fragment = value;
                Invoke(RegisterFunction("nkGPURenderPipelineDescriptor.SetFragment"), value?.Uid ?? -1);
            }
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _vertex = null;
                _fragment = null;

            }

            base.Dispose(disposing);
        }
    }
}
