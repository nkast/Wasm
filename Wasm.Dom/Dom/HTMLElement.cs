using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public abstract class HTMLElement<THTMLElement> : Element<THTMLElement>
        where THTMLElement : JSObject
    {
        private readonly int _fid_GetStyle;
        private readonly int _fid_Focus;
        private readonly int _fid_Blur;


        public CSSStyleDeclaration Style
        {
            get
            {
                int uid = InvokeRetInt(_fid_GetStyle);

                CSSStyleDeclaration style = CSSStyleDeclaration.FromUid(uid);
                if (style != null)
                    return style;

                return new CSSStyleDeclaration(uid);
            } 
        }

        protected HTMLElement(int uid) : base(uid)
        {
            _fid_GetStyle = RegisterFunction("nkHTMLElement.GetStyle");
            _fid_Focus = RegisterFunction("nkHTMLElement.Focus");
            _fid_Blur = RegisterFunction("nkHTMLElement.Blur");
        }

        public void Focus()
        {
            Invoke(_fid_Focus);
        }

        public void Blur()
        {
            Invoke(_fid_Blur);
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
