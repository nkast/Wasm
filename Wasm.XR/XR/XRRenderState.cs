using System;
using System.Collections.Generic;
using System.Numerics;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRRenderState : CachedJSObject<XRRenderState>
    {
        internal XRRenderState(int uid) : base(uid)
        {
        }

        public unsafe float? DepthNear
        {
            get
            {
                XRViewport result = default;
                Invoke<IntPtr>("nkXRRenderState.GetDepthNear", new IntPtr(&result));
                if (result.X == -1)
                    return null;

                return result.X;
            }
        }

        public unsafe float? DepthFar
        {
            get
            {
                XRViewport result = default;
                Invoke<IntPtr>("nkXRRenderState.GetDepthFar", new IntPtr(&result));
                if (result.X == -1)
                    return null;

                return result.X;
            }
        }
        public unsafe float? InlineVerticalFieldOfView
        {
            get
            {
                XRViewport result = default;
                Invoke<IntPtr>("nkXRRenderState.GetInlineVerticalFieldOfView", new IntPtr(&result));
                if (result.X == -1)
                    return null;

                return result.X;
            }
        }

        public XRWebGLLayer BaseLayer
        {
            get
            {
                int uid = InvokeRet<int>("nkXRRenderState.GetBaseLayer");
                XRWebGLLayer glLayer = XRWebGLLayer.FromUid<XRWebGLLayer>(uid);
                if (glLayer != null)
                    return glLayer;

                throw new NotImplementedException();
                //return new XRWebGLLayer(uid);
            }
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