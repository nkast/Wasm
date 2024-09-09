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
