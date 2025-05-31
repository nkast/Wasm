using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas.WebGL
{
    internal class WebGL2RenderingContext : WebGLRenderingContext, IWebGL2RenderingContext, IDisposable
    {
        internal WebGL2RenderingContext(Canvas canvas, int uid) : base(canvas, uid)
        {
        }

        public int GetParameter(WebGL2PNameInteger pname)
        {
            return base.GetParameter((WebGLPNameInteger)pname);
        }

        public void BindFramebuffer(WebGL2FramebufferType target, WebGLFramebuffer framebuffer)
        {
            base.BindFramebuffer((WebGLFramebufferType)target, framebuffer);
        }

        public void FramebufferRenderbuffer(WebGL2FramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLRenderbufferType renderbuffertarget, WebGLRenderbuffer renderbuffer)
        {
            base.FramebufferRenderbuffer((WebGLFramebufferType)target, attachment, renderbuffertarget, renderbuffer);
        }

        public void FramebufferTexture2D(WebGL2FramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLTextureTarget texturetarget, WebGLTexture texture)
        {
            base.FramebufferTexture2D((WebGLFramebufferType)target, attachment, texturetarget, texture);
        }

        public void InvalidateFramebuffer(WebGL2FramebufferType target, WebGLFramebufferAttachmentPoint[] attachments)
        {
            Invoke("nkCanvasGL2Context.InvalidateFramebuffer", (int)target, 0, attachments.Length, attachments);
        }

        public void InvalidateFramebuffer(WebGL2FramebufferType target, WebGLFramebufferAttachmentPoint[] attachments, int startIndex, int length)
        {
            Invoke("nkCanvasGL2Context.InvalidateFramebuffer", (int)target, startIndex, length, attachments);
        }

        public void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, WebGLBufferBits mask, WebGLTexParam filter)
        {
            Invoke("nkCanvasGL2Context.BlitFramebuffer", srcX0, srcY0, srcX1, srcY1, 
                                                         dstX0, dstY0, dstX1, dstY1,
                                                         (int)mask, (int)filter);
        }

        public void ReadBuffer(WebGL2DrawBufferAttachmentPoint buffer)
        {
            Invoke("nkCanvasGL2Context.ReadBuffer", (int) buffer);
        }

        public void DrawBuffer(WebGL2DrawBufferAttachmentPoint buffer)
        {
            Invoke("nkCanvasGL2Context.DrawBuffer", (int)buffer);
        }

        public void DrawBuffers(WebGL2DrawBufferAttachmentPoint[] buffers)
        {
            Invoke("nkCanvasGL2Context.DrawBuffers", 0, buffers.Length, buffers);
        }

        public void DrawBuffers(WebGL2DrawBufferAttachmentPoint[] buffers, int startIndex, int length)
        {
            Invoke("nkCanvasGL2Context.DrawBuffers", startIndex, length, buffers);
        }

        public void DrawRangeElements(WebGLPrimitiveType mode, int start, int end, int count, WebGLDataType type, int offset)
        {
            //Invoke("nkCanvasGLContext.DrawElements", (int)mode, count, (int)type, offset);
            Invoke("nkCanvasGL2Context.DrawRangeElements", (int)mode, start, end, count, (int)type, offset);
        }

        public void GetBufferSubData<TData>(WebGLBufferType target, int offset, TData[] dstData) where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGL2Context.GetBufferSubData", (int)target, offset, stride, dstData);
        }

        public void GetBufferSubData<TData>(WebGLBufferType target, int offset, TData[] dstData, int startIndex) where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGL2Context.GetBufferSubData1", (int)target, offset, startIndex, stride, dstData);
        }

        public void GetBufferSubData<TData>(WebGLBufferType target, int offset, TData[] dstData, int startIndex, int length) where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            Invoke("nkCanvasGL2Context.GetBufferSubData2", (int)target, offset, startIndex, length, stride, dstData);
        }

        public void RenderbufferStorage(WebGLRenderbufferType target, WebGL2RenderbufferInternalFormat internalFormat, int width, int height)
        {
            base.RenderbufferStorage(target, (WebGLRenderbufferInternalFormat)internalFormat, width, height);
        }

        public void RenderbufferStorageMultisample(WebGLRenderbufferType target, int samples, WebGL2RenderbufferInternalFormat internalFormat, int width, int height)
        {
            Invoke("nkCanvasGL2Context.RenderbufferStorageMultisample", (int)target, samples, (int)internalFormat, width, height);
        }

        public WebGL2FramebufferStatus CheckFramebufferStatus(WebGL2FramebufferType target)
        {
            return (WebGL2FramebufferStatus)base.CheckFramebufferStatus((WebGLFramebufferType)target);
        }

        public void VertexAttribDivisor(int index, int divisor)
        {
            Invoke("nkCanvasGL2Context.VertexAttribDivisor", index, divisor);
        }

        public void DrawElementsInstanced(WebGLPrimitiveType mode, int count, WebGLDataType type, int offset, int instanceCount)
        {
            Invoke("nkCanvasGL2Context.DrawElementsInstanced", (int)mode, count, type, offset, instanceCount);
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
