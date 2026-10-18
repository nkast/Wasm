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
        GPUBindGroup _bindGroup;
        GPUTexture _texture;
        GPUSampler _sampler;
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


        private const string SHADER_SOURCE  = "@group(0) @binding(0) var<uniform> uWorldViewProj : mat4x4f;"
                                            + "@group(0) @binding(1) var uTexture : texture_2d<f32>;"
                                            + "@group(0) @binding(2) var uSampler : sampler;"
                                            + "struct VSOut"
                                            + "{"
                                            + "    @builtin(position) pos : vec4f,"
                                            + "    @location(0) color : vec3f,"
                                            + "    @location(1) uv : vec2f,"
                                            + "};"
                                            + ""
                                            + "@vertex fn vs_main(@location(0) aPos : vec3f, @location(1) aColor : vec3f, @location(2) aUV : vec2f) -> VSOut"
                                            + "{"
                                            + "    var o : VSOut;"
                                            + "    o.pos = uWorldViewProj * vec4f(aPos, 1.0);"
                                            + "    o.color = aColor;"
                                            + "    o.uv = aUV;"
                                            + "    return o;"
                                            + "}"
                                            + ""
                                            + "@fragment fn fs_main(@location(0) vColor : vec3f, @location(1) vUV : vec2f) -> @location(0) vec4f"
                                            + "{"
                                            + "    return vec4f(vColor, 1.0) * textureSample(uTexture, uSampler, vUV);"
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
                    new GPUVertexAttribute { Format = GPUVertexFormatType.Float32x2, Offset = 6 * sizeof(float), ShaderLocation = 2 },
                };
                GPUVertexBufferLayout vertexBufferLayout = new GPUVertexBufferLayout();
                vertexBufferLayout.ArrayStride = 8 * sizeof(float);
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
                    -0.5f, -0.5f, 0.0f,   1.0f, 1.0f, 1.0f,   0.0f, 1.0f,
                     0.0f,  0.5f, 0.0f,   1.0f, 1.0f, 1.0f,   0.5f, 0.0f,
                     0.5f, -0.5f, 0.0f,   1.0f, 1.0f, 1.0f,   1.0f, 1.0f
                };

                GPUBufferDescriptor bufferDescriptor = new GPUBufferDescriptor();
                bufferDescriptor.Size = vertices.Length * sizeof(float);
                bufferDescriptor.Usage = GPUBufferUsageType.Vertex | GPUBufferUsageType.CopyDst;
                _vertexBuffer = device.CreateBuffer(bufferDescriptor);
                device.Queue.WriteBuffer<float>(_vertexBuffer, 0, vertices);
            }

            if (_texture == null)
            {
                const int TEXTURE_SIZE = 8;
                GPUTextureDescriptor textureDescriptor = new GPUTextureDescriptor();
                textureDescriptor.Format = GPUTextureFormat.Rgba8Unorm;
                textureDescriptor.Usage = GPUTextureUsageType.TextureBinding | GPUTextureUsageType.CopyDst;
                textureDescriptor.Size.Width = TEXTURE_SIZE;
                textureDescriptor.Size.Height = TEXTURE_SIZE;
                _texture = device.CreateTexture(textureDescriptor);

                byte[] pixels = new byte[TEXTURE_SIZE * TEXTURE_SIZE * 4];
                for (int y = 0; y < TEXTURE_SIZE; y++)
                {
                    for (int x = 0; x < TEXTURE_SIZE; x++)
                    {
                        int i = (y * TEXTURE_SIZE + x) * 4;
                        bool checker = ((x / 2) + (y / 2)) % 2 == 0;
                        pixels[i + 0] = (byte)(x * 255 / (TEXTURE_SIZE - 1));
                        pixels[i + 1] = (byte)(y * 255 / (TEXTURE_SIZE - 1));
                        pixels[i + 2] = checker ? (byte)255 : (byte)0;
                        pixels[i + 3] = 255;
                    }
                }
                device.Queue.WriteTexture<byte>(
                    new GPUTextureCopyLocation { Texture = _texture },
                    pixels,
                    new GPUTextureDataLayout { BytesPerRow = TEXTURE_SIZE * 4 },
                    new GPUTextureSize(TEXTURE_SIZE, TEXTURE_SIZE));
            }

            if (_sampler == null)
            {
                GPUSamplerDescriptor samplerDescriptor = new GPUSamplerDescriptor();
                samplerDescriptor.MagFilter = GPUFilterType.Nearest;
                samplerDescriptor.MinFilter = GPUFilterType.Nearest;
                samplerDescriptor.LodMaxClamp = 32f;
                samplerDescriptor.MaxAnisotropy = 1;
                _sampler = device.CreateSampler(samplerDescriptor);
            }

            if (_uniformBuffer == null)
            {
                GPUBufferDescriptor uniformDescriptor = new GPUBufferDescriptor();
                uniformDescriptor.Size = 16 * sizeof(float);
                uniformDescriptor.Usage = GPUBufferUsageType.Uniform | GPUBufferUsageType.CopyDst;
                _uniformBuffer = device.CreateBuffer(uniformDescriptor);
            }

            if (_bindGroup == null)
            {
                GPUBindGroupEntry entry = new GPUBindGroupEntry();
                entry.Binding = 0;
                entry.Resource = _uniformBuffer;

                GPUBindGroupEntry textureEntry = new GPUBindGroupEntry();
                textureEntry.Binding = 1;
                textureEntry.Resource = _texture.CreateView();

                GPUBindGroupEntry samplerEntry = new GPUBindGroupEntry();
                samplerEntry.Binding = 2;
                samplerEntry.Resource = _sampler;

                GPUBindGroupEntryCollection entries = new GPUBindGroupEntryCollection();
                entries.Add(entry);
                entries.Add(textureEntry);
                entries.Add(samplerEntry);

                GPUBindGroupLayout layout = _pipeline.GetBindGroupLayout(0);

                GPUBindGroupDescriptor bindGroupDescriptor = new GPUBindGroupDescriptor();
                bindGroupDescriptor.Layout = layout;
                bindGroupDescriptor.Entries = entries;
                _bindGroup = device.CreateBindGroup(bindGroupDescriptor);
            }

            Matrix4x4 world = Matrix4x4.CreateRotationY(_angleY) * dc.world;
            Matrix4x4 worldViewProj = world * dc.view * dc.proj;
            float[] wvparray = MatrixToArray(worldViewProj);
            device.Queue.WriteBuffer<float>(_uniformBuffer, 0, wvparray);

            renderPass.SetPipeline(_pipeline);
            renderPass.SetVertexBuffer(0, _vertexBuffer);
            renderPass.SetBindGroup(0, _bindGroup);
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
                _bindGroup?.Dispose();
                _bindGroup = null;
                _texture?.Dispose();
                _texture = null;
                _sampler?.Dispose();
                _sampler = null;
                _uniformBuffer?.Dispose();
                _uniformBuffer = null;
            }

            base.Dispose(disposing);
        }
    }
}
