using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas
{
    public class Canvas : HTMLElement<Canvas>
    {
        private readonly int _fid_GetWidth;
        private readonly int _fid_SetWidth;
        private readonly int _fid_GetHeight;
        private readonly int _fid_SetHeight;
        private readonly int _fid_RegisterEvents;
        private readonly int _fid_Create2DContext;
        private readonly int _fid_CreateWebGLContext;
        private readonly int _fid_CreateWebGL2Context;
        private readonly int _fid_Create2DContext1;
        private readonly int _fid_CreateWebGLContext1;
        private readonly int _fid_CreateWebGL2Context1;
        private readonly int _fid_UnregisterEvents;


        public event EventHandler WebGLContextLost;
        public event EventHandler WebGLContextRestored;

        //get or set the width of the canvas
        public int Width
        { 
            get { return InvokeRetInt(_fid_GetWidth); }
            set { Invoke(_fid_SetWidth, value); }
        }

        //get or set the height of the canvas
        public int Height
        {
            get { return InvokeRetInt(_fid_GetHeight); }
            set { Invoke(_fid_SetHeight, value); }
        }

        CanvasRenderingContext _canvasRenderingContext;
        WebGL.WebGLRenderingContext _webglRenderingContext;
        WebGL.WebGL2RenderingContext _webgl2RenderingContext;


        private Canvas(int uid) : base(uid)
        {
            _fid_GetWidth = RegisterFunction("nkCanvas.GetWidth");
            _fid_SetWidth = RegisterFunction("nkCanvas.SetWidth");
            _fid_GetHeight = RegisterFunction("nkCanvas.GetHeight");
            _fid_SetHeight = RegisterFunction("nkCanvas.SetHeight");
            _fid_RegisterEvents = RegisterFunction("nkCanvas.RegisterEvents");
            _fid_Create2DContext = RegisterFunction("nkCanvas.Create2DContext");
            _fid_CreateWebGLContext = RegisterFunction("nkCanvas.CreateWebGLContext");
            _fid_CreateWebGL2Context = RegisterFunction("nkCanvas.CreateWebGL2Context");
            _fid_Create2DContext1 = RegisterFunction("nkCanvas.Create2DContext1");
            _fid_CreateWebGLContext1 = RegisterFunction("nkCanvas.CreateWebGLContext1");
            _fid_CreateWebGL2Context1 = RegisterFunction("nkCanvas.CreateWebGL2Context1");
            _fid_UnregisterEvents = RegisterFunction("nkCanvas.UnregisterEvents");
            Invoke(_fid_RegisterEvents);
        }

        [JSInvokable] 
        public static void JsCanvasOnWebGLContextLost(int uid)
        {
            Canvas canvas = Canvas.FromUid(uid);
            if (canvas == null)
                return;

            var handler = canvas.WebGLContextLost;
            if (handler != null)
                handler(canvas, EventArgs.Empty);
        }

        [JSInvokable]
        public static void JsCanvasOnWebGLContextRestored(int uid)
        {
            Canvas canvas = Canvas.FromUid(uid);
            if (canvas == null)
                return;

            var handler = canvas.WebGLContextRestored;
            if (handler != null)
                handler(canvas, EventArgs.Empty);
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

                _canvasRenderingContext = new CanvasRenderingContext(this, uid);

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

                _webglRenderingContext = new WebGL.WebGLRenderingContext(this, uid);

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
                    _webgl2RenderingContext = new WebGL.WebGL2RenderingContext(this, uid);

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
                
                _canvasRenderingContext = new CanvasRenderingContext(this, uid);

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

                _webglRenderingContext = new WebGL.WebGLRenderingContext(this, uid);

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
                    _webgl2RenderingContext = new WebGL.WebGL2RenderingContext(this, uid);

                return (TContext)(WebGL.IWebGL2RenderingContext)_webgl2RenderingContext;
            }

            throw new NotSupportedException();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Invoke(_fid_UnregisterEvents);

            base.Dispose(disposing);
        }

    }
}
