using System;
using System.Numerics;
using nkast.Wasm.Canvas.WebGPU;
using CanvasGPU.Engine;

namespace CanvasGPU.Pages
{
    public partial class TriangleClip : Clip
    {
        public TriangleClip() : base()
        {
            base.size = new Size(800, 480);
        }


        public override void Update(UpdateContext uc)
        {
            float dt = (float)uc.dt.TotalSeconds;

            base.Update(uc);
        }


        public override void Draw(DrawContext dc)
        {
            if (dc.Layer == 0)
            {
                DrawTriangle(dc);
            }

            base.Draw(dc);
        }


        private void DrawTriangle(DrawContext dc)
        {
            GPUDevice device = dc.GPUDevice;
            GPURenderPassEncoder renderPass = dc.RenderPass;

            // TODO: create shader module
            // TODO: create render pipeline
            // TODO: create vertex buffer
            // TODO: upload vertices
            // TODO: set pipeline
            // TODO: set vertex buffer
            // TODO: set uniforms (worldViewProj)
            // TODO: draw triangle
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // TODO: dispose pipeline, shader module and vertex buffer
            }

            base.Dispose(disposing);
        }
    }
}
