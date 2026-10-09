using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPURenderPassDescriptor : JSObject
    {
        private int? _maxDrawCount;
        private GPUColorAttachmentCollection _colorAttachments;

        public GPURenderPassDescriptor() : base(Register())
        {
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPURenderPassDescriptor.Create"));
        }

        public int? MaxDrawCount
        {
            get { return _maxDrawCount; }
            set
            {
                _maxDrawCount = value;
                Invoke(RegisterFunction("nkGPURenderPassDescriptor.SetMaxDrawCount"), value ?? -1);
            }
        }

        public GPUColorAttachmentCollection ColorAttachments
        {
            get { return _colorAttachments; }
            set
            {
                _colorAttachments = value;
                Invoke(RegisterFunction("nkGPURenderPassDescriptor.SetColorAttachments"), value.Uid);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _colorAttachments = null;
            }

            base.Dispose(disposing);
        }
    }
}
