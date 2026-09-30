using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XHR
{
    public class XMLHttpRequest : CachedJSObject<XMLHttpRequest>
    { 

        public event EventHandler Load;
        public event EventHandler Error;

        public XMLHttpRequest() : base(Register())
        {
            Invoke(RegisterFunction("nkXHR.RegisterEvents"));
        }

        private static int Register()
        {
            int uid = JSObject.StaticInvokeRetInt(JSObject.RegisterFunction("nkXHR.Create"));
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
            Invoke(RegisterFunction("nkXHR.Open"), method, url, async?1:0);
        }

        public void OverrideMimeType(string mimeType)
        {
            Invoke(RegisterFunction("nkXHR.OverrideMimeType"), mimeType);
        }

        public void SetRequestHeader(string header, string value)
        {
            Invoke(RegisterFunction("nkXHR.SetRequestHeader"), header, value);
        }

        public void Send()
        {
            Invoke(RegisterFunction("nkXHR.Send"));
        }

        public int Status
        {
            get
            {
                int status = InvokeRetInt(RegisterFunction("nkXHR.GetStatus"));
                return status;
            }
        }

        public string ResponseText
        {
            get
            {
                string responseText = InvokeRetString(RegisterFunction("nkXHR.GetResponseText"));
                return responseText;
            }
        }

        public ReadyState ReadyState
        {
            get
            {
                int readyState = InvokeRetInt(RegisterFunction("nkXHR.GetReadyState"));
                return (ReadyState) readyState;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Invoke(RegisterFunction("nkXHR.UnregisterEvents"));

            base.Dispose(disposing);
        }

        public unsafe void DecompressBrotliStream(byte[] compressedBuffer, uint compressedDataSize, byte[] decompressedBuffer, uint decompressedDataSize)
        {
            fixed (byte* pCompressedBuffer = compressedBuffer)
            fixed (byte* pDecompressedBuffer = decompressedBuffer)
            {
                    Invoke(RegisterFunction("nkXHR.DecompressBrotliStream"), compressedDataSize, decompressedDataSize, (int)pCompressedBuffer, (int)pDecompressedBuffer);
            }
        }

        public unsafe void DecompressBrotliStream(Span<byte> compressedBuffer, uint compressedDataSize, Span<byte> decompressedBuffer, uint decompressedDataSize)
        {
            fixed (byte* pCompressedBuffer = compressedBuffer)
            fixed (byte* pDecompressedBuffer = decompressedBuffer)
            {
                    Invoke(RegisterFunction("nkXHR.DecompressBrotliStream"), compressedDataSize, decompressedDataSize, (int)pCompressedBuffer, (int)pDecompressedBuffer);
            }
        }
    }
}
