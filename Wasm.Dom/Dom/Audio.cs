using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public class Audio : HTMLMediaElement, IHTMLMediaElement
    {
        private Audio(int uid) : base(uid)
        {
        }

        public Audio() : base(Register())
        {
        }

        private static int Register()
        {
            int uid = JSObject.StaticInvokeRetInt("nkAudio.Create");
            return uid;
        }
    }
}
