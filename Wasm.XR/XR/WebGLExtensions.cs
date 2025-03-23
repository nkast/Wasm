using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Canvas.WebGL;
using nkast.Wasm.Dom;


namespace nkast.Wasm.Canvas.WebGL
{
    public static class WebGLExtensions
    {
        public static Task MakeXRCompatibleAsync(this IWebGLRenderingContext glContext)
        {
            WebAssemblyJSRuntime runtime = new WasmJSRuntime();
            int uid = runtime.InvokeUnmarshalled<int, int>("nkXRSystem.makeXRCompatible", ((JSObject)glContext).Uid);

            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        internal sealed class WasmJSRuntime : WebAssemblyJSRuntime
        {
        }
    }
}

