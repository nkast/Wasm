using System;
using System.Collections.Generic;
using Microsoft.JSInterop;

namespace nkast.Wasm.Dom
{
    public abstract class Element<TElement> : CachedJSObject<TElement>
        where TElement : JSObject
    {
        public int ClientWidth
        {
            get { return InvokeRetInt("nkElement.GetClientWidth"); }
        }

        public int ClientHeight
        {
            get { return InvokeRetInt("nkElement.GetClientHeight"); }
        }

        protected Element(int uid) : base(uid)
        {
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
