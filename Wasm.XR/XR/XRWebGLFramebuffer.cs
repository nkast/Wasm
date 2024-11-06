﻿using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;
using nkast.Wasm.Canvas.WebGL;

namespace nkast.Wasm.XR
{
    internal class XRWebGLFramebuffer : WebGLFramebuffer
    {
        internal XRWebGLFramebuffer(int uid, IWebGLRenderingContext glContext)
            : base(uid, glContext)
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            //base.Dispose(disposing);
        }
    }
}