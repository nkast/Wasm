using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public class Video : HTMLMediaElement, IHTMLMediaElement
    {
        private static readonly int _fid_Create = RegisterFunction("nkVideo.Create");

        private Video(int uid) : base(uid)
        {
        }

        public Video() : base(Register())
        {
        }

        private static int Register()
        {
            int uid = JSObject.StaticInvokeRetInt(_fid_Create);
            return uid;
        }
    }
}
