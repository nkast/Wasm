using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas.WebGL
{
    internal class WebGLRenderingContext : RenderingContext, IWebGLRenderingContext, IDisposable
    {
        WebGLPolygonModeExtension _polygonModeExtension;

        internal WebGLRenderingContext(Canvas canvas, int uid) : base(canvas, uid)
        {
        }

        public WebGLPolygonModeExtension PolygonModeExtension
        {
            get
            {
                if (_polygonModeExtension == null)
                    _polygonModeExtension = GetExtension<WebGLPolygonModeExtension>("WEBGL_polygon_mode");

                return _polygonModeExtension;
            }
        }

        public void Enable(WebGLCapability cap)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.Enable"), (int)cap);
        }

        public void Disable(WebGLCapability cap)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.Disable"), (int)cap);
        }

        public void BlendEquationSeparate(WebGLEquationFunc modeRGB, WebGLEquationFunc modeAlpha)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.BlendEquationSeparate"), modeRGB, modeAlpha);
        }

        public void BlendFuncSeparate(WebGLBlendFunc srcRGB, WebGLBlendFunc dstRGB, WebGLBlendFunc srcAlpha, WebGLBlendFunc dstAlpha)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.BlendFuncSeparate"), srcRGB, dstRGB, srcAlpha, dstAlpha);
        }

        public void BlendColor(float red, float green, float blue, float alpha)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.BlendColor"), red, green, blue, alpha);
        }

        public void ColorMask(bool red, bool green, bool blue, bool alpha)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.ColorMask"), red?1:0, green?1:0, blue?1:0, alpha?1:0);
        }

        public void CullFace(WebGLCullFaceMode mode)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.CullFace"), (int)mode);
        }

        public void FrontFace(WebGLWinding mode)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.FrontFace"), (int)mode);
        }

        public void PolygonOffset(float factor, float units)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.PolygonOffset"), factor, units);
        }

        public void DepthMask(bool enable)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.DepthMask"), enable ?1:0);
        }

        public void StencilMask(int mask)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.StencilMask"), mask);
        }

        public void StencilMaskSeparate(WebGLCullFaceMode mode, int mask)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.StencilMaskSeparate"), (int)mode, mask);
        }

        public void DepthFunc(WebGLDepthComparisonFunc func)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.DepthFunc"), (int)func);
        }

        public void StencilFunc(WebGLDepthComparisonFunc func, int StencilRef, int stencilMask)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.StencilFunc"), (int)func, StencilRef, stencilMask);
        }

        public void StencilFuncSeparate(WebGLCullFaceMode mode, WebGLDepthComparisonFunc func, int stencilRef, int stencilMask)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.StencilFuncSeparate"), (int)mode, (int)func, stencilRef, stencilMask);
        }

        public void StencilOp(WebGLStencilOpFunc fail, WebGLStencilOpFunc zfail, WebGLStencilOpFunc zpass)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.StencilOp"), (int)fail, (int)zfail, (int)zpass);
        }

        public void StencilOpSeparate(WebGLCullFaceMode mode, WebGLStencilOpFunc fail, WebGLStencilOpFunc zfail, WebGLStencilOpFunc zpass)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.StencilOpSeparate"), (int)mode, (int)fail, (int)zfail, (int)zpass);
        }

        public void Viewport(int x, int y, int width, int height)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.Viewport"), x, y, width, height);
        }

        public void DepthRange(float zNear, float zFar)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.DepthRange"), zNear, zFar);
        }

        public void Scissor(int x, int y, int width, int height)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.Scissor"), x, y, width, height);
        }


        public void ClearColor(float r, float g, float b, float a)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.ClearColor"), r, g, b, a);
        }

        public void ClearDepth(float depth)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.ClearDepth"), depth);
        }

        public void ClearStencil(int stencil)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.ClearStencil"), stencil);
        }

        public void Clear(WebGLBufferBits bufferBits)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.Clear"), (int)bufferBits);
        }

        public int GetParameter(WebGLPNameInteger pname)
        {
            return InvokeRetInt<int>(RegisterFunction("nkCanvasGLContext.GetParameterInt"), (int)pname);
        }

        public string GetParameter(WebGLPNameString pname)
        {
            return InvokeRetString<int>(RegisterFunction("nkCanvasGLContext.GetParameterString"), (int)pname);
        }

        public WebGLTexture CreateTexture()
        {
            int uid = InvokeRetInt(RegisterFunction("nkCanvasGLContext.CreateTexture"));
            return new WebGLTexture(uid, this);
        }

        internal void DeleteTexture(WebGLTexture texture)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.DeleteTexture"), texture.Uid);
        }

        public WebGLShader CreateShader(WebGLShaderType type)
        {
            int uid = InvokeRetInt<int>(RegisterFunction("nkCanvasGLContext.CreateShader"), (int)type);
            return new WebGLShader(uid, this);
        }

        internal void DeleteShader(WebGLShader shader)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.DeleteShader"), shader.Uid);
        }

        public WebGLProgram CreateProgram()
        {
            int uid = InvokeRetInt(RegisterFunction("nkCanvasGLContext.CreateProgram"));
            return new WebGLProgram(uid, this);
        }

        internal void DeleteProgram(WebGLProgram program)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.DeleteProgram"), program.Uid);
        }

        public WebGLBuffer CreateBuffer()
        {
            int uid = InvokeRetInt(RegisterFunction("nkCanvasGLContext.CreateBuffer"));
            return new WebGLBuffer(uid, this);
        }

        internal void DeleteBuffer(WebGLBuffer buffer)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.DeleteBuffer"), buffer.Uid);
        }

        public WebGLFramebuffer CreateFramebuffer()
        {
            int uid = InvokeRetInt(RegisterFunction("nkCanvasGLContext.CreateFramebuffer"));
            return new WebGLFramebuffer(uid, this);
        }

        internal void DeleteFramebuffer(WebGLFramebuffer framebuffer)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.DeleteFramebuffer"), framebuffer.Uid);
        }

        public WebGLRenderbuffer CreateRenderbuffer()
        {
            int uid = InvokeRetInt(RegisterFunction("nkCanvasGLContext.CreateRenderbuffer"));
            return new WebGLRenderbuffer(uid, this);
        }

        internal void DeleteRenderbuffer(WebGLRenderbuffer renderbuffer)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.DeleteRenderbuffer"), renderbuffer.Uid);
        }

        public void ShaderSource(WebGLShader shader, string source)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.ShaderSource"), shader.Uid, source);
        }

        public void CompileShader(WebGLShader shader)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.CompileShader"), shader.Uid);
        }

        public bool GetShaderParameter(WebGLShader shader, WebGLShaderStatus pname)
        {
            return InvokeRetBool<int, int>(RegisterFunction("nkCanvasGLContext.GetShaderParameter"), shader.Uid, (int)pname);
        }

        public bool GetProgramParameter(WebGLProgram program, WebGLProgramStatus pname)
        {
            return InvokeRetBool<int, int>(RegisterFunction("nkCanvasGLContext.GetProgramParameter"), program.Uid, (int)pname);
        }

        public void TexImage2D(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, WebGLFormat format, WebGLTexelType type)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.TexImage2D"), (int)target, level, (int)internalFormat, width, height, (int)format, (int)type);
        }

        public unsafe void TexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.TexImage2D1"), (int)target, level, (int)internalFormat, width, height, (int)format, (int)type, stride, (int)pPixels, pixels.Length);
            }
        }

        public unsafe void TexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, WebGLFormat format, WebGLTexelType type, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.TexImage2D1"), (int)target, level, (int)internalFormat, width, height, (int)format, (int)type, stride, (int)pPixels, pixels.Length);
            }
        }

        public unsafe void TexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.TexImage2D3"), (int)target, level, (int)internalFormat, width, height, (int)format, (int)type, stride, (int)pPixels, index, count);
            }
        }

        public void TexImage2D(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, WebGLFormat format, WebGLTexelType type, Video video)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.TexImage2D2"), (int)target, level, (int)internalFormat,  (int)format, (int)type, video.Uid);
        }

        public unsafe void TexSubImage2D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels) 
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int>(xoffset, yoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.TexSubImage2D1"), (int)target, level, position, width, height, (int)format, (int)type, stride, (int)pPixels, pixels.Length);
            }
        }

        public unsafe void TexSubImage2D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int width, int height, WebGLFormat format, WebGLTexelType type, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int>(xoffset, yoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.TexSubImage2D1"), (int)target, level, position, width, height, (int)format, (int)type, stride, (int)pPixels, pixels.Length);
            }
        }

        public unsafe void TexSubImage2D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int>(xoffset, yoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.TexSubImage2D2"), (int)target, level, position, width, height, (int)format, (int)type, stride, (int)pPixels, index, count);
            }
        }

        public unsafe void CompressedTexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.CompressedTexImage2D"), (int)target, level, (int)internalFormat, width, height, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.CompressedTexImage2D"), (int)target, level, (int)internalFormat, width, height, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.CompressedTexImage2D"), (int)target, level, (int)internalFormat, width, height, stride, (int)pPixels, index, count);
            }
        }

        public unsafe void CompressedTexSubImage2D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int width, int height, WebGLFormat format, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int>(xoffset, yoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.CompressedTexSubImage2D"), (int)target, level, position, width, height, (int)format, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexSubImage2D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int width, int height, WebGLFormat format, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int>(xoffset, yoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.CompressedTexSubImage2D"), (int)target, level, position, width, height, (int)format, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexSubImage2D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int width, int height, WebGLFormat format, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int>(xoffset, yoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.CompressedTexSubImage2D"), (int)target, level, position, width, height, (int)format, stride, (int)pPixels, index, count);
            }
        }

        public unsafe void ReadPixels<TData>(int x, int y, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.ReadPixels"), x, y, width, height, format, type, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void ReadPixels<TData>(int x, int y, int width, int height, WebGLFormat format, WebGLTexelType type, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.ReadPixels"), x, y, width, height, format, type, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void ReadPixels<TData>(int x, int y, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.ReadPixels"), x, y, width, height, format, type, stride, (int)pPixels, index, count);
            }
        }

        public void TexParameter(WebGLTextureTarget target, WebGLTexParamName pname, WebGLTexParam param)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.TexParameteri"), (int)target, (int)pname, (int)param);
        }

        public void TexParameter(WebGLTextureTarget target, WebGLTexParamName pname, float param)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.TexParameterf"), (int)target, (int)pname, param);
        }

        public void PixelStore(WebGLPixelParameter pname, int param)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.PixelStorei"), (int)pname, param);
        }

        public void BindTexture(WebGLTextureTarget target, WebGLTexture texture)
        {
            int uid = (texture != null) ? texture.Uid : -1;
            Invoke(RegisterFunction("nkCanvasGLContext.BindTexture"), (int)target, uid);
        }

        public void BindBuffer(WebGLBufferType type, WebGLBuffer buffer)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.BindBuffer"), (int)type, buffer.Uid);
        }

        public void BindFramebuffer(WebGLFramebufferType type, WebGLFramebuffer framebuffer)
        {
            int uid = (framebuffer != null) ? framebuffer.Uid : -1;
            Invoke(RegisterFunction("nkCanvasGLContext.BindFramebuffer"), (int)type, uid);
        }

        public void BindRenderbuffer(WebGLRenderbufferType type, WebGLRenderbuffer renderbuffer)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.BindRenderbuffer"), (int)type, renderbuffer.Uid);
        }

        public void FramebufferRenderbuffer(WebGLFramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLRenderbufferType renderbuffertarget, WebGLRenderbuffer renderbuffer)
        {
            int uid = (renderbuffer != null) ? renderbuffer.Uid : -1;
            Invoke(RegisterFunction("nkCanvasGLContext.FramebufferRenderbuffer"), (int)target, (int)attachment, (int)renderbuffertarget, uid);
        }

        public void FramebufferTexture2D(WebGLFramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLTextureTarget texturetarget, WebGLTexture texture)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.FramebufferTexture2D"), (int)target, (int)attachment, (int)texturetarget, texture.Uid);
        }

        public void FramebufferTexture2D(WebGLFramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLTextureTarget texturetarget, WebGLTexture texture, int level)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.FramebufferTexture2D1"), (int)target, (int)attachment, (int)texturetarget, texture.Uid, level);
        }

        public void RenderbufferStorage(WebGLRenderbufferType target, WebGLRenderbufferInternalFormat internalFormat, int width, int height)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.RenderbufferStorage"), (int)target, (int)internalFormat, width, height);
        }

        public WebGLFramebufferStatus CheckFramebufferStatus(WebGLFramebufferType target)
        {
            return (WebGLFramebufferStatus)InvokeRetInt<int>(RegisterFunction("nkCanvasGLContext.CheckFramebufferStatus"), (int)target);
        }

        public void GenerateMipmap(WebGLTextureTarget target)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.GenerateMipmap"), (int)target);
        }

        public void AttachShader(WebGLProgram program, WebGLShader shader)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.AttachShader"), program.Uid, shader.Uid);
        }

        public string GetProgramInfoLog(WebGLProgram program)
        {
            return InvokeRetString<int>(RegisterFunction("nkCanvasGLContext.GetProgramInfoLog"), program.Uid);
        }

        public string GetShaderInfoLog(WebGLShader shader)
        {
            return InvokeRetString<int>(RegisterFunction("nkCanvasGLContext.GetShaderInfoLog"), shader.Uid);
        }

        public int GetAttribLocation(WebGLProgram program, string name)
        {
            return InvokeRetInt<int, string>(RegisterFunction("nkCanvasGLContext.GetAttribLocation"), program.Uid, name);
        }

        public WebGLUniformLocation GetUniformLocation(WebGLProgram program, string name)
        {
            int uid = InvokeRetInt<int, string>(RegisterFunction("nkCanvasGLContext.GetUniformLocation"), program.Uid, name);
            if (uid == -1)
                return null;
            return new WebGLUniformLocation(uid, this);
        }

        public void Uniform1i(WebGLUniformLocation location, int v0)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.Uniform1i"), location.Uid, v0);
        }

        public void Uniform2i(WebGLUniformLocation location, int v0, int v1)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.Uniform2i"), location.Uid, v0, v1);
        }

        public void Uniform3i(WebGLUniformLocation location, int v0, int v1, int v2)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.Uniform3i"), location.Uid, v0, v1, v2);
        }

        public void Uniform4i(WebGLUniformLocation location, int v0, int v1, int v2, int v3)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.Uniform4i"), location.Uid, v0, v1, v2, v3);
        }

        public void Uniform1f(WebGLUniformLocation location, float v0)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.Uniform1f"), location.Uid, v0);
        }
        public void Uniform2f(WebGLUniformLocation location, float v0, float v1)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.Uniform2f"), location.Uid, v0, v1);
        }
        public void Uniform3f(WebGLUniformLocation location, float v0, float v1, float v2)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.Uniform3f"), location.Uid, v0, v1, v2);
        }

        public void Uniform4f(WebGLUniformLocation location, float v0, float v1, float v2, float v3)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.Uniform4f"), location.Uid, v0, v1, v2, v3);
        }

        public unsafe void Uniform1iv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform1iv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform1iv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform1iv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform2iv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform2iv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform2iv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform2iv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform3iv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform3iv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform3iv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform3iv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform4iv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform4iv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform4iv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform4iv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public unsafe void Uniform1fv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform1fv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform1fv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform1fv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform2fv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform2fv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform2fv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform2fv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform3fv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform3fv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform3fv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform3fv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform4fv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform4fv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }
        
        public unsafe void Uniform4fv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.Uniform4fv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public unsafe void UniformMatrix2fv<TData>(WebGLUniformLocation location, TData[] value) 
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.UniformMatrix2fv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public unsafe void UniformMatrix2fv<TData>(WebGLUniformLocation location, Span<TData> value) 
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.UniformMatrix2fv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public unsafe void UniformMatrix3fv<TData>(WebGLUniformLocation location, TData[] value) 
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.UniformMatrix3fv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public unsafe void UniformMatrix3fv<TData>(WebGLUniformLocation location, Span<TData> value) 
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.UniformMatrix3fv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public unsafe void UniformMatrix4fv<TData>(WebGLUniformLocation location, TData[] value) 
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.UniformMatrix4fv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public unsafe void UniformMatrix4fv<TData>(WebGLUniformLocation location, Span<TData> value) 
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.UniformMatrix4fv"), location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public void LinkProgram(WebGLProgram program)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.LinkProgram"), program.Uid);
        }

        public void BufferData(WebGLBufferType type, int size, WebGLBufferUsageHint usage)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.BufferData"), (int)type, size, (int)usage);
        }

        public unsafe void BufferData<TData>(WebGLBufferType type, TData[] data, WebGLBufferUsageHint usage)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pData = data)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.BufferData1"), (int)type, (int)usage, stride, (int)pData, data.Length);
            }
        }

        public unsafe void BufferData<TData>(WebGLBufferType type, Span<TData> data, WebGLBufferUsageHint usage)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pData = data)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.BufferData1"), (int)type, (int)usage, stride, (int)pData, data.Length);
            }
        }

        public unsafe void BufferSubData<TData>(WebGLBufferType target, int offset, TData[] srcData, int length)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pSrcData = srcData)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.BufferSubData"), (int)target, offset, 0, length, stride, (int)pSrcData);
            }
        }

        public unsafe void BufferSubData<TData>(WebGLBufferType target, int offset, Span<TData> srcData)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pSrcData = srcData)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.BufferSubData"), (int)target, offset, 0, srcData.Length, stride, (int)pSrcData);
            }
        }

        public unsafe void BufferSubData<TData>(WebGLBufferType target, int offset, TData[] srcData, int startIndex, int length)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pSrcData = srcData)
            {
                Invoke(RegisterFunction("nkCanvasGLContext.BufferSubData"), (int)target, offset, startIndex, length, stride, (int)pSrcData);
            }
        }

        public void VertexAttribPointer(int index, int size, WebGLDataType type, bool normalized, int stride, int offset)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.VertexAttribPointer"), index, size, (int)type, normalized?1:0, stride, offset);
        }

        public void EnableVertexAttribArray(int index)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.EnableVertexAttribArray"), index);
        }

        public void DisableVertexAttribArray(int index)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.DisableVertexAttribArray"), index);
        }

        public void UseProgram(WebGLProgram program)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.UseProgram"), program.Uid);
        }

        public void ActiveTexture(WebGLTextureUnit textureUnit)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.ActiveTexture"), (int)textureUnit);
        }

        public void DrawArrays(WebGLPrimitiveType mode, int first, int count)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.DrawArrays"), (int)mode, first, count);
        }

        public void DrawElements(WebGLPrimitiveType mode, int count, WebGLDataType type, int offset)
        {
            Invoke(RegisterFunction("nkCanvasGLContext.DrawElements"), (int)mode, count, (int)type, offset);
        }

        public void Flush()
        {
            Invoke(RegisterFunction("nkCanvasGLContext.Flush"));
        }

        public void Finish()
        {
            Invoke(RegisterFunction("nkCanvasGLContext.Finish"));
        }

        public bool IsContextLost()
        {
            return InvokeRetBool(RegisterFunction("nkCanvasGLContext.IsContextLost"));
        }

        public bool GetExtension(string name)
        {
            return InvokeRetBool<string>(RegisterFunction("nkCanvasGLContext.GetExtension"), name);
        }

        public TExtension GetExtension<TExtension>(string name)
            where TExtension : WebGLExtension
        {
            int uid = InvokeRetInt<string>(RegisterFunction("nkCanvasGLContext.GetExtension1"), name);

            switch (name)
            {
                case "WEBGL_lose_context":
                    return (TExtension)(WebGLExtension)new WebGLLoseContextExtension(uid);

                case "WEBGL_polygon_mode":
                    return (TExtension)(WebGLExtension)new WebGLPolygonModeExtension(uid);

                case "OES_draw_buffers_indexed":
                    return (TExtension)(WebGLExtension)new WebGL2DrawBuffersIndexedExtension(uid);

                default:
                    return (TExtension)new WebGLExtension(uid);
            }
        }

        public WebGLErrorCode GetError()
        {   
            return (WebGLErrorCode)InvokeRetInt(RegisterFunction("nkCanvasGLContext.GetError"));
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
