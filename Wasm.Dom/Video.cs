using System;
using Microsoft.JSInterop.WebAssembly;

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
            WebAssemblyJSRuntime runtime = new WasmJSRuntime();
            int uid = runtime.InvokeUnmarshalled<int>("nkVideo.Create");
            return uid;
        }

        internal sealed class WasmJSRuntime : WebAssemblyJSRuntime
        {
        }
    }
}
