using System;
using System.Numerics;
using nkast.Wasm.Canvas.WebGPU;
using CanvasGPU.Engine;

namespace CanvasGPU.Pages
{
    public partial class TriangleClip : Clip
    {
        GPUBuffer _vertexBuffer;
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
            GPUCanvasConfiguration canvasConfiguration = dc.CanvasConfiguration;

            // TODO: create shader module
            // TODO: create render pipeline

            if (_vertexBuffer == null)
            {
                float[] vertices = new[]
                {
                    -0.5f, -0.5f, 0.0f,   1.0f, 0.0f, 0.0f,
                     0.0f,  0.5f, 0.0f,   0.0f, 0.0f, 0.0f,
                     0.5f, -0.5f, 0.0f,   0.0f, 1.0f, 0.0f
                };

                GPUBufferDescriptor bufferDescriptor = new GPUBufferDescriptor();
                bufferDescriptor.Size = vertices.Length * sizeof(float);
                bufferDescriptor.Usage = GPUBufferUsageType.Vertex | GPUBufferUsageType.CopyDst;
                _vertexBuffer = device.CreateBuffer(bufferDescriptor);
                device.Queue.WriteBuffer<float>(_vertexBuffer, 0, vertices);
            }
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
                // TODO: dispose pipeline, shader module
                _vertexBuffer?.Dispose();
                _vertexBuffer = null;
            }

            base.Dispose(disposing);
        }
    }
}
