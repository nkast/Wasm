using System;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XHR
{
    public class XMLHttpRequest : JSObject
    { 
        public XMLHttpRequest() : base(Register())
        {
        
        }

        private static int Register()
        {
            WebAssemblyJSRuntime runtime = new WasmJSRuntime();
            int uid = runtime.InvokeUnmarshalled<int>("nkXHR.Create");
            return uid;
        }

        public void Open(string method, string url, bool async = true)
        {
            Invoke("nkXHR.Open", method, url, async?1:0);
        }

        public void OverrideMimeType(string mimeType)
        {
            Invoke("nkXHR.OverrideMimeType", mimeType);
        }

        public void Send()
        {
            Invoke("nkXHR.Send");
        }

        public int Status
        {
            get
            {
                int status = InvokeRet<int>("nkXHR.GetStatus");
                return status;
            }
        }

        public string ResponseText
        {
            get
            {
                string responseText = InvokeRet<string>("nkXHR.GetResponseText");
                return responseText;
            }
        }

        internal sealed class WasmJSRuntime : WebAssemblyJSRuntime
        {
        }
    }
}
