using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas.WebGL
{
    internal class WebGLRenderingContext : RenderingContext, IWebGLRenderingContext, IDisposable
    {
        WebGLPolygonModeExtension _polygonModeExtension;

        private readonly int _fid_Enable;
        private readonly int _fid_Disable;
        private readonly int _fid_BlendEquationSeparate;
        private readonly int _fid_BlendFuncSeparate;
        private readonly int _fid_BlendColor;
        private readonly int _fid_ColorMask;
        private readonly int _fid_CullFace;
        private readonly int _fid_FrontFace;
        private readonly int _fid_PolygonOffset;
        private readonly int _fid_DepthMask;
        private readonly int _fid_StencilMask;
        private readonly int _fid_StencilMaskSeparate;
        private readonly int _fid_DepthFunc;
        private readonly int _fid_StencilFunc;
        private readonly int _fid_StencilFuncSeparate;
        private readonly int _fid_StencilOp;
        private readonly int _fid_StencilOpSeparate;
        private readonly int _fid_Viewport;
        private readonly int _fid_DepthRange;
        private readonly int _fid_Scissor;
        private readonly int _fid_ClearColor;
        private readonly int _fid_ClearDepth;
        private readonly int _fid_ClearStencil;
        private readonly int _fid_Clear;
        private readonly int _fid_GetParameterInt;
        private readonly int _fid_GetParameterString;
        private readonly int _fid_CreateTexture;
        private readonly int _fid_DeleteTexture;
        private readonly int _fid_CreateShader;
        private readonly int _fid_DeleteShader;
        private readonly int _fid_CreateProgram;
        private readonly int _fid_DeleteProgram;
        private readonly int _fid_CreateBuffer;
        private readonly int _fid_DeleteBuffer;
        private readonly int _fid_CreateFramebuffer;
        private readonly int _fid_DeleteFramebuffer;
        private readonly int _fid_CreateRenderbuffer;
        private readonly int _fid_DeleteRenderbuffer;
        private readonly int _fid_ShaderSource;
        private readonly int _fid_CompileShader;
        private readonly int _fid_GetShaderParameter;
        private readonly int _fid_GetProgramParameter;
        private readonly int _fid_TexImage2D;
        private readonly int _fid_TexImage2D1;
        private readonly int _fid_TexImage2D3;
        private readonly int _fid_TexImage2D2;
        private readonly int _fid_TexSubImage2D1;
        private readonly int _fid_TexSubImage2D2;
        private readonly int _fid_CompressedTexImage2D;
        private readonly int _fid_CompressedTexSubImage2D;
        private readonly int _fid_ReadPixels;
        private readonly int _fid_TexParameteri;
        private readonly int _fid_TexParameterf;
        private readonly int _fid_PixelStorei;
        private readonly int _fid_BindTexture;
        private readonly int _fid_BindBuffer;
        private readonly int _fid_BindFramebuffer;
        private readonly int _fid_BindRenderbuffer;
        private readonly int _fid_FramebufferRenderbuffer;
        private readonly int _fid_FramebufferTexture2D;
        private readonly int _fid_FramebufferTexture2D1;
        private readonly int _fid_RenderbufferStorage;
        private readonly int _fid_CheckFramebufferStatus;
        private readonly int _fid_GenerateMipmap;
        private readonly int _fid_AttachShader;
        private readonly int _fid_GetProgramInfoLog;
        private readonly int _fid_GetShaderInfoLog;
        private readonly int _fid_GetAttribLocation;
        private readonly int _fid_GetUniformLocation;
        private readonly int _fid_Uniform1i;
        private readonly int _fid_Uniform2i;
        private readonly int _fid_Uniform3i;
        private readonly int _fid_Uniform4i;
        private readonly int _fid_Uniform1f;
        private readonly int _fid_Uniform2f;
        private readonly int _fid_Uniform3f;
        private readonly int _fid_Uniform4f;
        private readonly int _fid_Uniform1iv;
        private readonly int _fid_Uniform2iv;
        private readonly int _fid_Uniform3iv;
        private readonly int _fid_Uniform4iv;
        private readonly int _fid_Uniform1fv;
        private readonly int _fid_Uniform2fv;
        private readonly int _fid_Uniform3fv;
        private readonly int _fid_Uniform4fv;
        private readonly int _fid_UniformMatrix2fv;
        private readonly int _fid_UniformMatrix3fv;
        private readonly int _fid_UniformMatrix4fv;
        private readonly int _fid_LinkProgram;
        private readonly int _fid_BufferData;
        private readonly int _fid_BufferData1;
        private readonly int _fid_BufferSubData;
        private readonly int _fid_VertexAttribPointer;
        private readonly int _fid_EnableVertexAttribArray;
        private readonly int _fid_DisableVertexAttribArray;
        private readonly int _fid_UseProgram;
        private readonly int _fid_ActiveTexture;
        private readonly int _fid_DrawArrays;
        private readonly int _fid_DrawElements;
        private readonly int _fid_Flush;
        private readonly int _fid_Finish;
        private readonly int _fid_IsContextLost;
        private readonly int _fid_GetExtension;
        private readonly int _fid_GetExtension1;
        private readonly int _fid_GetError;

        internal WebGLRenderingContext(Canvas canvas, int uid) : base(canvas, uid)
        {
            _fid_Enable = RegisterFunction("nkCanvasGLContext.Enable");
            _fid_Disable = RegisterFunction("nkCanvasGLContext.Disable");
            _fid_BlendEquationSeparate = RegisterFunction("nkCanvasGLContext.BlendEquationSeparate");
            _fid_BlendFuncSeparate = RegisterFunction("nkCanvasGLContext.BlendFuncSeparate");
            _fid_BlendColor = RegisterFunction("nkCanvasGLContext.BlendColor");
            _fid_ColorMask = RegisterFunction("nkCanvasGLContext.ColorMask");
            _fid_CullFace = RegisterFunction("nkCanvasGLContext.CullFace");
            _fid_FrontFace = RegisterFunction("nkCanvasGLContext.FrontFace");
            _fid_PolygonOffset = RegisterFunction("nkCanvasGLContext.PolygonOffset");
            _fid_DepthMask = RegisterFunction("nkCanvasGLContext.DepthMask");
            _fid_StencilMask = RegisterFunction("nkCanvasGLContext.StencilMask");
            _fid_StencilMaskSeparate = RegisterFunction("nkCanvasGLContext.StencilMaskSeparate");
            _fid_DepthFunc = RegisterFunction("nkCanvasGLContext.DepthFunc");
            _fid_StencilFunc = RegisterFunction("nkCanvasGLContext.StencilFunc");
            _fid_StencilFuncSeparate = RegisterFunction("nkCanvasGLContext.StencilFuncSeparate");
            _fid_StencilOp = RegisterFunction("nkCanvasGLContext.StencilOp");
            _fid_StencilOpSeparate = RegisterFunction("nkCanvasGLContext.StencilOpSeparate");
            _fid_Viewport = RegisterFunction("nkCanvasGLContext.Viewport");
            _fid_DepthRange = RegisterFunction("nkCanvasGLContext.DepthRange");
            _fid_Scissor = RegisterFunction("nkCanvasGLContext.Scissor");
            _fid_ClearColor = RegisterFunction("nkCanvasGLContext.ClearColor");
            _fid_ClearDepth = RegisterFunction("nkCanvasGLContext.ClearDepth");
            _fid_ClearStencil = RegisterFunction("nkCanvasGLContext.ClearStencil");
            _fid_Clear = RegisterFunction("nkCanvasGLContext.Clear");
            _fid_GetParameterInt = RegisterFunction("nkCanvasGLContext.GetParameterInt");
            _fid_GetParameterString = RegisterFunction("nkCanvasGLContext.GetParameterString");
            _fid_CreateTexture = RegisterFunction("nkCanvasGLContext.CreateTexture");
            _fid_DeleteTexture = RegisterFunction("nkCanvasGLContext.DeleteTexture");
            _fid_CreateShader = RegisterFunction("nkCanvasGLContext.CreateShader");
            _fid_DeleteShader = RegisterFunction("nkCanvasGLContext.DeleteShader");
            _fid_CreateProgram = RegisterFunction("nkCanvasGLContext.CreateProgram");
            _fid_DeleteProgram = RegisterFunction("nkCanvasGLContext.DeleteProgram");
            _fid_CreateBuffer = RegisterFunction("nkCanvasGLContext.CreateBuffer");
            _fid_DeleteBuffer = RegisterFunction("nkCanvasGLContext.DeleteBuffer");
            _fid_CreateFramebuffer = RegisterFunction("nkCanvasGLContext.CreateFramebuffer");
            _fid_DeleteFramebuffer = RegisterFunction("nkCanvasGLContext.DeleteFramebuffer");
            _fid_CreateRenderbuffer = RegisterFunction("nkCanvasGLContext.CreateRenderbuffer");
            _fid_DeleteRenderbuffer = RegisterFunction("nkCanvasGLContext.DeleteRenderbuffer");
            _fid_ShaderSource = RegisterFunction("nkCanvasGLContext.ShaderSource");
            _fid_CompileShader = RegisterFunction("nkCanvasGLContext.CompileShader");
            _fid_GetShaderParameter = RegisterFunction("nkCanvasGLContext.GetShaderParameter");
            _fid_GetProgramParameter = RegisterFunction("nkCanvasGLContext.GetProgramParameter");
            _fid_TexImage2D = RegisterFunction("nkCanvasGLContext.TexImage2D");
            _fid_TexImage2D1 = RegisterFunction("nkCanvasGLContext.TexImage2D1");
            _fid_TexImage2D3 = RegisterFunction("nkCanvasGLContext.TexImage2D3");
            _fid_TexImage2D2 = RegisterFunction("nkCanvasGLContext.TexImage2D2");
            _fid_TexSubImage2D1 = RegisterFunction("nkCanvasGLContext.TexSubImage2D1");
            _fid_TexSubImage2D2 = RegisterFunction("nkCanvasGLContext.TexSubImage2D2");
            _fid_CompressedTexImage2D = RegisterFunction("nkCanvasGLContext.CompressedTexImage2D");
            _fid_CompressedTexSubImage2D = RegisterFunction("nkCanvasGLContext.CompressedTexSubImage2D");
            _fid_ReadPixels = RegisterFunction("nkCanvasGLContext.ReadPixels");
            _fid_TexParameteri = RegisterFunction("nkCanvasGLContext.TexParameteri");
            _fid_TexParameterf = RegisterFunction("nkCanvasGLContext.TexParameterf");
            _fid_PixelStorei = RegisterFunction("nkCanvasGLContext.PixelStorei");
            _fid_BindTexture = RegisterFunction("nkCanvasGLContext.BindTexture");
            _fid_BindBuffer = RegisterFunction("nkCanvasGLContext.BindBuffer");
            _fid_BindFramebuffer = RegisterFunction("nkCanvasGLContext.BindFramebuffer");
            _fid_BindRenderbuffer = RegisterFunction("nkCanvasGLContext.BindRenderbuffer");
            _fid_FramebufferRenderbuffer = RegisterFunction("nkCanvasGLContext.FramebufferRenderbuffer");
            _fid_FramebufferTexture2D = RegisterFunction("nkCanvasGLContext.FramebufferTexture2D");
            _fid_FramebufferTexture2D1 = RegisterFunction("nkCanvasGLContext.FramebufferTexture2D1");
            _fid_RenderbufferStorage = RegisterFunction("nkCanvasGLContext.RenderbufferStorage");
            _fid_CheckFramebufferStatus = RegisterFunction("nkCanvasGLContext.CheckFramebufferStatus");
            _fid_GenerateMipmap = RegisterFunction("nkCanvasGLContext.GenerateMipmap");
            _fid_AttachShader = RegisterFunction("nkCanvasGLContext.AttachShader");
            _fid_GetProgramInfoLog = RegisterFunction("nkCanvasGLContext.GetProgramInfoLog");
            _fid_GetShaderInfoLog = RegisterFunction("nkCanvasGLContext.GetShaderInfoLog");
            _fid_GetAttribLocation = RegisterFunction("nkCanvasGLContext.GetAttribLocation");
            _fid_GetUniformLocation = RegisterFunction("nkCanvasGLContext.GetUniformLocation");
            _fid_Uniform1i = RegisterFunction("nkCanvasGLContext.Uniform1i");
            _fid_Uniform2i = RegisterFunction("nkCanvasGLContext.Uniform2i");
            _fid_Uniform3i = RegisterFunction("nkCanvasGLContext.Uniform3i");
            _fid_Uniform4i = RegisterFunction("nkCanvasGLContext.Uniform4i");
            _fid_Uniform1f = RegisterFunction("nkCanvasGLContext.Uniform1f");
            _fid_Uniform2f = RegisterFunction("nkCanvasGLContext.Uniform2f");
            _fid_Uniform3f = RegisterFunction("nkCanvasGLContext.Uniform3f");
            _fid_Uniform4f = RegisterFunction("nkCanvasGLContext.Uniform4f");
            _fid_Uniform1iv = RegisterFunction("nkCanvasGLContext.Uniform1iv");
            _fid_Uniform2iv = RegisterFunction("nkCanvasGLContext.Uniform2iv");
            _fid_Uniform3iv = RegisterFunction("nkCanvasGLContext.Uniform3iv");
            _fid_Uniform4iv = RegisterFunction("nkCanvasGLContext.Uniform4iv");
            _fid_Uniform1fv = RegisterFunction("nkCanvasGLContext.Uniform1fv");
            _fid_Uniform2fv = RegisterFunction("nkCanvasGLContext.Uniform2fv");
            _fid_Uniform3fv = RegisterFunction("nkCanvasGLContext.Uniform3fv");
            _fid_Uniform4fv = RegisterFunction("nkCanvasGLContext.Uniform4fv");
            _fid_UniformMatrix2fv = RegisterFunction("nkCanvasGLContext.UniformMatrix2fv");
            _fid_UniformMatrix3fv = RegisterFunction("nkCanvasGLContext.UniformMatrix3fv");
            _fid_UniformMatrix4fv = RegisterFunction("nkCanvasGLContext.UniformMatrix4fv");
            _fid_LinkProgram = RegisterFunction("nkCanvasGLContext.LinkProgram");
            _fid_BufferData = RegisterFunction("nkCanvasGLContext.BufferData");
            _fid_BufferData1 = RegisterFunction("nkCanvasGLContext.BufferData1");
            _fid_BufferSubData = RegisterFunction("nkCanvasGLContext.BufferSubData");
            _fid_VertexAttribPointer = RegisterFunction("nkCanvasGLContext.VertexAttribPointer");
            _fid_EnableVertexAttribArray = RegisterFunction("nkCanvasGLContext.EnableVertexAttribArray");
            _fid_DisableVertexAttribArray = RegisterFunction("nkCanvasGLContext.DisableVertexAttribArray");
            _fid_UseProgram = RegisterFunction("nkCanvasGLContext.UseProgram");
            _fid_ActiveTexture = RegisterFunction("nkCanvasGLContext.ActiveTexture");
            _fid_DrawArrays = RegisterFunction("nkCanvasGLContext.DrawArrays");
            _fid_DrawElements = RegisterFunction("nkCanvasGLContext.DrawElements");
            _fid_Flush = RegisterFunction("nkCanvasGLContext.Flush");
            _fid_Finish = RegisterFunction("nkCanvasGLContext.Finish");
            _fid_IsContextLost = RegisterFunction("nkCanvasGLContext.IsContextLost");
            _fid_GetExtension = RegisterFunction("nkCanvasGLContext.GetExtension");
            _fid_GetExtension1 = RegisterFunction("nkCanvasGLContext.GetExtension1");
            _fid_GetError = RegisterFunction("nkCanvasGLContext.GetError");
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
            Invoke(_fid_Enable, (int)cap);
        }

        public void Disable(WebGLCapability cap)
        {
            Invoke(_fid_Disable, (int)cap);
        }

        public void BlendEquationSeparate(WebGLEquationFunc modeRGB, WebGLEquationFunc modeAlpha)
        {
            Invoke(_fid_BlendEquationSeparate, modeRGB, modeAlpha);
        }

        public void BlendFuncSeparate(WebGLBlendFunc srcRGB, WebGLBlendFunc dstRGB, WebGLBlendFunc srcAlpha, WebGLBlendFunc dstAlpha)
        {
            Invoke(_fid_BlendFuncSeparate, srcRGB, dstRGB, srcAlpha, dstAlpha);
        }

        public void BlendColor(float red, float green, float blue, float alpha)
        {
            Invoke(_fid_BlendColor, red, green, blue, alpha);
        }

        public void ColorMask(bool red, bool green, bool blue, bool alpha)
        {
            Invoke(_fid_ColorMask, red?1:0, green?1:0, blue?1:0, alpha?1:0);
        }

        public void CullFace(WebGLCullFaceMode mode)
        {
            Invoke(_fid_CullFace, (int)mode);
        }

        public void FrontFace(WebGLWinding mode)
        {
            Invoke(_fid_FrontFace, (int)mode);
        }

        public void PolygonOffset(float factor, float units)
        {
            Invoke(_fid_PolygonOffset, factor, units);
        }

        public void DepthMask(bool enable)
        {
            Invoke(_fid_DepthMask, enable ?1:0);
        }

        public void StencilMask(int mask)
        {
            Invoke(_fid_StencilMask, mask);
        }

        public void StencilMaskSeparate(WebGLCullFaceMode mode, int mask)
        {
            Invoke(_fid_StencilMaskSeparate, (int)mode, mask);
        }

        public void DepthFunc(WebGLDepthComparisonFunc func)
        {
            Invoke(_fid_DepthFunc, (int)func);
        }

        public void StencilFunc(WebGLDepthComparisonFunc func, int StencilRef, int stencilMask)
        {
            Invoke(_fid_StencilFunc, (int)func, StencilRef, stencilMask);
        }

        public void StencilFuncSeparate(WebGLCullFaceMode mode, WebGLDepthComparisonFunc func, int stencilRef, int stencilMask)
        {
            Invoke(_fid_StencilFuncSeparate, (int)mode, (int)func, stencilRef, stencilMask);
        }

        public void StencilOp(WebGLStencilOpFunc fail, WebGLStencilOpFunc zfail, WebGLStencilOpFunc zpass)
        {
            Invoke(_fid_StencilOp, (int)fail, (int)zfail, (int)zpass);
        }

        public void StencilOpSeparate(WebGLCullFaceMode mode, WebGLStencilOpFunc fail, WebGLStencilOpFunc zfail, WebGLStencilOpFunc zpass)
        {
            Invoke(_fid_StencilOpSeparate, (int)mode, (int)fail, (int)zfail, (int)zpass);
        }

        public void Viewport(int x, int y, int width, int height)
        {
            Invoke(_fid_Viewport, x, y, width, height);
        }

        public void DepthRange(float zNear, float zFar)
        {
            Invoke(_fid_DepthRange, zNear, zFar);
        }

        public void Scissor(int x, int y, int width, int height)
        {
            Invoke(_fid_Scissor, x, y, width, height);
        }


        public void ClearColor(float r, float g, float b, float a)
        {
            Invoke(_fid_ClearColor, r, g, b, a);
        }

        public void ClearDepth(float depth)
        {
            Invoke(_fid_ClearDepth, depth);
        }

        public void ClearStencil(int stencil)
        {
            Invoke(_fid_ClearStencil, stencil);
        }

        public void Clear(WebGLBufferBits bufferBits)
        {
            Invoke(_fid_Clear, (int)bufferBits);
        }

        public int GetParameter(WebGLPNameInteger pname)
        {
            return InvokeRetInt<int>(_fid_GetParameterInt, (int)pname);
        }

        public string GetParameter(WebGLPNameString pname)
        {
            return InvokeRetString<int>(_fid_GetParameterString, (int)pname);
        }

        public WebGLTexture CreateTexture()
        {
            int uid = InvokeRetInt(_fid_CreateTexture);
            return new WebGLTexture(uid, this);
        }

        internal void DeleteTexture(WebGLTexture texture)
        {
            Invoke(_fid_DeleteTexture, texture.Uid);
        }

        public WebGLShader CreateShader(WebGLShaderType type)
        {
            int uid = InvokeRetInt<int>(_fid_CreateShader, (int)type);
            return new WebGLShader(uid, this);
        }

        internal void DeleteShader(WebGLShader shader)
        {
            Invoke(_fid_DeleteShader, shader.Uid);
        }

        public WebGLProgram CreateProgram()
        {
            int uid = InvokeRetInt(_fid_CreateProgram);
            return new WebGLProgram(uid, this);
        }

        internal void DeleteProgram(WebGLProgram program)
        {
            Invoke(_fid_DeleteProgram, program.Uid);
        }

        public WebGLBuffer CreateBuffer()
        {
            int uid = InvokeRetInt(_fid_CreateBuffer);
            return new WebGLBuffer(uid, this);
        }

        internal void DeleteBuffer(WebGLBuffer buffer)
        {
            Invoke(_fid_DeleteBuffer, buffer.Uid);
        }

        public WebGLFramebuffer CreateFramebuffer()
        {
            int uid = InvokeRetInt(_fid_CreateFramebuffer);
            return new WebGLFramebuffer(uid, this);
        }

        internal void DeleteFramebuffer(WebGLFramebuffer framebuffer)
        {
            Invoke(_fid_DeleteFramebuffer, framebuffer.Uid);
        }

        public WebGLRenderbuffer CreateRenderbuffer()
        {
            int uid = InvokeRetInt(_fid_CreateRenderbuffer);
            return new WebGLRenderbuffer(uid, this);
        }

        internal void DeleteRenderbuffer(WebGLRenderbuffer renderbuffer)
        {
            Invoke(_fid_DeleteRenderbuffer, renderbuffer.Uid);
        }

        public void ShaderSource(WebGLShader shader, string source)
        {
            Invoke(_fid_ShaderSource, shader.Uid, source);
        }

        public void CompileShader(WebGLShader shader)
        {
            Invoke(_fid_CompileShader, shader.Uid);
        }

        public bool GetShaderParameter(WebGLShader shader, WebGLShaderStatus pname)
        {
            return InvokeRetBool<int, int>(_fid_GetShaderParameter, shader.Uid, (int)pname);
        }

        public bool GetProgramParameter(WebGLProgram program, WebGLProgramStatus pname)
        {
            return InvokeRetBool<int, int>(_fid_GetProgramParameter, program.Uid, (int)pname);
        }

        public void TexImage2D(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, WebGLFormat format, WebGLTexelType type)
        {
            Invoke(_fid_TexImage2D, (int)target, level, (int)internalFormat, width, height, (int)format, (int)type);
        }

        public unsafe void TexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_TexImage2D1, (int)target, level, (int)internalFormat, width, height, (int)format, (int)type, stride, (int)pPixels, pixels.Length);
            }
        }

        public unsafe void TexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, WebGLFormat format, WebGLTexelType type, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_TexImage2D1, (int)target, level, (int)internalFormat, width, height, (int)format, (int)type, stride, (int)pPixels, pixels.Length);
            }
        }

        public unsafe void TexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_TexImage2D3, (int)target, level, (int)internalFormat, width, height, (int)format, (int)type, stride, (int)pPixels, index, count);
            }
        }

        public void TexImage2D(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, WebGLFormat format, WebGLTexelType type, Video video)
        {
            Invoke(_fid_TexImage2D2, (int)target, level, (int)internalFormat,  (int)format, (int)type, video.Uid);
        }

        public unsafe void TexSubImage2D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels) 
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int>(xoffset, yoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_TexSubImage2D1, (int)target, level, position, width, height, (int)format, (int)type, stride, (int)pPixels, pixels.Length);
            }
        }

        public unsafe void TexSubImage2D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int width, int height, WebGLFormat format, WebGLTexelType type, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int>(xoffset, yoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_TexSubImage2D1, (int)target, level, position, width, height, (int)format, (int)type, stride, (int)pPixels, pixels.Length);
            }
        }

        public unsafe void TexSubImage2D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int>(xoffset, yoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_TexSubImage2D2, (int)target, level, position, width, height, (int)format, (int)type, stride, (int)pPixels, index, count);
            }
        }

        public unsafe void CompressedTexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_CompressedTexImage2D, (int)target, level, (int)internalFormat, width, height, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_CompressedTexImage2D, (int)target, level, (int)internalFormat, width, height, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_CompressedTexImage2D, (int)target, level, (int)internalFormat, width, height, stride, (int)pPixels, index, count);
            }
        }

        public unsafe void CompressedTexSubImage2D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int width, int height, WebGLFormat format, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int>(xoffset, yoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_CompressedTexSubImage2D, (int)target, level, position, width, height, (int)format, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexSubImage2D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int width, int height, WebGLFormat format, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int>(xoffset, yoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_CompressedTexSubImage2D, (int)target, level, position, width, height, (int)format, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexSubImage2D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int width, int height, WebGLFormat format, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int>(xoffset, yoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_CompressedTexSubImage2D, (int)target, level, position, width, height, (int)format, stride, (int)pPixels, index, count);
            }
        }

        public unsafe void ReadPixels<TData>(int x, int y, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_ReadPixels, x, y, width, height, format, type, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void ReadPixels<TData>(int x, int y, int width, int height, WebGLFormat format, WebGLTexelType type, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_ReadPixels, x, y, width, height, format, type, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void ReadPixels<TData>(int x, int y, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_ReadPixels, x, y, width, height, format, type, stride, (int)pPixels, index, count);
            }
        }

        public void TexParameter(WebGLTextureTarget target, WebGLTexParamName pname, WebGLTexParam param)
        {
            Invoke(_fid_TexParameteri, (int)target, (int)pname, (int)param);
        }

        public void TexParameter(WebGLTextureTarget target, WebGLTexParamName pname, float param)
        {
            Invoke(_fid_TexParameterf, (int)target, (int)pname, param);
        }

        public void PixelStore(WebGLPixelParameter pname, int param)
        {
            Invoke(_fid_PixelStorei, (int)pname, param);
        }

        public void BindTexture(WebGLTextureTarget target, WebGLTexture texture)
        {
            int uid = (texture != null) ? texture.Uid : -1;
            Invoke(_fid_BindTexture, (int)target, uid);
        }

        public void BindBuffer(WebGLBufferType type, WebGLBuffer buffer)
        {
            Invoke(_fid_BindBuffer, (int)type, buffer.Uid);
        }

        public void BindFramebuffer(WebGLFramebufferType type, WebGLFramebuffer framebuffer)
        {
            int uid = (framebuffer != null) ? framebuffer.Uid : -1;
            Invoke(_fid_BindFramebuffer, (int)type, uid);
        }

        public void BindRenderbuffer(WebGLRenderbufferType type, WebGLRenderbuffer renderbuffer)
        {
            Invoke(_fid_BindRenderbuffer, (int)type, renderbuffer.Uid);
        }

        public void FramebufferRenderbuffer(WebGLFramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLRenderbufferType renderbuffertarget, WebGLRenderbuffer renderbuffer)
        {
            int uid = (renderbuffer != null) ? renderbuffer.Uid : -1;
            Invoke(_fid_FramebufferRenderbuffer, (int)target, (int)attachment, (int)renderbuffertarget, uid);
        }

        public void FramebufferTexture2D(WebGLFramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLTextureTarget texturetarget, WebGLTexture texture)
        {
            Invoke(_fid_FramebufferTexture2D, (int)target, (int)attachment, (int)texturetarget, texture.Uid);
        }

        public void FramebufferTexture2D(WebGLFramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLTextureTarget texturetarget, WebGLTexture texture, int level)
        {
            Invoke(_fid_FramebufferTexture2D1, (int)target, (int)attachment, (int)texturetarget, texture.Uid, level);
        }

        public void RenderbufferStorage(WebGLRenderbufferType target, WebGLRenderbufferInternalFormat internalFormat, int width, int height)
        {
            Invoke(_fid_RenderbufferStorage, (int)target, (int)internalFormat, width, height);
        }

        public WebGLFramebufferStatus CheckFramebufferStatus(WebGLFramebufferType target)
        {
            return (WebGLFramebufferStatus)InvokeRetInt<int>(_fid_CheckFramebufferStatus, (int)target);
        }

        public void GenerateMipmap(WebGLTextureTarget target)
        {
            Invoke(_fid_GenerateMipmap, (int)target);
        }

        public void AttachShader(WebGLProgram program, WebGLShader shader)
        {
            Invoke(_fid_AttachShader, program.Uid, shader.Uid);
        }

        public string GetProgramInfoLog(WebGLProgram program)
        {
            return InvokeRetString<int>(_fid_GetProgramInfoLog, program.Uid);
        }

        public string GetShaderInfoLog(WebGLShader shader)
        {
            return InvokeRetString<int>(_fid_GetShaderInfoLog, shader.Uid);
        }

        public int GetAttribLocation(WebGLProgram program, string name)
        {
            return InvokeRetInt<int, string>(_fid_GetAttribLocation, program.Uid, name);
        }

        public WebGLUniformLocation GetUniformLocation(WebGLProgram program, string name)
        {
            int uid = InvokeRetInt<int, string>(_fid_GetUniformLocation, program.Uid, name);
            if (uid == -1)
                return null;
            return new WebGLUniformLocation(uid, this);
        }

        public void Uniform1i(WebGLUniformLocation location, int v0)
        {
            Invoke(_fid_Uniform1i, location.Uid, v0);
        }

        public void Uniform2i(WebGLUniformLocation location, int v0, int v1)
        {
            Invoke(_fid_Uniform2i, location.Uid, v0, v1);
        }

        public void Uniform3i(WebGLUniformLocation location, int v0, int v1, int v2)
        {
            Invoke(_fid_Uniform3i, location.Uid, v0, v1, v2);
        }

        public void Uniform4i(WebGLUniformLocation location, int v0, int v1, int v2, int v3)
        {
            Invoke(_fid_Uniform4i, location.Uid, v0, v1, v2, v3);
        }

        public void Uniform1f(WebGLUniformLocation location, float v0)
        {
            Invoke(_fid_Uniform1f, location.Uid, v0);
        }
        public void Uniform2f(WebGLUniformLocation location, float v0, float v1)
        {
            Invoke(_fid_Uniform2f, location.Uid, v0, v1);
        }
        public void Uniform3f(WebGLUniformLocation location, float v0, float v1, float v2)
        {
            Invoke(_fid_Uniform3f, location.Uid, v0, v1, v2);
        }

        public void Uniform4f(WebGLUniformLocation location, float v0, float v1, float v2, float v3)
        {
            Invoke(_fid_Uniform4f, location.Uid, v0, v1, v2, v3);
        }

        public unsafe void Uniform1iv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform1iv, location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform1iv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform1iv, location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform2iv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform2iv, location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform2iv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform2iv, location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform3iv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform3iv, location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform3iv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform3iv, location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform4iv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform4iv, location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform4iv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform4iv, location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public unsafe void Uniform1fv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform1fv, location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform1fv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform1fv, location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform2fv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform2fv, location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform2fv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform2fv, location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform3fv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform3fv, location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform3fv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform3fv, location.Uid, stride, (int)pValue, value.Length);
            }
        }
        public unsafe void Uniform4fv<TData>(WebGLUniformLocation location, TData[] value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform4fv, location.Uid, stride, (int)pValue, value.Length);
            }
        }
        
        public unsafe void Uniform4fv<TData>(WebGLUniformLocation location, Span<TData> value)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_Uniform4fv, location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public unsafe void UniformMatrix2fv<TData>(WebGLUniformLocation location, TData[] value) 
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_UniformMatrix2fv, location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public unsafe void UniformMatrix2fv<TData>(WebGLUniformLocation location, Span<TData> value) 
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_UniformMatrix2fv, location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public unsafe void UniformMatrix3fv<TData>(WebGLUniformLocation location, TData[] value) 
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_UniformMatrix3fv, location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public unsafe void UniformMatrix3fv<TData>(WebGLUniformLocation location, Span<TData> value) 
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_UniformMatrix3fv, location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public unsafe void UniformMatrix4fv<TData>(WebGLUniformLocation location, TData[] value) 
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_UniformMatrix4fv, location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public unsafe void UniformMatrix4fv<TData>(WebGLUniformLocation location, Span<TData> value) 
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pValue = value)
            {
                Invoke(_fid_UniformMatrix4fv, location.Uid, stride, (int)pValue, value.Length);
            }
        }

        public void LinkProgram(WebGLProgram program)
        {
            Invoke(_fid_LinkProgram, program.Uid);
        }

        public void BufferData(WebGLBufferType type, int size, WebGLBufferUsageHint usage)
        {
            Invoke(_fid_BufferData, (int)type, size, (int)usage);
        }

        public unsafe void BufferData<TData>(WebGLBufferType type, TData[] data, WebGLBufferUsageHint usage)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pData = data)
            {
                Invoke(_fid_BufferData1, (int)type, (int)usage, stride, (int)pData, data.Length);
            }
        }

        public unsafe void BufferData<TData>(WebGLBufferType type, Span<TData> data, WebGLBufferUsageHint usage)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pData = data)
            {
                Invoke(_fid_BufferData1, (int)type, (int)usage, stride, (int)pData, data.Length);
            }
        }

        public unsafe void BufferSubData<TData>(WebGLBufferType target, int offset, TData[] srcData, int length)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pSrcData = srcData)
            {
                Invoke(_fid_BufferSubData, (int)target, offset, 0, length, stride, (int)pSrcData);
            }
        }

        public unsafe void BufferSubData<TData>(WebGLBufferType target, int offset, Span<TData> srcData)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pSrcData = srcData)
            {
                Invoke(_fid_BufferSubData, (int)target, offset, 0, srcData.Length, stride, (int)pSrcData);
            }
        }

        public unsafe void BufferSubData<TData>(WebGLBufferType target, int offset, TData[] srcData, int startIndex, int length)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pSrcData = srcData)
            {
                Invoke(_fid_BufferSubData, (int)target, offset, startIndex, length, stride, (int)pSrcData);
            }
        }

        public void VertexAttribPointer(int index, int size, WebGLDataType type, bool normalized, int stride, int offset)
        {
            Invoke(_fid_VertexAttribPointer, index, size, (int)type, normalized?1:0, stride, offset);
        }

        public void EnableVertexAttribArray(int index)
        {
            Invoke(_fid_EnableVertexAttribArray, index);
        }

        public void DisableVertexAttribArray(int index)
        {
            Invoke(_fid_DisableVertexAttribArray, index);
        }

        public void UseProgram(WebGLProgram program)
        {
            Invoke(_fid_UseProgram, program.Uid);
        }

        public void ActiveTexture(WebGLTextureUnit textureUnit)
        {
            Invoke(_fid_ActiveTexture, (int)textureUnit);
        }

        public void DrawArrays(WebGLPrimitiveType mode, int first, int count)
        {
            Invoke(_fid_DrawArrays, (int)mode, first, count);
        }

        public void DrawElements(WebGLPrimitiveType mode, int count, WebGLDataType type, int offset)
        {
            Invoke(_fid_DrawElements, (int)mode, count, (int)type, offset);
        }

        public void Flush()
        {
            Invoke(_fid_Flush);
        }

        public void Finish()
        {
            Invoke(_fid_Finish);
        }

        public bool IsContextLost()
        {
            return InvokeRetBool(_fid_IsContextLost);
        }

        public bool GetExtension(string name)
        {
            return InvokeRetBool<string>(_fid_GetExtension, name);
        }

        public TExtension GetExtension<TExtension>(string name)
            where TExtension : WebGLExtension
        {
            int uid = InvokeRetInt<string>(_fid_GetExtension1, name);

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
            return (WebGLErrorCode)InvokeRetInt(_fid_GetError);
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
