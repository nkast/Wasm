using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public class CSSStyleDeclaration : CachedJSObject<CSSStyleDeclaration>
    {
        public string this[string propertyName]
        {
            get { return GetPropertyValue(propertyName); }
            set { SetProperty(propertyName, value); }
        }

        internal CSSStyleDeclaration(int uid) : base(uid)
        {
        }

        public string GetPropertyValue(string propertyName)
        {
            return InvokeRetString("nkStyleDeclaration.GetPropertyValue", propertyName);
        }

        public void SetProperty(string propertyName, string value)
        {
            Invoke("nkStyleDeclaration.SetProperty", propertyName, value);
        }
    }
}