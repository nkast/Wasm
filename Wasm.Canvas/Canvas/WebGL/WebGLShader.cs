﻿using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGLShader : JSObject
    {
        WebGLRenderingContext _glContext;

        internal WebGLShader(int uid, WebGLRenderingContext glContext) : base(uid)
        {
            _glContext = glContext;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _glContext.DeleteShader(this);
            _glContext = null;

            base.Dispose(disposing);
        }
    }
}