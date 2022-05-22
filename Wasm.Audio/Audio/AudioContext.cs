using System;
using System.Collections.Generic;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Audio
{
    public class AudioContext : BaseAudioContext
    {
        public AudioContext() : base(Register())
        {
        }


        private static int Register()
        {
            WebAssemblyJSRuntime runtime = new WasmJSRuntime();
            int uid = runtime.InvokeUnmarshalled<int>("nkAudioContext.Create");
            return uid;
        }

        public void Close()
        {
            Invoke("nkAudioContext.Close");
        }

        protected override void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (disposing)
            {

            }

            Close();

            base.Dispose(disposing);
        }

        internal sealed class WasmJSRuntime : WebAssemblyJSRuntime
        {
        }

    }
}
