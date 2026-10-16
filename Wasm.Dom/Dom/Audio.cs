using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public class Audio : HTMLMediaElement, IHTMLMediaElement
    {
        private static readonly int _fid_Create = RegisterFunction("nkAudio.Create");

        private Audio(int uid) : base(uid)
        {
        }

        public Audio() : base(Register())
        {
        }

        private static int Register()
        {
            int uid = JSObject.StaticInvokeRetInt(_fid_Create);
            return uid;
        }
    }
}
