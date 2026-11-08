using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public abstract class Element<TElement> : CachedJSObject<TElement>
        where TElement : JSObject
    {
        private readonly int _fid_GetClientLeft;
        private readonly int _fid_GetClientTop;
        private readonly int _fid_GetClientWidth;
        private readonly int _fid_GetClientHeight;
        private readonly int _fid_GetBoundingClientRect;
        private readonly int _fid_RequestFullscreen;

        public int ClientLeft
        {
            get { return InvokeRetInt(_fid_GetClientLeft); }
        }

        public int ClientTop
        {
            get { return InvokeRetInt(_fid_GetClientTop); }
        }

        public int ClientWidth
        {
            get { return InvokeRetInt(_fid_GetClientWidth); }
        }

        public int ClientHeight
        {
            get { return InvokeRetInt(_fid_GetClientHeight); }
        }

        protected Element(int uid) : base(uid)
        {
            _fid_GetClientLeft = RegisterFunction("nkElement.GetClientLeft");
            _fid_GetClientTop = RegisterFunction("nkElement.GetClientTop");
            _fid_GetClientWidth = RegisterFunction("nkElement.GetClientWidth");
            _fid_GetClientHeight = RegisterFunction("nkElement.GetClientHeight");
            _fid_GetBoundingClientRect = RegisterFunction("nkElement.GetBoundingClientRect");
            _fid_RequestFullscreen = RegisterFunction("nkElement.RequestFullscreen");
        }

        public unsafe DOMRect GetBoundingClientRect()
        {
            DOMRect result = default;
            Invoke<IntPtr>(_fid_GetBoundingClientRect, new IntPtr(&result));
            return result;
        }

        public Task RequestFullscreenAsync()
        {
            int uid = InvokeRetInt(_fid_RequestFullscreen);

            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
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
