using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Canvas
{
    public class OffscreenCanvas : JSObject
    {
        private static readonly int _fid_Create = RegisterFunction("nkOffscreenCanvas.Create");
        private readonly int _fid_GetWidth;
        private readonly int _fid_SetWidth;
        private readonly int _fid_GetHeight;
        private readonly int _fid_SetHeight;
        private readonly int _fid_Create2DContext;
        private readonly int _fid_CreateWebGLContext;
        private readonly int _fid_CreateWebGL2Context;
        private readonly int _fid_Create2DContext1;
        private readonly int _fid_CreateWebGLContext1;
        private readonly int _fid_CreateWebGL2Context1;

        //get or set the width of the OffscreenCanvas
        public int Width
        { 
            get { return InvokeRetInt(_fid_GetWidth); }
            set { Invoke(_fid_SetWidth, value); }
        }

        //get or set the height of the OffscreenCanvas
        public int Height
        {
            get { return InvokeRetInt(_fid_GetHeight); }
            set { Invoke(_fid_SetHeight, value); }
        }

        CanvasRenderingContext _canvasRenderingContext;
        WebGL.WebGLRenderingContext _webglRenderingContext;
        WebGL.WebGL2RenderingContext _webgl2RenderingContext;


        public OffscreenCanvas(int width, int height) : base(Register(width, height))
        {
            _fid_GetWidth = RegisterFunction("nkOffscreenCanvas.GetWidth");
            _fid_SetWidth = RegisterFunction("nkOffscreenCanvas.SetWidth");
            _fid_GetHeight = RegisterFunction("nkOffscreenCanvas.GetHeight");
            _fid_SetHeight = RegisterFunction("nkOffscreenCanvas.SetHeight");
            _fid_Create2DContext = RegisterFunction("nkOffscreenCanvas.Create2DContext");
            _fid_CreateWebGLContext = RegisterFunction("nkOffscreenCanvas.CreateWebGLContext");
            _fid_CreateWebGL2Context = RegisterFunction("nkCanvas.CreateWebGL2Context");
            _fid_Create2DContext1 = RegisterFunction("nkOffscreenCanvas.Create2DContext1");
            _fid_CreateWebGLContext1 = RegisterFunction("nkOffscreenCanvas.CreateWebGLContext1");
            _fid_CreateWebGL2Context1 = RegisterFunction("nkOffscreenCanvas.CreateWebGL2Context1");
        }

        private static int Register(int width, int height)
        {
            int uid = JSObject.StaticInvokeRetInt(_fid_Create, width, height);
            return uid;
        }

        public TContext GetContext<TContext>()
            where TContext : IRenderingContext
        {
            if (typeof(TContext) == typeof(ICanvasRenderingContext))
            {
                //TODO: implement a Disposed event in IRenderingContext
                if (_canvasRenderingContext != null && _canvasRenderingContext.IsDisposed)
                    _canvasRenderingContext = null;

                if (_canvasRenderingContext != null)
                    return (TContext)(IRenderingContext)_canvasRenderingContext;

                int uid = InvokeRetInt(_fid_Create2DContext);

                _canvasRenderingContext = new CanvasRenderingContext(null, uid);

                return (TContext)(IRenderingContext)_canvasRenderingContext;
            }

            if (typeof(TContext) == typeof(WebGL.IWebGLRenderingContext))
            {
                //TODO: implement a Disposed event in IRenderingContext
                if (_webglRenderingContext != null && _webglRenderingContext.IsDisposed)
                    _webglRenderingContext = null;
                
                if (_webglRenderingContext != null)
                    return (TContext)(WebGL.IWebGLRenderingContext)_webglRenderingContext;

                int uid = InvokeRetInt(_fid_CreateWebGLContext);

                _webglRenderingContext = new WebGL.WebGLRenderingContext(null, uid);

                return (TContext)(WebGL.IWebGLRenderingContext)_webglRenderingContext;
            }

            if (typeof(TContext) == typeof(WebGL.IWebGL2RenderingContext))
            {
                //TODO: implement a Disposed event in IRenderingContext
                if (_webgl2RenderingContext != null && _webgl2RenderingContext.IsDisposed)
                    _webgl2RenderingContext = null;

                if (_webgl2RenderingContext != null)
                    return (TContext)(WebGL.IWebGL2RenderingContext)_webgl2RenderingContext;

                int uid = InvokeRetInt(_fid_CreateWebGL2Context);
                if (uid > 0)
                    _webgl2RenderingContext = new WebGL.WebGL2RenderingContext(null, uid);

                return (TContext)(WebGL.IWebGL2RenderingContext)_webgl2RenderingContext;
            }

            throw new NotSupportedException();
        }

        public TContext GetContext<TContext>(ContextAttributes attributes)
            where TContext : IRenderingContext
        {
            if (attributes == null)
                throw new ArgumentNullException(nameof(attributes));

            if (typeof(TContext) == typeof(ICanvasRenderingContext))
            {
                //TODO: implement a Disposed event in IRenderingContext
                if (_canvasRenderingContext != null && _canvasRenderingContext.IsDisposed)
                    _canvasRenderingContext = null;

                if (_canvasRenderingContext != null)
                    return (TContext)(IRenderingContext)_canvasRenderingContext;

                if (attributes.Depth != null
                ||  attributes.Stencil != null
                ||  attributes.Antialias != null
                ||  attributes.PowerPreference != null
                ||  attributes.PremultipliedAlpha != null
                ||  attributes.PreserveDrawingBuffer != null
                ||  attributes.XrCompatible != null)
                    throw new ArgumentException("attributes are not valid for 2d canvas context.", nameof(attributes));

                int uid = InvokeRetInt<int>(_fid_Create2DContext1, attributes.ToBit());
                
                _canvasRenderingContext = new CanvasRenderingContext(null, uid);

                return (TContext)(IRenderingContext)_canvasRenderingContext;
            }

            if (typeof(TContext) == typeof(WebGL.IWebGLRenderingContext))
            {
                //TODO: implement a Disposed event in IRenderingContext
                if (_webglRenderingContext != null && _webglRenderingContext.IsDisposed)
                    _webglRenderingContext = null;

                if (_webglRenderingContext != null)
                    return (TContext)(WebGL.IWebGLRenderingContext)_webglRenderingContext;

                int uid = InvokeRetInt<int>(_fid_CreateWebGLContext1, attributes.ToBit());

                _webglRenderingContext = new WebGL.WebGLRenderingContext(null, uid);

                return (TContext)(WebGL.IWebGLRenderingContext)_webglRenderingContext;
            }

            if (typeof(TContext) == typeof(WebGL.IWebGL2RenderingContext))
            {
                //TODO: implement a Disposed event in IRenderingContext
                if (_webgl2RenderingContext != null && _webgl2RenderingContext.IsDisposed)
                    _webgl2RenderingContext = null;

                if (_webgl2RenderingContext != null)
                    return (TContext)(WebGL.IWebGL2RenderingContext)_webgl2RenderingContext;

                int uid = InvokeRetInt<int>(_fid_CreateWebGL2Context1, attributes.ToBit());
                if (uid > 0)
                    _webgl2RenderingContext = new WebGL.WebGL2RenderingContext(null, uid);

                return (TContext)(WebGL.IWebGL2RenderingContext)_webgl2RenderingContext;
            }

            throw new NotSupportedException();
        }
    }
}
