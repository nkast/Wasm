using System;
using System.Numerics;
using nkast.Wasm.Canvas;
using nkast.Wasm.Canvas.WebGPU;

namespace CanvasGPU.Engine
{
    public class DrawContext
    {
        public IGPUCanvasContext CanvasContext;
        public GPUDevice GPUDevice;
        public GPURenderPassEncoder RenderPass;
        public GPUCanvasConfiguration CanvasConfiguration;
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