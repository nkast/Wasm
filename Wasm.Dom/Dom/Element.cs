using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public abstract class Element<TElement> : CachedJSObject<TElement>
        where TElement : JSObject
    {
        public int ClientLeft
        {
            get { return InvokeRetInt(RegisterFunction("nkElement.GetClientLeft")); }
        }

        public int ClientTop
        {
            get { return InvokeRetInt(RegisterFunction("nkElement.GetClientTop")); }
        }

        public int ClientWidth
        {
            get { return InvokeRetInt(RegisterFunction("nkElement.GetClientWidth")); }
        }

        public int ClientHeight
        {
            get { return InvokeRetInt(RegisterFunction("nkElement.GetClientHeight")); }
        }

        protected Element(int uid) : base(uid)
        {
        }

        public unsafe DOMRect GetBoundingClientRect()
        {
            DOMRect result = default;
            Invoke<IntPtr>(RegisterFunction("nkElement.GetBoundingClientRect"), new IntPtr(&result));
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
