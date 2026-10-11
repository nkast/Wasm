using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUVertexBufferLayout : CachedJSObject<GPUVertexBufferLayout>
    {
        public long ArrayStride
        {
            get { return InvokeRetInt(RegisterFunction("nkGPUVertexBufferLayout.GetArrayStride")); }
            set { Invoke(RegisterFunction("nkGPUVertexBufferLayout.SetArrayStride"), (int)value); }
        }

        public GPUVertexStepMode StepMode
        {
            get { return (GPUVertexStepMode)InvokeRetInt(RegisterFunction("nkGPUVertexBufferLayout.GetStepMode")); }
            set { Invoke(RegisterFunction("nkGPUVertexBufferLayout.SetStepMode"), (int)value); }
        }

        public GPUVertexAttributeCollection Attributes
        {
            get
            {
                int uid = InvokeRetInt(RegisterFunction("nkGPUVertexBufferLayout.GetAttributes"));
                GPUVertexAttributeCollection attributes = GPUVertexAttributeCollection.FromUid(uid);
                if (attributes == null)
                    attributes = new GPUVertexAttributeCollection(uid);
                return attributes;
            }
            set
            {
                Invoke(RegisterFunction("nkGPUVertexBufferLayout.SetAttributes"), value.Uid);
            }
        }

        public GPUVertexBufferLayout() : base(Register())
        {
        }

        internal GPUVertexBufferLayout(int uid) : base(uid)
        {
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
