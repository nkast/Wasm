using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPURenderPassDescriptor : JSObject
    {
        private readonly int _fid_SetMaxDrawCount;
        private readonly int _fid_SetColorAttachments;

        private int? _maxDrawCount;
        private GPUColorAttachmentCollection _colorAttachments;

        public GPURenderPassDescriptor() : base(Register())
        {
            _fid_SetMaxDrawCount = RegisterFunction("nkGPURenderPassDescriptor.SetMaxDrawCount");
            _fid_SetColorAttachments = RegisterFunction("nkGPURenderPassDescriptor.SetColorAttachments");
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
                Invoke(_fid_SetMaxDrawCount, value ?? -1);
            }
        }

        public GPUColorAttachmentCollection ColorAttachments
        {
            get { return _colorAttachments; }
            set
            {
                _colorAttachments = value;
                Invoke(_fid_SetColorAttachments, value.Uid);
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
