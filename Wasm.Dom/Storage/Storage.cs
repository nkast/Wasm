using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.WebStorage
{
    public class Storage : JSObject
    {
        public Storage(int uid) : base(uid)
        {

        }

        public int Length
        {
            get { return InvokeRetInt("nkStorage.GetLength"); }
        }

        public void SetItem(string key, string value)
        {
            Invoke<string, string>("nkStorage.SetItem", key, value);
        }

        public string GetItem(string key)
        {
            return InvokeRetString<string>("nkStorage.GetItem", key);
        }

        public void RemoveItem(string key)
        {
            Invoke<string>("nkStorage.RemoveItem", key);
        }

        public void Clear()
        {
            Invoke("nkStorage.Clear");
        }
    }
}
