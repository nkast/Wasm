using System;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Canvas
{
    public class Canvas : JSObject
    {
        private readonly string _id;

        public string Id { get { return _id; } }

        //get or set the width of the canvas
        public int Width
        { 
            get { return InvokeRet<int>("nkCanvas.GetWidth"); }
            set { Invoke("nkCanvas.SetWidth", value); }
        }

        //get or set the height of the canvas
        public int Height
        {
            get { return InvokeRet<int>("nkCanvas.GetHeight"); }
            set { Invoke("nkCanvas.SetHeight", value); }
        }

        CanvasRenderingContext _canvasRenderingContext;
        WebGL.WebGLRenderingContext _webglRenderingContext;



        private Canvas(int uid) : base(uid)
        {
        }

        public TContext GetContext<TContext>()
            where TContext : IRenderingContext
        {
            if (typeof(TContext) == typeof(ICanvasRenderingContext))
            {
                //TODO: implement a Disposed event in IRenderingContext
                if (_canvasRenderingContext.IsDisposed)
                    _canvasRenderingContext = null;

                if (_canvasRenderingContext != null)
                    return (TContext)(IRenderingContext)_canvasRenderingContext;

                int uid = InvokeRet<int>("nkCanvas.Create2DContext");
                _canvasRenderingContext = new CanvasRenderingContext(this, uid);

                return (TContext)(IRenderingContext)_canvasRenderingContext;
            }

            if (typeof(TContext) == typeof(WebGL.IWebGLRenderingContext))
            {
                //TODO: implement a Disposed event in IRenderingContext
                if (_webglRenderingContext.IsDisposed)
                    _webglRenderingContext = null;
                
                if (_webglRenderingContext != null)
                    return (TContext)(WebGL.IWebGLRenderingContext)_webglRenderingContext;

                int uid = InvokeRet<int>("nkCanvas.CreateWebGLContext");
                _webglRenderingContext = new WebGL.WebGLRenderingContext(this, uid);

                return (TContext)(WebGL.IWebGLRenderingContext)_webglRenderingContext;
            }

            throw new NotSupportedException();
        }

    }
}
