using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGPU
{
    internal class GPUCanvasTexture : CachedJSObject<GPUCanvasTexture>, IGPUTexture
    {
        internal GPUCanvasTexture(int uid) : base(uid)
        {
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
