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
        GPURenderPipeline _pipeline;
        GPUBuffer _uniformBuffer;
        float _angleY;

        public TriangleClip() : base()
        {
            base.size = new Size(800, 480);
        }


        public override void Update(UpdateContext uc)
        {
            float dt = (float)uc.dt.TotalSeconds;

            _angleY += dt;

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

            if (_pipeline == null)
            {
                GPUVertexAttributeCollection attributes = new GPUVertexAttributeCollection()
                {
                    new GPUVertexAttribute { Format = GPUVertexFormatType.Float32x3, Offset = 0, ShaderLocation = 0 },
                    new GPUVertexAttribute { Format = GPUVertexFormatType.Float32x3, Offset = 3 * sizeof(float), ShaderLocation = 1 },
                };
                GPUVertexBufferLayout vertexBufferLayout = new GPUVertexBufferLayout();
                vertexBufferLayout.ArrayStride = 6 * sizeof(float);
                vertexBufferLayout.Attributes = attributes;

                GPUVertexBufferLayoutCollection buffers = new GPUVertexBufferLayoutCollection();
                buffers.Add(vertexBufferLayout);

                GPUVertexState vertexState = new GPUVertexState();
                vertexState.Module = _shaderModule;
                vertexState.EntryPoint = "vs_main";
                vertexState.Buffers = buffers;

                GPUColorTargetState colorTarget = new GPUColorTargetState();
                colorTarget.Format = canvasConfiguration.Format;

                GPUColorTargetStateCollection targets = new GPUColorTargetStateCollection();
                targets.Add(colorTarget);

                GPUFragmentState fragmentState = new GPUFragmentState();
                fragmentState.Module = _shaderModule;
                fragmentState.EntryPoint = "fs_main";
                fragmentState.Targets = targets;

                GPUPrimitiveState primitiveState = new GPUPrimitiveState();
                primitiveState.Topology = GPUPrimitiveTopologyType.TriangleList;

                GPURenderPipelineDescriptor pipelineDescriptor = new GPURenderPipelineDescriptor();
                pipelineDescriptor.Vertex = vertexState;
                pipelineDescriptor.Fragment = fragmentState;
                pipelineDescriptor.Primitive = primitiveState;
                _pipeline = device.CreateRenderPipeline(pipelineDescriptor);
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

            if (_uniformBuffer == null)
            {
                GPUBufferDescriptor uniformDescriptor = new GPUBufferDescriptor();
                uniformDescriptor.Size = 16 * sizeof(float);
                uniformDescriptor.Usage = GPUBufferUsageType.Uniform | GPUBufferUsageType.CopyDst;
                _uniformBuffer = device.CreateBuffer(uniformDescriptor);
            }

            {
                GPUBindGroupEntry entry = new GPUBindGroupEntry();
                entry.Binding = 0;
                entry.Resource = _uniformBuffer;

                GPUBindGroupEntryCollection entries = new GPUBindGroupEntryCollection();
                entries.Add(entry);

                GPUBindGroupLayout layout = _pipeline.GetBindGroupLayout(0);

                GPUBindGroupDescriptor bindGroupDescriptor = new GPUBindGroupDescriptor();
                bindGroupDescriptor.Layout = layout;
                bindGroupDescriptor.Entries = entries;
            }

            Matrix4x4 world = Matrix4x4.CreateRotationY(_angleY) * dc.world;
            Matrix4x4 worldViewProj = world * dc.view * dc.proj;
            float[] wvparray = MatrixToArray(worldViewProj);
            device.Queue.WriteBuffer<float>(_uniformBuffer, 0, wvparray);

            renderPass.SetPipeline(_pipeline);
            renderPass.SetVertexBuffer(0, _vertexBuffer);
            // TODO: set uniforms (worldViewProj)
            renderPass.Draw(3);
        }


        private float[] MatrixToArray(Matrix4x4 matrix)
        {
            return new float[]
            {
                matrix.M11, matrix.M12, matrix.M13, matrix.M14,
                matrix.M21, matrix.M22, matrix.M23, matrix.M24,
                matrix.M31, matrix.M32, matrix.M33, matrix.M34,
                matrix.M41, matrix.M42, matrix.M43, matrix.M44
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _shaderModule?.Dispose();
                _shaderModule = null;
                _vertexBuffer?.Dispose();
                _vertexBuffer = null;
                _pipeline?.Dispose();
                _pipeline = null;
                _uniformBuffer?.Dispose();
                _uniformBuffer = null;
            }

            base.Dispose(disposing);
        }
    }
}
