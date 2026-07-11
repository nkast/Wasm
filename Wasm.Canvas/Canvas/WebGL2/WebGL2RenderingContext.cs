using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using nkast.Wasm.JSInterop;

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

        public unsafe void InvalidateFramebuffer(WebGL2FramebufferType target, WebGLFramebufferAttachmentPoint[] attachments)
        {
            fixed (WebGLFramebufferAttachmentPoint* pAttachments = attachments)
            {
                Invoke("nkCanvasGL2Context.InvalidateFramebuffer", (int)target, 0, attachments.Length, (int)pAttachments);
            }
        }

        public unsafe void InvalidateFramebuffer(WebGL2FramebufferType target, Span<WebGLFramebufferAttachmentPoint> attachments)
        {
            fixed (WebGLFramebufferAttachmentPoint* pAttachments = attachments)
            {
                Invoke("nkCanvasGL2Context.InvalidateFramebuffer", (int)target, 0, attachments.Length, (int)pAttachments);
            }
        }

        public unsafe void InvalidateFramebuffer(WebGL2FramebufferType target, WebGLFramebufferAttachmentPoint[] attachments, int startIndex, int length)
        {
            fixed (WebGLFramebufferAttachmentPoint* pAttachments = attachments)
            {
                Invoke("nkCanvasGL2Context.InvalidateFramebuffer", (int)target, startIndex, length, (int)pAttachments);
            }
        }

        public void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, WebGLBufferBits mask, WebGLTexParam filter)
        {
            Invoke("nkCanvasGL2Context.BlitFramebuffer", srcX0, srcY0, srcX1, srcY1, 
                                                         dstX0, dstY0, dstX1, dstY1,
                                                         (int)mask, (int)filter);
        }

        public void FramebufferTextureLayer(WebGL2FramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLTexture texture, int level, int layer)
        {
            int uid = (texture != null) ? texture.Uid : -1;
            Invoke("nkCanvasGL2Context.FramebufferTextureLayer", (int)target, attachment, uid, level, layer);
        }

        public void ReadBuffer(WebGL2DrawBufferAttachmentPoint buffer)
        {
            Invoke("nkCanvasGL2Context.ReadBuffer", (int) buffer);
        }

        public void DrawBuffer(WebGL2DrawBufferAttachmentPoint buffer)
        {
            Invoke("nkCanvasGL2Context.DrawBuffer", (int)buffer);
        }

        public unsafe void DrawBuffers(WebGL2DrawBufferAttachmentPoint[] buffers)
        {
            fixed (WebGL2DrawBufferAttachmentPoint* pBuffers = buffers)
            {
                Invoke("nkCanvasGL2Context.DrawBuffers", 0, buffers.Length, (int)pBuffers);
            }
        }

        public unsafe void DrawBuffers(Span<WebGL2DrawBufferAttachmentPoint> buffers)
        {
            fixed (WebGL2DrawBufferAttachmentPoint* pBuffers = buffers)
            {
                Invoke("nkCanvasGL2Context.DrawBuffers", 0, buffers.Length, (int)pBuffers);
            }
        }

        public unsafe void DrawBuffers(WebGL2DrawBufferAttachmentPoint[] buffers, int startIndex, int length)
        {
            fixed (WebGL2DrawBufferAttachmentPoint* pBuffers = buffers)
            {
                Invoke("nkCanvasGL2Context.DrawBuffers", startIndex, length, (int)pBuffers);
            }
        }

        public void DrawRangeElements(WebGLPrimitiveType mode, int start, int end, int count, WebGLDataType type, int offset)
        {
            //Invoke("nkCanvasGLContext.DrawElements", (int)mode, count, (int)type, offset);
            Invoke("nkCanvasGL2Context.DrawRangeElements", (int)mode, start, end, count, (int)type, offset);
        }

        public unsafe void GetBufferSubData<TData>(WebGLBufferType target, int offset, TData[] dstData) where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pDstData = dstData)
            {
                Invoke("nkCanvasGL2Context.GetBufferSubData", (int)target, offset, stride, (int)pDstData, dstData.Length);
            }
        }

        public unsafe void GetBufferSubData<TData>(WebGLBufferType target, int offset, Span<TData> dstData) where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pDstData = dstData)
            {
                Invoke("nkCanvasGL2Context.GetBufferSubData", (int)target, offset, stride, (int)pDstData, dstData.Length);
            }
        }

        public unsafe void GetBufferSubData<TData>(WebGLBufferType target, int offset, TData[] dstData, int startIndex) where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pDstData = dstData)
            {
                Invoke("nkCanvasGL2Context.GetBufferSubData1", (int)target, offset, startIndex, stride, (int)pDstData, dstData.Length);
            }
        }

        public unsafe void GetBufferSubData<TData>(WebGLBufferType target, int offset, TData[] dstData, int startIndex, int length) where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pDstData = dstData)
            {
                Invoke("nkCanvasGL2Context.GetBufferSubData2", (int)target, offset, startIndex, length, stride, (int)pDstData, dstData.Length);
            }
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

        public void TexImage3D(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, int depth, WebGLFormat format, WebGLTexelType type)
        {
            Invoke("nkCanvasGL2Context.TexImage3D", (int)target, level, (int)internalFormat, width, height, depth, (int)format, (int)type);
        }

        public unsafe void TexImage3D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, int depth, WebGLFormat format, WebGLTexelType type, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke("nkCanvasGL2Context.TexImage3D1", (int)target, level, (int)internalFormat, width, height, depth, (int)format, (int)type, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void TexImage3D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, int depth, WebGLFormat format, WebGLTexelType type, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke("nkCanvasGL2Context.TexImage3D1", (int)target, level, (int)internalFormat, width, height, depth, (int)format, (int)type, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void TexImage3D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, int depth, WebGLFormat format, WebGLTexelType type, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke("nkCanvasGL2Context.TexImage3D1", (int)target, level, (int)internalFormat, width, height, depth, (int)format, (int)type, stride, (int)pPixels, index, count);
            }
        }

        public unsafe void TexSubImage3D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, WebGLFormat format, WebGLTexelType type, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int, int>(xoffset, yoffset, zoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke("nkCanvasGL2Context.TexSubImage3D", (int)target, level, position, width, height, depth, (int)format, (int)type, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void TexSubImage3D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, WebGLFormat format, WebGLTexelType type, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int, int>(xoffset, yoffset, zoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke("nkCanvasGL2Context.TexSubImage3D", (int)target, level, position, width, height, depth, (int)format, (int)type, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void TexSubImage3D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, WebGLFormat format, WebGLTexelType type, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int, int>(xoffset, yoffset, zoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke("nkCanvasGL2Context.TexSubImage3D", (int)target, level, position, width, height, depth, (int)format, (int)type, stride, (int)pPixels, index, count);
            }
        }

        public unsafe void CompressedTexImage3D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, int depth, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke("nkCanvasGL2Context.CompressedTexImage3D", (int)target, level, (int)internalFormat, width, height, depth, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexImage3D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, int depth, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke("nkCanvasGL2Context.CompressedTexImage3D", (int)target, level, (int)internalFormat, width, height, depth, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexImage3D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, int depth, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke("nkCanvasGL2Context.CompressedTexImage3D", (int)target, level, (int)internalFormat, width, height, depth, stride, (int)pPixels, index, count);
            }
        }

        public unsafe void CompressedTexSubImage3D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, WebGLFormat format, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int, int>(xoffset, yoffset, zoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke("nkCanvasGL2Context.CompressedTexSubImage3D", (int)target, level, position, width, height, depth, (int)format, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexSubImage3D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, WebGLFormat format, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int, int>(xoffset, yoffset, zoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke("nkCanvasGL2Context.CompressedTexSubImage3D", (int)target, level, position, width, height, depth, (int)format, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexSubImage3D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, WebGLFormat format, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int, int>(xoffset, yoffset, zoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke("nkCanvasGL2Context.CompressedTexSubImage3D", (int)target, level, position, width, height, depth, (int)format, stride, (int)pPixels, index, count);
            }
        }

        public void VertexAttribDivisor(int index, int divisor)
        {
            Invoke("nkCanvasGL2Context.VertexAttribDivisor", index, divisor);
        }

        public void DrawElementsInstanced(WebGLPrimitiveType mode, int count, WebGLDataType type, int offset, int instanceCount)
        {
            Invoke("nkCanvasGL2Context.DrawElementsInstanced", (int)mode, count, type, offset, instanceCount);
        }

        public WebGL2Query CreateQuery()
        {
            int uid = InvokeRetInt("nkCanvasGL2Context.CreateQuery");
            return new WebGL2Query(uid, this);
        }

        public void DeleteQuery(WebGL2Query query)
        {
            Invoke("nkCanvasGL2Context.DeleteQuery", query.Uid);
        }

        public bool IsQuery(WebGL2Query query)
        {
            return InvokeRetBool("nkCanvasGL2Context.IsQuery", query.Uid);
        }

        public void BeginQuery(WebGL2QueryType target, WebGL2Query query)
        {
            Invoke("nkCanvasGL2Context.BeginQuery", (int)target, query.Uid);
        }

        public void EndQuery(WebGL2QueryType target)
        {
            Invoke("nkCanvasGL2Context.EndQuery", (int)target);
        }

        public WebGL2Query GetQuery(WebGL2QueryType target, WebGL2QueryParam pname)
        {
            int uid = InvokeRetInt("nkCanvasGL2Context.GetQuery", (int)target, (int)pname);
            if (uid == -1)
                return null;

            WebGL2Query query = WebGL2Query.FromUid(uid);
            return query;
        }

        public int GetQueryParameter(WebGL2Query query, WebGL2QueryParam pname)
        {
            return InvokeRetInt("nkCanvasGL2Context.GetQueryParameter", query.Uid, (int)pname);
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
