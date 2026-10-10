using System;
using System.Numerics;
using nkast.Wasm.Canvas.WebGPU;
using CanvasGPU.Engine;

namespace CanvasGPU.Pages
{
    public partial class TriangleClip : Clip
    {
        GPUShaderModule _shaderModule;
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


        private const string SHADER_SOURCE  = "struct VSOut"
                                            + "{"
                                            + "    @builtin(position) pos : vec4f,"
                                            + "    @location(0) color : vec3f,"
                                            + "};"
                                            + ""
                                            + "@vertex fn vs_main(@location(0) aPos : vec3f, @location(1) aColor : vec3f) -> VSOut"
                                            + "{"
                                            + "    var o : VSOut;"
                                            + "    o.pos = vec4f(aPos, 1.0);"
                                            + "    o.color = aColor;"
                                            + "    return o;"
                                            + "}"
                                            + ""
                                            + "@fragment fn fs_main(@location(0) vColor : vec3f) -> @location(0) vec4f"
                                            + "{"
                                            + "    return vec4f(vColor, 1.0);"
                                            + "}";


        private void DrawTriangle(DrawContext dc)
        {
            GPUDevice device = dc.GPUDevice;
            GPURenderPassEncoder renderPass = dc.RenderPass;
            GPUCanvasConfiguration canvasConfiguration = dc.CanvasConfiguration;

            if (_shaderModule == null)
            {
                GPUShaderModuleDescriptor shaderDescriptor = new GPUShaderModuleDescriptor();
                shaderDescriptor.Code = SHADER_SOURCE;
                _shaderModule = device.CreateShaderModule(shaderDescriptor);
            }

            {
                // TODO: create render pipeline

                GPUVertexState vertexState = new GPUVertexState();

                GPURenderPipelineDescriptor pipelineDescriptor = new GPURenderPipelineDescriptor();
                pipelineDescriptor.Vertex = vertexState;
            }

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
            renderPass.SetVertexBuffer(0, _vertexBuffer);
            // TODO: set uniforms (worldViewProj)
            // TODO: draw triangle
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _shaderModule?.Dispose();
                _shaderModule = null;
                _vertexBuffer?.Dispose();
                _vertexBuffer = null;
                // TODO: dispose pipeline
            }

            base.Dispose(disposing);
        }
    }
}
