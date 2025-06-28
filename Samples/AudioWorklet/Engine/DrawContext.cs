﻿using System;
using System.Numerics;
using nkast.Wasm.Canvas;
using nkast.Wasm.Canvas.WebGL;

namespace AudioWorklet.Engine
{
    public class DrawContext
    {
        public ICanvasRenderingContext CanvasContext;
        public IWebGLRenderingContext GLContext;
        public int Layer;
        public TimeSpan t, dt;

        public Matrix4x4 world;
        public Matrix4x4 view;
        public Matrix4x4 proj;

        public DrawContext()
        {
        }
    }
}