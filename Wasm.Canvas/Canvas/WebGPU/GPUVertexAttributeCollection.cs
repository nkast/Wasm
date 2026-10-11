using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    public class GPUVertexAttributeCollection : CachedJSObject<GPUVertexAttributeCollection>
    {

        public GPUVertexAttributeCollection() : base(Register())
        {
        }

        internal GPUVertexAttributeCollection(int uid) : base(uid)
        {
        }

        private static int Register()
        {
            return JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkGPUVertexAttributeCollection.Create"));
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
