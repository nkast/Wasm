using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas.WebGL
{
    public interface IWebGL2RenderingContext : IWebGLRenderingContext
    {
        void DrawBuffer(WebGL2DrawBufferAttachmentPoint buffer);
        void DrawBuffers(WebGL2DrawBufferAttachmentPoint[] buffers);
        void DrawBuffers(WebGL2DrawBufferAttachmentPoint[] buffers, int startIndex, int length);
        void DrawRangeElements(WebGLPrimitiveType mode, int start, int end, int count, WebGLDataType type, int offset);
        void GetBufferSubData<TData>(WebGLBufferType target, int offset, TData[] dstData) where TData : struct;
        void GetBufferSubData<TData>(WebGLBufferType target, int offset, TData[] dstData, int startIndex) where TData : struct;
        void GetBufferSubData<TData>(WebGLBufferType target, int offset, TData[] dstData, int startIndex, int length) where TData : struct;
    }
}
