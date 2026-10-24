using System;
using System.Collections.Generic;
using System.Numerics;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XR
{
    public class XRRenderState : CachedJSObject<XRRenderState>
    {
        private readonly int _fid_GetDepthNear;
        private readonly int _fid_GetDepthFar;
        private readonly int _fid_GetInlineVerticalFieldOfView;
        private readonly int _fid_GetBaseLayer;

        internal XRRenderState(int uid) : base(uid)
        {
            _fid_GetDepthNear = RegisterFunction("nkXRRenderState.GetDepthNear");
            _fid_GetDepthFar = RegisterFunction("nkXRRenderState.GetDepthFar");
            _fid_GetInlineVerticalFieldOfView = RegisterFunction("nkXRRenderState.GetInlineVerticalFieldOfView");
            _fid_GetBaseLayer = RegisterFunction("nkXRRenderState.GetBaseLayer");
        }

        public unsafe float? DepthNear
        {
            get
            {
                Vector4 result = default;
                Invoke<IntPtr>(_fid_GetDepthNear, new IntPtr(&result));
                if (result.X == -1)
                    return null;

                return result.X;
            }
        }

        public unsafe float? DepthFar
        {
            get
            {
                Vector4 result = default;
                Invoke<IntPtr>(_fid_GetDepthFar, new IntPtr(&result));
                if (result.X == -1)
                    return null;

                return result.X;
            }
        }
        public unsafe float? InlineVerticalFieldOfView
        {
            get
            {
                Vector4 result = default;
                Invoke<IntPtr>(_fid_GetInlineVerticalFieldOfView, new IntPtr(&result));
                if (result.X == -1)
                    return null;

                return result.X;
            }
        }

        public XRWebGLLayer BaseLayer
        {
            get
            {
                int uid = InvokeRetInt(_fid_GetBaseLayer);
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