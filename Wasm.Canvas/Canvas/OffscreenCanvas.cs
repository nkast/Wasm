using System;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas
{
    public class OffscreenCanvas : JSObject
    {
        //get or set the width of the OffscreenCanvas
        public int Width
        { 
            get { return InvokeRetInt("nkOffscreenCanvas.GetWidth"); }
            set { Invoke("nkOffscreenCanvas.SetWidth", value); }
        }

        //get or set the height of the OffscreenCanvas
        public int Height
        {
            get { return InvokeRetInt("nkOffscreenCanvas.GetHeight"); }
            set { Invoke("nkOffscreenCanvas.SetHeight", value); }
        }

        CanvasRenderingContext _canvasRenderingContext;
        WebGL.WebGLRenderingContext _webglRenderingContext;
        WebGL.WebGL2RenderingContext _webgl2RenderingContext;


        public OffscreenCanvas(int width, int height) : base(Register(width, height))
        {
        }

        private static int Register(int width, int height)
        {
            int uid = JSObject.StaticInvokeRetInt("nkOffscreenCanvas.Create", width, height);
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

                int uid = InvokeRetInt("nkOffscreenCanvas.Create2DContext");

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

                int uid = InvokeRetInt("nkOffscreenCanvas.CreateWebGLContext");

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

                int uid = InvokeRetInt("nkCanvas.CreateWebGL2Context");
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

                int uid = InvokeRetInt<int>("nkOffscreenCanvas.Create2DContext1", attributes.ToBit());
                
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

                int uid = InvokeRetInt<int>("nkOffscreenCanvas.CreateWebGLContext1", attributes.ToBit());

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

                int uid = InvokeRetInt<int>("nkOffscreenCanvas.CreateWebGL2Context1", attributes.ToBit());
                if (uid > 0)
                    _webgl2RenderingContext = new WebGL.WebGL2RenderingContext(null, uid);

                return (TContext)(WebGL.IWebGL2RenderingContext)_webgl2RenderingContext;
            }

            throw new NotSupportedException();
        }
    }
}
