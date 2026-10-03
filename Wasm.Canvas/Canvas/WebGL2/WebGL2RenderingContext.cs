using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas.WebGL
{
    internal class WebGL2RenderingContext : WebGLRenderingContext, IWebGL2RenderingContext, IDisposable
    {
        private readonly int _fid_InvalidateFramebuffer;
        private readonly int _fid_BlitFramebuffer;
        private readonly int _fid_FramebufferTextureLayer;
        private readonly int _fid_ReadBuffer;
        private readonly int _fid_DrawBuffer;
        private readonly int _fid_DrawBuffers;
        private readonly int _fid_DrawRangeElements;
        private readonly int _fid_GetBufferSubData;
        private readonly int _fid_GetBufferSubData1;
        private readonly int _fid_GetBufferSubData2;
        private readonly int _fid_RenderbufferStorageMultisample;
        private readonly int _fid_TexImage3D;
        private readonly int _fid_TexImage3D1;
        private readonly int _fid_TexSubImage3D;
        private readonly int _fid_CompressedTexImage3D;
        private readonly int _fid_CompressedTexSubImage3D;
        private readonly int _fid_VertexAttribDivisor;
        private readonly int _fid_DrawElementsInstanced;
        private readonly int _fid_CreateQuery;
        private readonly int _fid_DeleteQuery;
        private readonly int _fid_IsQuery;
        private readonly int _fid_BeginQuery;
        private readonly int _fid_EndQuery;
        private readonly int _fid_GetQuery;
        private readonly int _fid_GetQueryParameter;

        WebGL2DrawBuffersIndexedExtension _drawBuffersIndexedExtension;

        internal WebGL2RenderingContext(Canvas canvas, int uid) : base(canvas, uid)
        {
            _fid_InvalidateFramebuffer = RegisterFunction("nkCanvasGL2Context.InvalidateFramebuffer");
            _fid_BlitFramebuffer = RegisterFunction("nkCanvasGL2Context.BlitFramebuffer");
            _fid_FramebufferTextureLayer = RegisterFunction("nkCanvasGL2Context.FramebufferTextureLayer");
            _fid_ReadBuffer = RegisterFunction("nkCanvasGL2Context.ReadBuffer");
            _fid_DrawBuffer = RegisterFunction("nkCanvasGL2Context.DrawBuffer");
            _fid_DrawBuffers = RegisterFunction("nkCanvasGL2Context.DrawBuffers");
            _fid_DrawRangeElements = RegisterFunction("nkCanvasGL2Context.DrawRangeElements");
            _fid_GetBufferSubData = RegisterFunction("nkCanvasGL2Context.GetBufferSubData");
            _fid_GetBufferSubData1 = RegisterFunction("nkCanvasGL2Context.GetBufferSubData1");
            _fid_GetBufferSubData2 = RegisterFunction("nkCanvasGL2Context.GetBufferSubData2");
            _fid_RenderbufferStorageMultisample = RegisterFunction("nkCanvasGL2Context.RenderbufferStorageMultisample");
            _fid_TexImage3D = RegisterFunction("nkCanvasGL2Context.TexImage3D");
            _fid_TexImage3D1 = RegisterFunction("nkCanvasGL2Context.TexImage3D1");
            _fid_TexSubImage3D = RegisterFunction("nkCanvasGL2Context.TexSubImage3D");
            _fid_CompressedTexImage3D = RegisterFunction("nkCanvasGL2Context.CompressedTexImage3D");
            _fid_CompressedTexSubImage3D = RegisterFunction("nkCanvasGL2Context.CompressedTexSubImage3D");
            _fid_VertexAttribDivisor = RegisterFunction("nkCanvasGL2Context.VertexAttribDivisor");
            _fid_DrawElementsInstanced = RegisterFunction("nkCanvasGL2Context.DrawElementsInstanced");
            _fid_CreateQuery = RegisterFunction("nkCanvasGL2Context.CreateQuery");
            _fid_DeleteQuery = RegisterFunction("nkCanvasGL2Context.DeleteQuery");
            _fid_IsQuery = RegisterFunction("nkCanvasGL2Context.IsQuery");
            _fid_BeginQuery = RegisterFunction("nkCanvasGL2Context.BeginQuery");
            _fid_EndQuery = RegisterFunction("nkCanvasGL2Context.EndQuery");
            _fid_GetQuery = RegisterFunction("nkCanvasGL2Context.GetQuery");
            _fid_GetQueryParameter = RegisterFunction("nkCanvasGL2Context.GetQueryParameter");
        }

        public WebGL2DrawBuffersIndexedExtension DrawBuffersIndexedExtension
        {
            get
            {
                if (_drawBuffersIndexedExtension == null)
                    _drawBuffersIndexedExtension = GetExtension<WebGL2DrawBuffersIndexedExtension>("OES_draw_buffers_indexed");

                return _drawBuffersIndexedExtension;
            }
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
                Invoke(_fid_InvalidateFramebuffer, (int)target, 0, attachments.Length, (int)pAttachments);
            }
        }

        public unsafe void InvalidateFramebuffer(WebGL2FramebufferType target, Span<WebGLFramebufferAttachmentPoint> attachments)
        {
            fixed (WebGLFramebufferAttachmentPoint* pAttachments = attachments)
            {
                Invoke(_fid_InvalidateFramebuffer, (int)target, 0, attachments.Length, (int)pAttachments);
            }
        }

        public unsafe void InvalidateFramebuffer(WebGL2FramebufferType target, WebGLFramebufferAttachmentPoint[] attachments, int startIndex, int length)
        {
            fixed (WebGLFramebufferAttachmentPoint* pAttachments = attachments)
            {
                Invoke(_fid_InvalidateFramebuffer, (int)target, startIndex, length, (int)pAttachments);
            }
        }

        public void BlitFramebuffer(int srcX0, int srcY0, int srcX1, int srcY1, int dstX0, int dstY0, int dstX1, int dstY1, WebGLBufferBits mask, WebGLTexParam filter)
        {
            Invoke(_fid_BlitFramebuffer, srcX0, srcY0, srcX1, srcY1, 
                                                         dstX0, dstY0, dstX1, dstY1,
                                                         (int)mask, (int)filter);
        }

        public void FramebufferTextureLayer(WebGL2FramebufferType target, WebGLFramebufferAttachmentPoint attachment, WebGLTexture texture, int level, int layer)
        {
            int uid = (texture != null) ? texture.Uid : -1;
            Invoke(_fid_FramebufferTextureLayer, (int)target, attachment, uid, level, layer);
        }

        public void ReadBuffer(WebGL2DrawBufferAttachmentPoint buffer)
        {
            Invoke(_fid_ReadBuffer, (int) buffer);
        }

        public void DrawBuffer(WebGL2DrawBufferAttachmentPoint buffer)
        {
            Invoke(_fid_DrawBuffer, (int)buffer);
        }

        public unsafe void DrawBuffers(WebGL2DrawBufferAttachmentPoint[] buffers)
        {
            fixed (WebGL2DrawBufferAttachmentPoint* pBuffers = buffers)
            {
                Invoke(_fid_DrawBuffers, 0, buffers.Length, (int)pBuffers);
            }
        }

        public unsafe void DrawBuffers(Span<WebGL2DrawBufferAttachmentPoint> buffers)
        {
            fixed (WebGL2DrawBufferAttachmentPoint* pBuffers = buffers)
            {
                Invoke(_fid_DrawBuffers, 0, buffers.Length, (int)pBuffers);
            }
        }

        public unsafe void DrawBuffers(WebGL2DrawBufferAttachmentPoint[] buffers, int startIndex, int length)
        {
            fixed (WebGL2DrawBufferAttachmentPoint* pBuffers = buffers)
            {
                Invoke(_fid_DrawBuffers, startIndex, length, (int)pBuffers);
            }
        }

        public void DrawRangeElements(WebGLPrimitiveType mode, int start, int end, int count, WebGLDataType type, int offset)
        {
            //Invoke("nkCanvasGLContext.DrawElements", (int)mode, count, (int)type, offset);
            Invoke(_fid_DrawRangeElements, (int)mode, start, end, count, (int)type, offset);
        }

        public unsafe void GetBufferSubData<TData>(WebGLBufferType target, int offset, TData[] dstData) where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pDstData = dstData)
            {
                Invoke(_fid_GetBufferSubData, (int)target, offset, stride, (int)pDstData, dstData.Length);
            }
        }

        public unsafe void GetBufferSubData<TData>(WebGLBufferType target, int offset, Span<TData> dstData) where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pDstData = dstData)
            {
                Invoke(_fid_GetBufferSubData, (int)target, offset, stride, (int)pDstData, dstData.Length);
            }
        }

        public unsafe void GetBufferSubData<TData>(WebGLBufferType target, int offset, TData[] dstData, int startIndex) where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pDstData = dstData)
            {
                Invoke(_fid_GetBufferSubData1, (int)target, offset, startIndex, stride, (int)pDstData, dstData.Length);
            }
        }

        public unsafe void GetBufferSubData<TData>(WebGLBufferType target, int offset, TData[] dstData, int startIndex, int length) where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pDstData = dstData)
            {
                Invoke(_fid_GetBufferSubData2, (int)target, offset, startIndex, length, stride, (int)pDstData, dstData.Length);
            }
        }

        public void RenderbufferStorage(WebGLRenderbufferType target, WebGL2RenderbufferInternalFormat internalFormat, int width, int height)
        {
            base.RenderbufferStorage(target, (WebGLRenderbufferInternalFormat)internalFormat, width, height);
        }

        public void RenderbufferStorageMultisample(WebGLRenderbufferType target, int samples, WebGL2RenderbufferInternalFormat internalFormat, int width, int height)
        {
            Invoke(_fid_RenderbufferStorageMultisample, (int)target, samples, (int)internalFormat, width, height);
        }

        public WebGL2FramebufferStatus CheckFramebufferStatus(WebGL2FramebufferType target)
        {
            return (WebGL2FramebufferStatus)base.CheckFramebufferStatus((WebGLFramebufferType)target);
        }

        public void TexImage3D(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, int depth, WebGLFormat format, WebGLTexelType type)
        {
            Invoke(_fid_TexImage3D, (int)target, level, (int)internalFormat, width, height, depth, (int)format, (int)type);
        }

        public unsafe void TexImage3D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, int depth, WebGLFormat format, WebGLTexelType type, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_TexImage3D1, (int)target, level, (int)internalFormat, width, height, depth, (int)format, (int)type, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void TexImage3D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, int depth, WebGLFormat format, WebGLTexelType type, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_TexImage3D1, (int)target, level, (int)internalFormat, width, height, depth, (int)format, (int)type, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void TexImage3D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, int depth, WebGLFormat format, WebGLTexelType type, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_TexImage3D1, (int)target, level, (int)internalFormat, width, height, depth, (int)format, (int)type, stride, (int)pPixels, index, count);
            }
        }

        public unsafe void TexSubImage3D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, WebGLFormat format, WebGLTexelType type, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int, int>(xoffset, yoffset, zoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_TexSubImage3D, (int)target, level, position, width, height, depth, (int)format, (int)type, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void TexSubImage3D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, WebGLFormat format, WebGLTexelType type, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int, int>(xoffset, yoffset, zoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_TexSubImage3D, (int)target, level, position, width, height, depth, (int)format, (int)type, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void TexSubImage3D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, WebGLFormat format, WebGLTexelType type, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int, int>(xoffset, yoffset, zoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_TexSubImage3D, (int)target, level, position, width, height, depth, (int)format, (int)type, stride, (int)pPixels, index, count);
            }
        }

        public unsafe void CompressedTexImage3D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, int depth, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_CompressedTexImage3D, (int)target, level, (int)internalFormat, width, height, depth, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexImage3D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, int depth, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_CompressedTexImage3D, (int)target, level, (int)internalFormat, width, height, depth, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexImage3D<TData>(WebGLTextureTarget target, int level, WebGLInternalFormat internalFormat, int width, int height, int depth, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_CompressedTexImage3D, (int)target, level, (int)internalFormat, width, height, depth, stride, (int)pPixels, index, count);
            }
        }

        public unsafe void CompressedTexSubImage3D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, WebGLFormat format, TData[] pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int, int>(xoffset, yoffset, zoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_CompressedTexSubImage3D, (int)target, level, position, width, height, depth, (int)format, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexSubImage3D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, WebGLFormat format, Span<TData> pixels)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int, int>(xoffset, yoffset, zoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_CompressedTexSubImage3D, (int)target, level, position, width, height, depth, (int)format, stride, (int)pPixels, 0, pixels.Length);
            }
        }

        public unsafe void CompressedTexSubImage3D<TData>(WebGLTextureTarget target, int level, int xoffset, int yoffset, int zoffset, int width, int height, int depth, WebGLFormat format, TData[] pixels, int index, int count)
            where TData : struct
        {
            int stride = Marshal.SizeOf<TData>();
            var position = ValueTuple.Create<int, int, int>(xoffset, yoffset, zoffset);
            fixed (TData* pPixels = pixels)
            {
                Invoke(_fid_CompressedTexSubImage3D, (int)target, level, position, width, height, depth, (int)format, stride, (int)pPixels, index, count);
            }
        }

        public void VertexAttribDivisor(int index, int divisor)
        {
            Invoke(_fid_VertexAttribDivisor, index, divisor);
        }

        public void DrawElementsInstanced(WebGLPrimitiveType mode, int count, WebGLDataType type, int offset, int instanceCount)
        {
            Invoke(_fid_DrawElementsInstanced, (int)mode, count, type, offset, instanceCount);
        }

        public WebGL2Query CreateQuery()
        {
            int uid = InvokeRetInt(_fid_CreateQuery);
            return new WebGL2Query(uid, this);
        }

        public void DeleteQuery(WebGL2Query query)
        {
            Invoke(_fid_DeleteQuery, query.Uid);
        }

        public bool IsQuery(WebGL2Query query)
        {
            return InvokeRetBool(_fid_IsQuery, query.Uid);
        }

        public void BeginQuery(WebGL2QueryType target, WebGL2Query query)
        {
            Invoke(_fid_BeginQuery, (int)target, query.Uid);
        }

        public void EndQuery(WebGL2QueryType target)
        {
            Invoke(_fid_EndQuery, (int)target);
        }

        public WebGL2Query GetQuery(WebGL2QueryType target, WebGL2QueryParam pname)
        {
            int uid = InvokeRetInt(_fid_GetQuery, (int)target, (int)pname);
            if (uid == -1)
                return null;

            WebGL2Query query = WebGL2Query.FromUid(uid);
            return query;
        }

        public int GetQueryParameter(WebGL2Query query, WebGL2QueryParam pname)
        {
            return InvokeRetInt(_fid_GetQueryParameter, query.Uid, (int)pname);
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
