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
                if (_canvasRenderingContext == null || _canvasRenderingContext.IsDisposed)
                {
                    int uid = InvokeRet<int>("nkCanvas.Create2DContext");
                    _canvasRenderingContext = new CanvasRenderingContext(this, uid);
                }

                return (TContext)(IRenderingContext)_canvasRenderingContext;
            }

            if (typeof(TContext) == typeof(WebGL.IWebGLRenderingContext))
            {
                if (_webglRenderingContext == null || _webglRenderingContext.IsDisposed)
                {
                    int uid = InvokeRet<int>("nkCanvas.CreateWebGLContext");
                    _webglRenderingContext = new WebGL.WebGLRenderingContext(this, uid);
                }

                return (TContext)(WebGL.IWebGLRenderingContext)_webglRenderingContext;
            }

            throw new NotSupportedException();
        }

    }
}
