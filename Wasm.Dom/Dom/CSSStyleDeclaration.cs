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
            return InvokeRetString(RegisterFunction("nkStyleDeclaration.GetPropertyValue"), propertyName);
        }

        public void SetProperty(string propertyName, string value)
        {
            Invoke(RegisterFunction("nkStyleDeclaration.SetProperty"), propertyName, value);
        }
    }
}