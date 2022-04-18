using System;
using System.Collections.Generic;

namespace nkast.Wasm.Canvas.WebGL
{
    public interface IWebGLRenderingContext : IRenderingContext
    {
        void Enable(WebGLCapability cap);
        void Disable(WebGLCapability cap);

        void BlendEquationSeparate(WebGLEquationFunc modeRGB, WebGLEquationFunc modeAlpha);
        void BlendFuncSeparate(WebGLBlendFunc srcRGB, WebGLBlendFunc dstRGB, WebGLBlendFunc srcAlpha, WebGLBlendFunc dstAlpha);
        void BlendColor(float red, float green, float blue, float alpha);
        void ColorMask(bool red, bool green, bool blue, bool alpha);
        void FrontFace(WebGLWinding mode);
        void CullFace(WebGLCullFaceMode mode);
        void PolygonOffset(float factor, float units);
        void DepthMask(bool enable);
        void StencilMask(int stencilWriteMask);
        void StencilFunc(WebGLDepthComparisonFunc func, int StencilRef, int stencilMask);
        void StencilOp(WebGLStencilOpFunc fail, WebGLStencilOpFunc zfail, WebGLStencilOpFunc zpass);

        void Viewport(int x, int y, int width, int height);
        void DepthRange(float zNear, float zFar);
        void Scissor(int x, int y, int width, int height);

        void ClearColor(float r, float g, float b, float a);
        void ClearDepth(float depth);
        void ClearStencil(int stencil);
        void Clear(WebGLBufferBits bufferBits);
        void ActiveTexture(WebGLTextureUnit textureUnit);
        void DepthFunc(WebGLDepthComparisonFunc func);
        WebGLTexture CreateTexture();
        WebGLShader CreateShader(WebGLShaderType type);
        WebGLProgram CreateProgram();
        WebGLBuffer CreateBuffer();
        WebGLFramebuffer CreateFramebuffer();
        WebGLRenderbuffer CreateRenderbuffer();
        void ShaderSource(WebGLShader shader, string source);
        void CompileShader(WebGLShader shader);
        bool GetShaderParameter(WebGLShader shader, WebGLShaderStatus pname);
        bool GetProgramParameter(WebGLProgram program, WebGLProgramStatus pname);
        void TexImage2D(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, WebGLFormat format, WebGLTexelType type);
        void TexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels) where TData : struct;
        void TexSubImage2D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int width, int height, WebGLFormat format, WebGLTexelType type, TData[] pixels) where TData : struct;
        void CompressedTexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, TData[] pixels) where TData : struct;
        void CompressedTexImage2D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, TData[] pixels, int index, int count) where TData : struct;
        void TexParameter(WebGLTextureTarget target, WebGLTexParamName pname, WebGLTexParam param);
        void PixelStore(WebGLPixelParameter pname, int param);
        void BindTexture(WebGLTextureTarget target, WebGLTexture texture);
        void BindBuffer(WebGLBufferType type, WebGLBuffer buffer);
        void BindFramebuffer(WebGLFramebufferType target, WebGLFramebuffer framebuffer);
        void BindRenderbuffer(WebGLRenderbufferType target, WebGLRenderbuffer renderbuffer);
        void FramebufferRenderbuffer(WebGLFramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLRenderbufferType renderbuffertarget, WebGLRenderbuffer renderbuffer);
        void FramebufferTexture2D(WebGLFramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLTextureTarget texturetarget, WebGLTexture texture);
        void RenderbufferStorage(WebGLRenderbufferType target, WebGLRenderbufferInternalFormat internalFormat, int width, int height);
        WebGLFramebufferStatus CheckFramebufferStatus(WebGLFramebufferType target);
        void GenerateMipmap(WebGLTextureTarget target);
        void AttachShader(WebGLProgram program, WebGLShader vertexShader);
        string GetProgramInfoLog(WebGLProgram program);
        string GetShaderInfoLog(WebGLShader shader);

        int GetAttribLocation(WebGLProgram program, string name);
        WebGLUniformLocation GetUniformLocation(WebGLProgram program, string name);
        void Uniform1i(WebGLUniformLocation location, int v0);
        void Uniform2i(WebGLUniformLocation location, int v0, int v1);
        void Uniform3i(WebGLUniformLocation location, int v0, int v1, int v2);
        void Uniform4i(WebGLUniformLocation location, int v0, int v1, int v2, int v3);
        void Uniform1f(WebGLUniformLocation location, float v0);
        void Uniform2f(WebGLUniformLocation location, float v0, float v1);
        void Uniform3f(WebGLUniformLocation location, float v0, float v1, float v2);
        void Uniform4f(WebGLUniformLocation location, float v0, float v1, float v2, float v3);
        void Uniform1iv<TData>(WebGLUniformLocation location, TData[] value) where TData : struct;
        void Uniform2iv<TData>(WebGLUniformLocation location, TData[] value) where TData : struct;
        void Uniform3iv<TData>(WebGLUniformLocation location, TData[] value) where TData : struct;
        void Uniform4iv<TData>(WebGLUniformLocation location, TData[] value) where TData : struct;
        void Uniform1fv<TData>(WebGLUniformLocation location, TData[] value) where TData : struct;
        void Uniform2fv<TData>(WebGLUniformLocation location, TData[] value) where TData : struct;
        void Uniform3fv<TData>(WebGLUniformLocation location, TData[] value) where TData : struct;
        void Uniform4fv<TData>(WebGLUniformLocation location, TData[] value) where TData : struct;
        void UniformMatrix2fv<TData>(WebGLUniformLocation location, TData[] value) where TData : struct;
        void UniformMatrix3fv<TData>(WebGLUniformLocation location, TData[] value) where TData : struct;
        void UniformMatrix4fv<TData>(WebGLUniformLocation location, TData[] value) where TData : struct;

        void LinkProgram(WebGLProgram program);
        void BufferData(WebGLBufferType type, int size, WebGLBufferUsageHint usage);
        void BufferData<TData>(WebGLBufferType type, TData[] data, WebGLBufferUsageHint usage) where TData : struct;
        void BufferSubData<TData>(WebGLBufferType target, int offset, TData[] srcData, int length) where TData : struct;
        void VertexAttribPointer(int index, int size, WebGLDataType type, bool normalized, int stride, int offset);
        void EnableVertexAttribArray(int index);
        void DisableVertexAttribArray(int index);
        void UseProgram(WebGLProgram program);
        void DrawArrays(WebGLPrimitiveType mode, int first, int count);
        void DrawElements(WebGLPrimitiveType mode, int count, WebGLDataType type, int offset);

        void Flush();
        void Finish();
        bool GetExtension(string name);
        WebGLErrorCode GetError();
    }
}
