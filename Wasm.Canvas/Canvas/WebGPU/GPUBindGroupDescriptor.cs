using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUBindGroupDescriptor : JSObject
    {
        private GPUBindGroupLayout _layout;
        private GPUBindGroupEntryCollection _entries;

        public GPUBindGroupDescriptor() : base(Register())
        {
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUBindGroupDescriptor.Create"));
        }

        public GPUBindGroupLayout Layout
        {
            get { return _layout; }
            set
            {
                _layout = value;
                Invoke(RegisterFunction("nkGPUBindGroupDescriptor.SetLayout"), value.Uid);
            }
        }

        public GPUBindGroupEntryCollection Entries
        {
            get { return _entries; }
            set
            {
                _entries = value;
                Invoke(RegisterFunction("nkGPUBindGroupDescriptor.SetEntries"), value.Uid);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _layout = null;
                _entries = null;
            }

            base.Dispose(disposing);
        }
    }
}
