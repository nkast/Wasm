using System;
using System.Numerics;
using nkast.Wasm.Canvas.WebGL;
using CanvasGL.Engine;

namespace CanvasGL.Pages
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
            var gl = dc.GLContext;

            if (dc.Layer == 0)
            {
                DrawTriangle(gl);

            }

            base.Draw(dc);
        }


        private const string VS_SOURCE = "attribute vec3 aPos;" +
                                         "attribute vec3 aColor;" +
                                         "varying vec3 vColor;" +

                                         "void main() {" +
                                            "gl_Position = vec4(aPos, 1.0);" +
                                            "vColor = aColor;" +
                                         "}";

        private const string FS_SOURCE = "precision mediump float;" +
                                         "varying vec3 vColor;" +

                                         "void main() {" +
                                            "gl_FragColor = vec4(vColor, 1.0);" +
                                         "}";


        private void DrawTriangle(IWebGLRenderingContext gl)
        {
            using (var program = InitProgram(gl, VS_SOURCE, FS_SOURCE))
            using (var vertexBuffer = gl.CreateBuffer())
            {                
                gl.BindBuffer(WebGLBufferType.ARRAY, vertexBuffer);

                var vertices = new[]
                {
                    -0.5f, -0.5f, 0.0f,   1.0f, 0.0f, 0.0f,
                     0.0f,  0.5f, 0.0f,   0.0f, 0.0f, 1.0f,
                     0.5f, -0.5f, 0.0f,   0.0f, 1.0f, 0.0f
                };
                gl.BufferData(WebGLBufferType.ARRAY, vertices, WebGLBufferUsageHint.STATIC_DRAW);

                var data0 = new float[vertices.Length];
                ((IWebGL2RenderingContext)gl).GetBufferSubData(WebGLBufferType.ARRAY, 0, data0);

                var data0b = new float[vertices.Length];
                ((IWebGL2RenderingContext)gl).GetBufferSubData(WebGLBufferType.ARRAY, 3*sizeof(float), data0b);

                var data1 = new float[vertices.Length];
                ((IWebGL2RenderingContext)gl).GetBufferSubData(WebGLBufferType.ARRAY, 3 * sizeof(float), data1, startIndex:3);

                var data2 = new float[vertices.Length];
                ((IWebGL2RenderingContext)gl).GetBufferSubData(WebGLBufferType.ARRAY, 3 * sizeof(float), data2, startIndex: 3, 3);

                gl.VertexAttribPointer(0, 3, WebGLDataType.FLOAT, false, 6 * sizeof(float), 0);
                gl.VertexAttribPointer(1, 3, WebGLDataType.FLOAT, false, 6 * sizeof(float), 3 * sizeof(float));
                gl.EnableVertexAttribArray(0);
                gl.EnableVertexAttribArray(1);

                gl.UseProgram(program);

                gl.DrawArrays(WebGLPrimitiveType.TRIANGLES, 0, 3);
            }
            
        }

        private WebGLProgram InitProgram(IWebGLRenderingContext gl, string vsSource, string fsSource)
        {
            var vertexShader = LoadShader(gl, WebGLShaderType.VERTEX, vsSource);
            var fragmentShader = LoadShader(gl, WebGLShaderType.FRAGMENT, fsSource);

            var program = gl.CreateProgram();
            gl.AttachShader(program, vertexShader);
            gl.AttachShader(program, fragmentShader);
            gl.LinkProgram(program);

            vertexShader.Dispose();
            fragmentShader.Dispose();

            if (!gl.GetProgramParameter(program, WebGLProgramStatus.LINK))
            {
                string info = "An error occured while linking the program";
                info += ": " + gl.GetProgramInfoLog(program);
                throw new Exception(info);
            }

            return program;
        }

        private WebGLShader LoadShader(IWebGLRenderingContext gl, WebGLShaderType type, string source)
        {
            var shader = gl.CreateShader(type);

            gl.ShaderSource(shader, source);
            gl.CompileShader(shader);

            if (!gl.GetShaderParameter(shader, WebGLShaderStatus.COMPILE))
            {
                string info = "An error occured while compiling the shader";
                info += ": " + gl.GetShaderInfoLog(shader);
                shader.Dispose();
                throw new Exception(info);
            }

            return shader;
        }
    }
}
