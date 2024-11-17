using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XHR
{
    public class XMLHttpRequest : CachedJSObject<XMLHttpRequest>
    { 

        public event EventHandler Load;
        public event EventHandler Error;

        public XMLHttpRequest() : base(Register())
        {
            Invoke("nkXHR.RegisterEvents");
        }

        private static int Register()
        {
            WebAssemblyJSRuntime runtime = new WasmJSRuntime();
            int uid = runtime.InvokeUnmarshalled<int>("nkXHR.Create");
            return uid;
        }

        [JSInvokable]
        public static void JsXMLHttpRequestOnLoad(int uid)
        {
            XMLHttpRequest xmlHttpRequest = XMLHttpRequest.FromUid(uid);
            if (xmlHttpRequest == null)
                return;

            var handler = xmlHttpRequest.Load;
            if (handler != null)
                handler(xmlHttpRequest, EventArgs.Empty);
        }

        [JSInvokable]
        public static void JsXMLHttpRequestOnError(int uid)
        {
            XMLHttpRequest xmlHttpRequest = XMLHttpRequest.FromUid(uid);
            if (xmlHttpRequest == null)
                return;

            var handler = xmlHttpRequest.Error;
            if (handler != null)
                handler(xmlHttpRequest, EventArgs.Empty);
        }

        public void Open(string method, string url, bool async = true)
        {
            Invoke("nkXHR.Open", method, url, async?1:0);
        }

        public void OverrideMimeType(string mimeType)
        {
            Invoke("nkXHR.OverrideMimeType", mimeType);
        }

        public void SetRequestHeader(string header, string value)
        {
            Invoke("nkXHR.SetRequestHeader", header, value);
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

        public ReadyState ReadyState
        {
            get
            {
                int readyState = InvokeRet<int>("nkXHR.GetReadyState");
                return (ReadyState) readyState;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Invoke("nkXHR.UnregisterEvents");

            base.Dispose(disposing);
        }

        public void DecompressBrotliStream(byte[] compressedBuffer, uint compressedDataSize, byte[] decompressedBuffer, uint decompressedDataSize)
        {
            Invoke("nkXHR.DecompressBrotliStream", compressedDataSize, decompressedDataSize, compressedBuffer, decompressedBuffer);
        }

        internal sealed class WasmJSRuntime : WebAssemblyJSRuntime
        {
        }
    }
}
