using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public abstract class HTMLElement<THTMLElement> : Element<THTMLElement>
        where THTMLElement : JSObject
    {

        public CSSStyleDeclaration Style
        {
            get
            {
                int uid = InvokeRetInt("nkHTMLElement.GetStyle");

                CSSStyleDeclaration style = CSSStyleDeclaration.FromUid(uid);
                if (style != null)
                    return style;

                return new CSSStyleDeclaration(uid);
            } 
        }

        protected HTMLElement(int uid) : base(uid)
        {
        }

        public void Focus()
        {
            Invoke("nkHTMLElement.Focus");
        }

        public void Blur()
        {
            Invoke("nkHTMLElement.Blur");
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
