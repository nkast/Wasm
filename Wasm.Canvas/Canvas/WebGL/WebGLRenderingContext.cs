using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace nkast.Wasm.Canvas.WebGL
{
    internal class WebGLRenderingContext : RenderingContext, IWebGLRenderingContext, IDisposable
    {
        internal WebGLRenderingContext(Canvas canvas, int uid) : base(canvas, uid)
        {
        }

        public void Enable(WebGLCapability cap)
        {
            Invoke("nkCanvasGLContext.Enable", (int)cap);
        }

        public void Disable(WebGLCapability cap)
        {
            Invoke("nkCanvasGLContext.Disable", (int)cap);
        }

        public void BlendEquationSeparate(WebGLEquationFunc modeRGB, WebGLEquationFunc modeAlpha)
        {
            Invoke("nkCanvasGLContext.BlendEquationSeparate", modeRGB, modeAlpha);
        }

        public void BlendFuncSeparate(WebGLBlendFunc srcRGB, WebGLBlendFunc dstRGB, WebGLBlendFunc srcAlpha, WebGLBlendFunc dstAlpha)
        {
            Invoke("nkCanvasGLContext.BlendFuncSeparate", srcRGB, dstRGB, srcAlpha, dstAlpha);
        }

        public void BlendColor(float red, float green, float blue, float alpha)
        {
            Invoke("nkCanvasGLContext.BlendColor", red, green, blue, alpha);
        }

        public void ColorMask(bool red, bool green, bool blue, bool alpha)
        {
            Invoke("nkCanvasGLContext.ColorMask", red?1:0, green?1:0, blue?1:0, alpha?1:0);
        }

        public void CullFace(WebGLCullFaceMode mode)
        {
            Invoke("nkCanvasGLContext.CullFace", (int)mode);
        }

        public void FrontFace(WebGLWinding mode)
        {
            Invoke("nkCanvasGLContext.FrontFace", (int)mode);
        }

        public void PolygonOffset(float factor, float units)
        {
            Invoke("nkCanvasGLContext.PolygonOffset", factor, units);
        }

        public void DepthMask(bool enable)
        {
            Invoke("nkCanvasGLContext.DepthMask", enable?1:0);
        }

        public void StencilMask(int mask)
        {
            Invoke("nkCanvasGLContext.StencilMask", mask);
        }

        public void DepthFunc(WebGLDepthComparisonFunc func)
        {
            Invoke("nkCanvasGLContext.DepthFunc", (int)func);
        }

        public void StencilFunc(WebGLDepthComparisonFunc func, int StencilRef, int stencilMask)
        {
            Invoke("nkCanvasGLContext.StencilFunc", (int)func, StencilRef, stencilMask);
        }

        public void StencilOp(WebGLStencilOpFunc fail, WebGLStencilOpFunc zfail, WebGLStencilOpFunc zpass)
        {
            Invoke("nkCanvasGLContext.StencilOp", (int)fail, (int)zfail, (int)zpass);
        }

        public void Viewport(int x, int y, int width, int height)
        {
            Invoke("nkCanvasGLContext.Viewport", x, y, width, height);
        }

        public void DepthRange(float zNear, float zFar)
        {
            Invoke("nkCanvasGLContext.DepthRange", zNear, zFar);
        }

        public void Scissor(int x, int y, int width, int height)
        {
            Invoke("nkCanvasGLContext.Scissor", x, y, width, height);
        }


        public void ClearColor(float r, float g, float b, float a)
        {
            Invoke("nkCanvasGLContext.ClearColor", r, g, b, a);
        }

        public void ClearDepth(float depth)
        {
            Invoke("nkCanvasGLContext.ClearDepth", depth);
        }

        public void ClearStencil(int stencil)
        {
            Invoke("nkCanvasGLContext.ClearStencil", stencil);
        }

        public void Clear(WebGLBufferBits bufferBits)
        {
            Invoke("nkCanvasGLContext.Clear", (int)bufferBits);
        }

        public WebGLTexture CreateTexture()
        {
            int uid = InvokeRet<int>("nkCanvasGLContext.CreateTexture");
            return new WebGLTexture(uid, this);
        }

        internal void DeleteTexture(WebGLTexture texture)
        {
            Invoke("nkCanvasGLContext.DeleteTexture", texture.Uid);
        }

        public WebGLShader CreateShader(WebGLShaderType type)
        {
            int uid = InvokeRet<int, int>("nkCanvasGLContext.CreateShader", (int)type);
            return new WebGLShader(uid, this);
        }

        internal void DeleteShader(WebGLShader shader)
        {
            Invoke("nkCanvasGLContext.DeleteShader", shader.Uid);
        }

        public WebGLProgram CreateProgram()
        {
            int uid = InvokeRet<int>("nkCanvasGLContext.CreateProgram");
            return new WebGLProgram(uid, this);
        }

        internal void DeleteProgram(WebGLProgram program)
        {
            Invoke("nkCanvasGLContext.DeleteProgram", program.Uid);
        }

        public WebGLBuffer CreateBuffer()
        {
            int uid = InvokeRet<int>("nkCanvasGLContext.CreateBuffer");
            return new WebGLBuffer(uid, this);
        }

        internal void DeleteBuffer(WebGLBuffer buffer)
        {
            Invoke("nkCanvasGLContext.DeleteBuffer", buffer.Uid);
        }

        public WebGLFramebuffer CreateFramebuffer()
        {
            int uid = InvokeRet<int>("nkCanvasGLContext.CreateFramebuffer");
            return new WebGLFramebuffer(uid, this);
        }

        internal void DeleteFramebuffer(WebGLFramebuffer framebuffer)
        {
            Invoke("nkCanvasGLContext.DeleteFramebuffer", framebuffer.Uid);
        }

        public WebGLRenderbuffer CreateRenderbuffer()
        {
            int uid = InvokeRet<int>("nkCanvasGLContext.CreateRenderbuffer");
            return new WebGLRenderbuffer(uid, this);
        }

        internal void DeleteRenderbuffer(WebGLRenderbuffer renderbuffer)
        {
            Invoke("nkCanvasGLContext.DeleteRenderbuffer", renderbuffer.Uid);
        }

        public void ShaderSource(WebGLShader shader, string source)
        {
            Invoke("nkCanvasGLContext.ShaderSource", shader.Uid, source);
        }

        public void CompileShader(WebGLShader shader)
        {
            Invoke("nkCanvasGLContext.CompileShader", shader.Uid);
        }

        public bool GetShaderParameter(WebGLShader shader, WebGLShaderStatus pname)
        {
            return InvokeRet<int, int, bool>("nkCanvasGLContext.GetShaderParameter", shader.Uid, (int)pname);
        }

        public bool GetProgramParameter(WebGLProgram program, WebGLProgramStatus pname)
        {
            return InvokeRet<int, int, bool>("nkCanvasGLContext.GetProgramParameter", program.Uid, (int)pname);
        }

        public void TexImage2D(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, WebGLFormat format, WebGLTexelType type)
        {
            Invoke("nkCanvasGLContext.TexImage2D", (int)target, level, (int)internalFormat, width, height, (int)format, (int)type);
        }

        public void TexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels)
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.TexImage2D1", (int)target, level, (int)internalFormat, width, height, (int)format, (int)type, stride, pixels);
        }

        public void TexSubImage2D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels) 
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int>(xoffset, yoffset);
            Invoke("nkCanvasGLContext.TexSubImage2D1", (int)target, level, position, width, height, (int)format, (int)type, stride, pixels);
        }

        public void CompressedTexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, TData[] pixels)
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.CompressedTexImage2D", (int)target, level, (int)internalFormat, width, height, stride, pixels);
        }

        public void CompressedTexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, TData[] pixels, int index, int count)
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.CompressedTexImage2D1", (int)target, level, (int)internalFormat, width, height, stride, pixels, index, count);
        }

        public void TexParameter(WebGLTextureTarget target, WebGLTexParamName pname, WebGLTexParam param)
        {
            Invoke("nkCanvasGLContext.TexParameteri", (int)target, (int)pname, (int)param);
        }

        public void PixelStore(WebGLPixelParameter pname, int param)
        {
            Invoke("nkCanvasGLContext.PixelStorei", (int)pname, param);
        }

        public void BindTexture(WebGLTextureTarget target, WebGLTexture texture)
        {
            int uid = (texture != null) ? texture.Uid : -1;
            Invoke("nkCanvasGLContext.BindTexture", (int)target, uid);
        }

        public void BindBuffer(WebGLBufferType type, WebGLBuffer buffer)
        {
            Invoke("nkCanvasGLContext.BindBuffer", (int)type, buffer.Uid);
        }

        public void BindFramebuffer(WebGLFramebufferType type, WebGLFramebuffer framebuffer)
        {
            int uid = (framebuffer != null) ? framebuffer.Uid : -1;
            Invoke("nkCanvasGLContext.BindFramebuffer", (int)type, uid);
        }

        public void BindRenderbuffer(WebGLRenderbufferType type, WebGLRenderbuffer renderbuffer)
        {
            Invoke("nkCanvasGLContext.BindRenderbuffer", (int)type, renderbuffer.Uid);
        }

        public void FramebufferRenderbuffer(WebGLFramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLRenderbufferType renderbuffertarget, WebGLRenderbuffer renderbuffer)
        {
            int uid = (renderbuffer != null) ? renderbuffer.Uid : -1;
            Invoke("nkCanvasGLContext.FramebufferRenderbuffer", (int)target, (int)attachment, (int)renderbuffertarget, uid);
        }

        public void FramebufferTexture2D(WebGLFramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLTextureTarget texturetarget, WebGLTexture texture)
        {
            Invoke("nkCanvasGLContext.FramebufferTexture2D", (int)target, (int)attachment, (int)texturetarget, texture.Uid);
        }

        public void RenderbufferStorage(WebGLRenderbufferType target, WebGLRenderbufferInternalFormat internalFormat, int width, int height)
        {
            Invoke("nkCanvasGLContext.RenderbufferStorage", (int)target, (int)internalFormat, width, height);
        }

        public WebGLFramebufferStatus CheckFramebufferStatus(WebGLFramebufferType target)
        {
            return (WebGLFramebufferStatus)InvokeRet<int, int>("nkCanvasGLContext.CheckFramebufferStatus", (int)target);
        }

        public void GenerateMipmap(WebGLTextureTarget target)
        {
            Invoke("nkCanvasGLContext.GenerateMipmap", (int)target);
        }

        public void AttachShader(WebGLProgram program, WebGLShader shader)
        {
            Invoke("nkCanvasGLContext.AttachShader", program.Uid, shader.Uid);
        }

        public string GetProgramInfoLog(WebGLProgram program)
        {
            return InvokeRet<int, string>("nkCanvasGLContext.GetProgramInfoLog", program.Uid);
        }

        public string GetShaderInfoLog(WebGLShader shader)
        {
            return InvokeRet<int, string>("nkCanvasGLContext.GetShaderInfoLog", shader.Uid);
        }

        public int GetAttribLocation(WebGLProgram program, string name)
        {
            return InvokeRet<int, string, int>("nkCanvasGLContext.GetAttribLocation", program.Uid, name);
        }

        public WebGLUniformLocation GetUniformLocation(WebGLProgram program, string name)
        {
            int uid = InvokeRet<int, string, int>("nkCanvasGLContext.GetUniformLocation", program.Uid, name);
            if (uid == -1)
                return null;
            return new WebGLUniformLocation(uid, this);
        }

        public void Uniform1i(WebGLUniformLocation location, int v0)
        {
            Invoke("nkCanvasGLContext.Uniform1i", location.Uid, v0);
        }

        public void Uniform2i(WebGLUniformLocation location, int v0, int v1)
        {
            Invoke("nkCanvasGLContext.Uniform2i", location.Uid, v0, v1);
        }

        public void Uniform3i(WebGLUniformLocation location, int v0, int v1, int v2)
        {
            Invoke("nkCanvasGLContext.Uniform3i", location.Uid, v0, v1, v2);
        }

        public void Uniform4i(WebGLUniformLocation location, int v0, int v1, int v2, int v3)
        {
            Invoke("nkCanvasGLContext.Uniform4i", location.Uid, v0, v1, v2, v3);
        }

        public void Uniform1f(WebGLUniformLocation location, float v0)
        {
            Invoke("nkCanvasGLContext.Uniform1f", location.Uid, v0);
        }
        public void Uniform2f(WebGLUniformLocation location, float v0, float v1)
        {
            Invoke("nkCanvasGLContext.Uniform2f", location.Uid, v0, v1);
        }
        public void Uniform3f(WebGLUniformLocation location, float v0, float v1, float v2)
        {
            Invoke("nkCanvasGLContext.Uniform3f", location.Uid, v0, v1, v2);
        }

        public void Uniform4f(WebGLUniformLocation location, float v0, float v1, float v2, float v3)
        {
            Invoke("nkCanvasGLContext.Uniform4f", location.Uid, v0, v1, v2, v3);
        }

        public void Uniform1iv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.Uniform1iv", location.Uid, stride, value);
        }
        public void Uniform2iv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.Uniform2iv", location.Uid, stride, value);
        }
        public void Uniform3iv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.Uniform3iv", location.Uid, stride, value);
        }
        public void Uniform4iv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.Uniform4iv", location.Uid, stride, value);
        }

        public void Uniform1fv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.Uniform1fv", location.Uid, stride, value);
        }
        public void Uniform2fv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.Uniform2fv", location.Uid, stride, value);
        }
        public void Uniform3fv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.Uniform3fv", location.Uid, stride, value);
        }
        public void Uniform4fv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.Uniform4fv", location.Uid, stride, value);
        }
        
        public void UniformMatrix2fv<TData>(WebGLUniformLocation location, TData[] value) 
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.UniformMatrix2fv", location.Uid, stride, value);
        }

        public void UniformMatrix3fv<TData>(WebGLUniformLocation location, TData[] value) 
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.UniformMatrix3fv", location.Uid, stride, value);
        }

        public void UniformMatrix4fv<TData>(WebGLUniformLocation location, TData[] value) 
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.UniformMatrix4fv", location.Uid, stride, value);
        }

        public void LinkProgram(WebGLProgram program)
        {
            Invoke("nkCanvasGLContext.LinkProgram", program.Uid);
        }

        public void BufferData(WebGLBufferType type, int size, WebGLBufferUsageHint usage)
        {
            Invoke("nkCanvasGLContext.BufferData", (int)type, size, (int)usage);
        }

        public void BufferData<TData>(WebGLBufferType type, TData[] data, WebGLBufferUsageHint usage)
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.BufferData1", (int)type, (int)usage, stride, data);
        }

        public void BufferSubData<TData>(WebGLBufferType target, int offset, TData[] srcData, int length)
            where TData : struct
        {
            var stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGLContext.BufferSubData", (int)target, offset, length, stride, srcData);
        }

        public void VertexAttribPointer(int index, int size, WebGLDataType type, bool normalized, int stride, int offset)
        {
            Invoke("nkCanvasGLContext.VertexAttribPointer", index, size, (int)type, normalized?1:0, stride, offset);
        }

        public void EnableVertexAttribArray(int index)
        {
            Invoke("nkCanvasGLContext.EnableVertexAttribArray", index);
        }

        public void DisableVertexAttribArray(int index)
        {
            Invoke("nkCanvasGLContext.DisableVertexAttribArray", index);
        }

        public void UseProgram(WebGLProgram program)
        {
            Invoke("nkCanvasGLContext.UseProgram", program.Uid);
        }

        public void ActiveTexture(WebGLTextureUnit textureUnit)
        {
            Invoke("nkCanvasGLContext.ActiveTexture", (int)textureUnit);
        }

        public void DrawArrays(WebGLPrimitiveType mode, int first, int count)
        {
            Invoke("nkCanvasGLContext.DrawArrays", (int)mode, first, count);
        }

        public void DrawElements(WebGLPrimitiveType mode, int count, WebGLDataType type, int offset)
        {
            Invoke("nkCanvasGLContext.DrawElements", (int)mode, count, (int)type, offset);
        }

        public void Flush()
        {
            Invoke("nkCanvasGLContext.Flush");
        }

        public void Finish()
        {
            Invoke("nkCanvasGLContext.Finish");
        }

        public bool GetExtension(string name)
        {
            return InvokeRet<string, bool>("nkCanvasGLContext.GetExtension", name);
        }

        public WebGLErrorCode GetError()
        {   
            return (WebGLErrorCode)InvokeRet<int>("nkCanvasGLContext.GetError");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            //Invoke("nkCanvasGLContext.DisposeObject"); // DisposeWebGLContext
            base.Dispose(disposing);
        }

    }
}
