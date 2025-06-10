using System;
using System.Collections.Generic;
using Microsoft.JSInterop;

namespace nkast.Wasm.Dom
{
    public abstract class HTMLElement<THTMLElement> : CachedJSObject<THTMLElement>
        where THTMLElement : JSObject
    {

        protected HTMLElement(int uid) : base(uid)
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
