using System;
using System.Collections.Generic;
using nkast.Wasm.Canvas.WebGL;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRWebGLLayer : XRLayer
    {
        private readonly int _fid_GetFramebufferWidth;
        private readonly int _fid_GetFramebufferHeight;
        private readonly int _fid_GetIgnoreDepthValues;
        private readonly int _fid_GetAntialias;
        private readonly int _fid_GetFramebuffer;
        private readonly int _fid_GetViewport;


        private XRSession _xrSession;
        private IWebGLRenderingContext _glContext;

        public int FramebufferWidth
        {
            get { return InvokeRetInt(_fid_GetFramebufferWidth); }
        }

        public int FramebufferHeight
        {
            get { return InvokeRetInt(_fid_GetFramebufferHeight); }
        }

        public bool IgnoreDepthValues
        {
            get { return InvokeRetBool(_fid_GetIgnoreDepthValues); }
        }

        public bool Antialias
        {
            get { return InvokeRetBool(_fid_GetAntialias); }
        }

        public WebGLFramebuffer Framebuffer
        {
            get
            {
                int uid = InvokeRetInt(_fid_GetFramebuffer);
                XRWebGLFramebuffer framebuffer = XRWebGLFramebuffer.FromUid<XRWebGLFramebuffer>(uid);
                if (framebuffer != null)
                    return framebuffer;

                if (uid == -1)
                    return null;

                return new XRWebGLFramebuffer(uid);
            }
        }

        public XRWebGLLayer(XRSession xrSession, IWebGLRenderingContext glContext)
            : base(Register(xrSession, glContext))
        {
            _fid_GetFramebufferWidth = RegisterFunction("nkXRWebGLLayer.GetFramebufferWidth");
            _fid_GetFramebufferHeight = RegisterFunction("nkXRWebGLLayer.GetFramebufferHeight");
            _fid_GetIgnoreDepthValues = RegisterFunction("nkXRWebGLLayer.GetIgnoreDepthValues");
            _fid_GetAntialias = RegisterFunction("nkXRWebGLLayer.GetAntialias");
            _fid_GetFramebuffer = RegisterFunction("nkXRWebGLLayer.GetFramebuffer");
            _fid_GetViewport = RegisterFunction("nkXRWebGLLayer.GetViewport");
            this._xrSession = xrSession;
            this._glContext = glContext;
        }

        public XRWebGLLayer(XRSession xrSession, IWebGLRenderingContext glContext, XRWebGLLayerOptions options) 
            : base(Register(xrSession, glContext, options))
        {
            _fid_GetFramebufferWidth = RegisterFunction("nkXRWebGLLayer.GetFramebufferWidth");
            _fid_GetFramebufferHeight = RegisterFunction("nkXRWebGLLayer.GetFramebufferHeight");
            _fid_GetIgnoreDepthValues = RegisterFunction("nkXRWebGLLayer.GetIgnoreDepthValues");
            _fid_GetAntialias = RegisterFunction("nkXRWebGLLayer.GetAntialias");
            _fid_GetFramebuffer = RegisterFunction("nkXRWebGLLayer.GetFramebuffer");
            _fid_GetViewport = RegisterFunction("nkXRWebGLLayer.GetViewport");
        }

        private static int Register(XRSession xrSession, IWebGLRenderingContext glContext)
        {
            int uid = xrSession.CreateWebGLLayer(glContext);
            return uid;
        }

        private static int Register(XRSession xrSession, IWebGLRenderingContext glContext, XRWebGLLayerOptions options)
        {
            int uid = xrSession.CreateWebGLLayer(glContext, options);
            return uid;
        }

        public unsafe XRViewport GetViewport(XRView view)
        {
            XRViewport result = default;
            Invoke<int, IntPtr>(_fid_GetViewport, view.Uid, new IntPtr(&result));
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            base.Dispose(disposing);
        }
    }
}