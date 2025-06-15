using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;
using nkast.Wasm.Canvas.WebGL;

namespace nkast.Wasm.XR
{
    internal class XRWebGLFramebuffer : WebGLFramebuffer
    {
        internal XRWebGLFramebuffer(int uid)
            : base(uid, null)
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