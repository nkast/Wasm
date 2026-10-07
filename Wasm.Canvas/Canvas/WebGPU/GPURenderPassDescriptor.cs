using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPURenderPassDescriptor : JSObject
    {
        private int? _maxDrawCount;
        private GPUColorAttachment[] _colorAttachments;

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

        public unsafe GPUColorAttachment[] ColorAttachments
        {
            get { return _colorAttachments; }
            set
            {
                _colorAttachments = value;

                int length = (value != null) ? value.Length : 0;
                GPUColorAttachmentData[] data = new GPUColorAttachmentData[length];
                for (int i = 0; i < length; i++)
                {
                    data[i].ViewUid = value[i].View.Uid;
                    data[i].LoadOp = value[i].LoadOp;
                    data[i].StoreOp = value[i].StoreOp;
                    data[i].ClearValue = value[i].ClearValue.GetValueOrDefault();
                    data[i].DepthSlice = value[i].DepthSlice ?? -1;
                }

                fixed (GPUColorAttachmentData* pData = data)
                {
                    Invoke(RegisterFunction("nkGPURenderPassDescriptor.SetColorAttachments"), (int)pData, length);
                }
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
