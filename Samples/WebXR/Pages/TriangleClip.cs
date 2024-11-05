using System;
using System.Numerics;
using nkast.Wasm.Canvas.WebGL;
using WebXR.Engine;

namespace WebXR.Pages
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


        private const string VS_SOURCE = "attribute vec3 aPos;" +
                                         "attribute vec3 aColor;" +
                                         "varying vec3 vColor;" +

                                         "uniform mat4 uWorldViewProj; "+

                                         "void main() {" +
                                            "gl_Position =  uWorldViewProj * vec4(aPos, 1.0);" +
                                            "vColor = aColor;" +
                                         "}";

        private const string FS_SOURCE = "precision mediump float;" +
                                         "varying vec3 vColor;" +

                                         "void main() {" +
                                            "gl_FragColor = vec4(vColor, 1.0);" +
                                         "}";


        private void DrawTriangle(DrawContext dc)
        {
            var gl = dc.GLContext;

            using (WebGLProgram program = InitProgram(gl, VS_SOURCE, FS_SOURCE))
            using (WebGLBuffer vertexBuffer = gl.CreateBuffer())
            {                
                gl.BindBuffer(WebGLBufferType.ARRAY, vertexBuffer);

                float[] vertices = new[]
                {
                    -0.5f, -0.5f, 0.0f,   1.0f, 0.0f, 0.0f,
                     0.0f,  0.5f, 0.0f,   0.0f, 0.0f, 0.0f,
                     0.5f, -0.5f, 0.0f,   0.0f, 1.0f, 0.0f
                };
                gl.BufferData(WebGLBufferType.ARRAY, vertices, WebGLBufferUsageHint.STATIC_DRAW);

                gl.VertexAttribPointer(0, 3, WebGLDataType.FLOAT, false, 6 * sizeof(float), 0);
                gl.VertexAttribPointer(1, 3, WebGLDataType.FLOAT, false, 6 * sizeof(float), 3 * sizeof(float));
                gl.EnableVertexAttribArray(0);
                gl.EnableVertexAttribArray(1);

                gl.UseProgram(program);

                Matrix4x4 worldViewProj = dc.world * dc.view * dc.proj;
                float[] wvparray = MatrixToArray(worldViewProj);
                using (WebGLUniformLocation wvplocation = gl.GetUniformLocation(program, "uWorldViewProj"))
                {
                    gl.UniformMatrix4fv<float>(wvplocation, wvparray);
                }

                gl.DrawArrays(WebGLPrimitiveType.TRIANGLES, 0, 3);
            }
            
        }

        private float[] MatrixToArray(Matrix4x4 matrix)
        {
            return new[]
            {
                matrix.M11, matrix.M12, matrix.M13, matrix.M14,
                matrix.M21, matrix.M22, matrix.M23, matrix.M24,
                matrix.M31, matrix.M32, matrix.M33, matrix.M34,
                matrix.M41, matrix.M42, matrix.M43, matrix.M44
            };
        }

        private WebGLProgram InitProgram(IWebGLRenderingContext gl, string vsSource, string fsSource)
        {
            WebGLShader vertexShader = LoadShader(gl, WebGLShaderType.VERTEX, vsSource);
            var fragmentShader = LoadShader(gl, WebGLShaderType.FRAGMENT, fsSource);

            WebGLProgram program = gl.CreateProgram();
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
