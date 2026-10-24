using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPURenderPipelineDescriptor : JSObject
    {
        private readonly int _fid_SetVertex;
        private readonly int _fid_SetFragment;
        private readonly int _fid_SetPrimitive;

        private GPUVertexState _vertex;
        private GPUFragmentState _fragment;
        private GPUPrimitiveState? _primitive;

        public GPURenderPipelineDescriptor() : base(Register())
        {
            _fid_SetVertex = RegisterFunction("nkGPURenderPipelineDescriptor.SetVertex");
            _fid_SetFragment = RegisterFunction("nkGPURenderPipelineDescriptor.SetFragment");
            _fid_SetPrimitive = RegisterFunction("nkGPURenderPipelineDescriptor.SetPrimitive");
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
                Invoke(_fid_SetVertex, value.Uid);
            }
        }

        public GPUFragmentState Fragment
        {
            get { return _fragment; }
            set
            {
                _fragment = value;
                Invoke(_fid_SetFragment, value?.Uid ?? -1);
            }
        }

        public GPUPrimitiveState? Primitive
        {
            get { return _primitive; }
            set
            {
                _primitive = value;
                unsafe
                {
                    if (value.HasValue)
                    {
                        GPUPrimitiveState data = value.Value;
                        Invoke<IntPtr>(_fid_SetPrimitive, new IntPtr(&data));
                    }
                    else
                        Invoke<IntPtr>(_fid_SetPrimitive, IntPtr.Zero);
                }
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
