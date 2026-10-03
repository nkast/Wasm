using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUVertexBufferLayout : CachedJSObject<GPUVertexBufferLayout>
    {
        private readonly int _fid_GetArrayStride;
        private readonly int _fid_SetArrayStride;
        private readonly int _fid_GetStepMode;
        private readonly int _fid_SetStepMode;
        private readonly int _fid_GetAttributes;
        private readonly int _fid_SetAttributes;

        public long ArrayStride
        {
            get { return InvokeRetInt(_fid_GetArrayStride); }
            set { Invoke(_fid_SetArrayStride, (int)value); }
        }

        public GPUVertexStepMode StepMode
        {
            get { return (GPUVertexStepMode)InvokeRetInt(_fid_GetStepMode); }
            set { Invoke(_fid_SetStepMode, (int)value); }
        }

        public GPUVertexAttributeCollection Attributes
        {
            get
            {
                int uid = InvokeRetInt(_fid_GetAttributes);
                GPUVertexAttributeCollection attributes = GPUVertexAttributeCollection.FromUid(uid);
                if (attributes != null)
                    return attributes;

                return new GPUVertexAttributeCollection(uid, true);
            }
            set
            {
                Invoke(_fid_SetAttributes, value.Uid);
            }
        }

        public GPUVertexBufferLayout() : base(Register())
        {
            _fid_GetArrayStride = RegisterFunction("nkGPUVertexBufferLayout.GetArrayStride");
            _fid_SetArrayStride = RegisterFunction("nkGPUVertexBufferLayout.SetArrayStride");
            _fid_GetStepMode = RegisterFunction("nkGPUVertexBufferLayout.GetStepMode");
            _fid_SetStepMode = RegisterFunction("nkGPUVertexBufferLayout.SetStepMode");
            _fid_GetAttributes = RegisterFunction("nkGPUVertexBufferLayout.GetAttributes");
            _fid_SetAttributes = RegisterFunction("nkGPUVertexBufferLayout.SetAttributes");
        }

        internal GPUVertexBufferLayout(int uid) : base(uid)
        {
            _fid_GetArrayStride = RegisterFunction("nkGPUVertexBufferLayout.GetArrayStride");
            _fid_SetArrayStride = RegisterFunction("nkGPUVertexBufferLayout.SetArrayStride");
            _fid_GetStepMode = RegisterFunction("nkGPUVertexBufferLayout.GetStepMode");
            _fid_SetStepMode = RegisterFunction("nkGPUVertexBufferLayout.SetStepMode");
            _fid_GetAttributes = RegisterFunction("nkGPUVertexBufferLayout.GetAttributes");
            _fid_SetAttributes = RegisterFunction("nkGPUVertexBufferLayout.SetAttributes");
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUVertexBufferLayout.Create"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            base.Dispose(disposing);
        }
    }
}
