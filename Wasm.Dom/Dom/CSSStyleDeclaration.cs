using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public class CSSStyleDeclaration : CachedJSObject<CSSStyleDeclaration>
    {
        private readonly int _fid_GetPropertyValue;
        private readonly int _fid_SetProperty;

        public string this[string propertyName]
        {
            get { return GetPropertyValue(propertyName); }
            set { SetProperty(propertyName, value); }
        }

        internal CSSStyleDeclaration(int uid) : base(uid)
        {
            _fid_GetPropertyValue = RegisterFunction("nkStyleDeclaration.GetPropertyValue");
            _fid_SetProperty = RegisterFunction("nkStyleDeclaration.SetProperty");
        }

        public string GetPropertyValue(string propertyName)
        {
            return InvokeRetString(_fid_GetPropertyValue, propertyName);
        }

        public void SetProperty(string propertyName, string value)
        {
            Invoke(_fid_SetProperty, propertyName, value);
        }
    }
}