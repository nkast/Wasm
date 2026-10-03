using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.Dom;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.WebStorage
{
    public class Storage : JSObject
    {
        private readonly int _fid_GetLength;
        private readonly int _fid_SetItem;
        private readonly int _fid_GetItem;
        private readonly int _fid_RemoveItem;
        private readonly int _fid_Clear;

        public Storage(int uid) : base(uid)
        {
            _fid_GetLength = RegisterFunction("nkStorage.GetLength");
            _fid_SetItem = RegisterFunction("nkStorage.SetItem");
            _fid_GetItem = RegisterFunction("nkStorage.GetItem");
            _fid_RemoveItem = RegisterFunction("nkStorage.RemoveItem");
            _fid_Clear = RegisterFunction("nkStorage.Clear");

        }

        public int Length
        {
            get { return InvokeRetInt(_fid_GetLength); }
        }

        public void SetItem(string key, string value)
        {
            Invoke<string, string>(_fid_SetItem, key, value);
        }

        public string GetItem(string key)
        {
            return InvokeRetString<string>(_fid_GetItem, key);
        }

        public void RemoveItem(string key)
        {
            Invoke<string>(_fid_RemoveItem, key);
        }

        public void Clear()
        {
            Invoke(_fid_Clear);
        }
    }
}
