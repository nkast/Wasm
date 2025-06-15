using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public class Video : HTMLMediaElement, IHTMLMediaElement
    {
        private Video(int uid) : base(uid)
        {
        }

        public Video() : base(Register())
        {
        }

        private static int Register()
        {
            int uid = JSObject.StaticInvokeRetInt("nkVideo.Create");
            return uid;
        }
    }
}
