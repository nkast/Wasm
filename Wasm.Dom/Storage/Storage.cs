using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.Dom;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.WebStorage
{
    public class Storage : JSObject
    {
        public Storage(int uid) : base(uid)
        {

        }

        public int Length
        {
            get { return InvokeRetInt(RegisterFunction("nkStorage.GetLength")); }
        }

        public void SetItem(string key, string value)
        {
            Invoke<string, string>(RegisterFunction("nkStorage.SetItem"), key, value);
        }

        public string GetItem(string key)
        {
            return InvokeRetString<string>(RegisterFunction("nkStorage.GetItem"), key);
        }

        public void RemoveItem(string key)
        {
            Invoke<string>(RegisterFunction("nkStorage.RemoveItem"), key);
        }

        public void Clear()
        {
            Invoke(RegisterFunction("nkStorage.Clear"));
        }
    }
}
