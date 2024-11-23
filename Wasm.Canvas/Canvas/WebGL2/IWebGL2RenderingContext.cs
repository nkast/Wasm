using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas.WebGL
{
    public interface IWebGL2RenderingContext : IWebGLRenderingContext
    {
        int GetParameter(WebGL2PNameInteger pname);
        void BindFramebuffer(WebGL2FramebufferType target, WebGLFramebuffer framebuffer);
        void FramebufferRenderbuffer(WebGL2FramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLRenderbufferType renderbuffertarget, WebGLRenderbuffer renderbuffer);
        void FramebufferTexture2D(WebGL2FramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLTextureTarget texturetarget, WebGLTexture texture);
        void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, WebGLBufferBits mask, WebGLTexParam filter);
        void ReadBuffer(WebGL2DrawBufferAttachmentPoint buffer);
        void DrawBuffer(WebGL2DrawBufferAttachmentPoint buffer);
        void DrawBuffers(WebGL2DrawBufferAttachmentPoint[] buffers);
        void DrawBuffers(WebGL2DrawBufferAttachmentPoint[] buffers, int startIndex, int length);
        void DrawRangeElements(WebGLPrimitiveType mode, int start, int end, int count, WebGLDataType type, int offset);
        void GetBufferSubData<TData>(WebGLBufferType target, int offset, TData[] dstData) where TData : struct;
        void GetBufferSubData<TData>(WebGLBufferType target, int offset, TData[] dstData, int startIndex) where TData : struct;
        void GetBufferSubData<TData>(WebGLBufferType target, int offset, TData[] dstData, int startIndex, int length) where TData : struct;
        WebGL2FramebufferStatus CheckFramebufferStatus(WebGL2FramebufferType target);
    }
}
