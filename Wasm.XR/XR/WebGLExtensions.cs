using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using nkast.Wasm.Canvas.WebGL;
using nkast.Wasm.JSInterop;
using InteropServicesJS = System.Runtime.InteropServices.JavaScript;


namespace nkast.Wasm.Canvas.WebGL
{
    public static partial class WebGLExtensions
    {
        [InteropServicesJS.JSImport("globalThis.window.nkXRSystem.MakeXRCompatible")]
        private static partial int InvokeRetInt(int uid);

        public static Task MakeXRCompatibleAsync(this IWebGLRenderingContext glContext)
        {
            int uid = InvokeRetInt(((JSObject)glContext).Uid);

            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }
    }
}

