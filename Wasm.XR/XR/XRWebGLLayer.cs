using System;
using System.Collections.Generic;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Canvas.WebGL;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRWebGLLayer : XRLayer
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        private XRSession _xrSession;
        private IWebGLRenderingContext _glContext;

        private WebGLFramebuffer _framebuffer;

        public int FramebufferWidth
        {
            get { return InvokeRet<int>("nkXRWebGLLayer.GetFramebufferWidth"); }
        }

        public int FramebufferHeight
        {
            get { return InvokeRet<int>("nkXRWebGLLayer.GetFramebufferHeight"); }
        }

        public bool IgnoreDepthValues
        {
            get { return InvokeRet<bool>("nkXRWebGLLayer.GetIgnoreDepthValues"); }
        }

        public bool Antialias
        {
            get { return InvokeRet<bool>("nkXRWebGLLayer.GetAntialias"); }
        }

        public WebGLFramebuffer Framebuffer
        {
            get
            {
                if (_framebuffer != null)
                    return _framebuffer;

                int uid = InvokeRet<int>("nkXRWebGLLayer.GetFramebuffer");
                if (uid == -1)
                    return null;

                WebGLFramebuffer framebuffer = null;
                framebuffer = new XRWebGLFramebuffer(uid, _glContext);

                _framebuffer = framebuffer;
                return framebuffer;
            }
        }

        public XRWebGLLayer(XRSession xrSession, IWebGLRenderingContext glContext)
            : base(Register(xrSession, glContext))
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this, true));

            this._xrSession = xrSession;
            this._glContext = glContext;
        }

        private static int Register(XRSession xrSession, IWebGLRenderingContext glContext)
        {
            WebAssemblyJSRuntime runtime = new WasmJSRuntime();
            int uid = runtime.InvokeUnmarshalled<ValueTuple<int, int>, int>("nkXRWebGLLayer.Create",
                ValueTuple.Create<int, int>(xrSession.Uid, ((JSObject)glContext).Uid));
            return uid;
        }

        internal static XRWebGLLayer FromUid(int uid)
        {
            if (XRWebGLLayer._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (XRWebGLLayer)jsObj;

            return null;
        }

        public unsafe XRViewport GetViewport(XRView view)
        {
            XRViewport result = default;
            Invoke<int, IntPtr>("nkXRWebGLLayer.GetViewport", view.Uid, new IntPtr(&result));
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _uidMap.Remove(Uid);

            base.Dispose(disposing);
        }

        internal sealed class WasmJSRuntime : WebAssemblyJSRuntime
        {
        }
    }
}