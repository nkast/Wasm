using System;
using Microsoft.JSInterop.WebAssembly;

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
            WebAssemblyJSRuntime runtime = new WasmJSRuntime();
            int uid = runtime.InvokeUnmarshalled<int>("nkAudio.Create");
            return uid;
        }

        internal sealed class WasmJSRuntime : WebAssemblyJSRuntime
        {
        }
    }
}
