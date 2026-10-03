using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.XHR
{
    public class XMLHttpRequest : CachedJSObject<XMLHttpRequest>
    { 
        private static readonly int _fid_Create = RegisterFunction("nkXHR.Create");
        private readonly int _fid_RegisterEvents;
        private readonly int _fid_Open;
        private readonly int _fid_OverrideMimeType;
        private readonly int _fid_SetRequestHeader;
        private readonly int _fid_Send;
        private readonly int _fid_GetStatus;
        private readonly int _fid_GetResponseText;
        private readonly int _fid_GetReadyState;
        private readonly int _fid_UnregisterEvents;
        private readonly int _fid_DecompressBrotliStream;


        public event EventHandler Load;
        public event EventHandler Error;

        public XMLHttpRequest() : base(Register())
        {
            _fid_RegisterEvents = RegisterFunction("nkXHR.RegisterEvents");
            _fid_Open = RegisterFunction("nkXHR.Open");
            _fid_OverrideMimeType = RegisterFunction("nkXHR.OverrideMimeType");
            _fid_SetRequestHeader = RegisterFunction("nkXHR.SetRequestHeader");
            _fid_Send = RegisterFunction("nkXHR.Send");
            _fid_GetStatus = RegisterFunction("nkXHR.GetStatus");
            _fid_GetResponseText = RegisterFunction("nkXHR.GetResponseText");
            _fid_GetReadyState = RegisterFunction("nkXHR.GetReadyState");
            _fid_UnregisterEvents = RegisterFunction("nkXHR.UnregisterEvents");
            _fid_DecompressBrotliStream = RegisterFunction("nkXHR.DecompressBrotliStream");
            Invoke(_fid_RegisterEvents);
        }

        private static int Register()
        {
            int uid = JSObject.StaticInvokeRetInt(_fid_Create);
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
            Invoke(_fid_Open, method, url, async?1:0);
        }

        public void OverrideMimeType(string mimeType)
        {
            Invoke(_fid_OverrideMimeType, mimeType);
        }

        public void SetRequestHeader(string header, string value)
        {
            Invoke(_fid_SetRequestHeader, header, value);
        }

        public void Send()
        {
            Invoke(_fid_Send);
        }

        public int Status
        {
            get
            {
                int status = InvokeRetInt(_fid_GetStatus);
                return status;
            }
        }

        public string ResponseText
        {
            get
            {
                string responseText = InvokeRetString(_fid_GetResponseText);
                return responseText;
            }
        }

        public ReadyState ReadyState
        {
            get
            {
                int readyState = InvokeRetInt(_fid_GetReadyState);
                return (ReadyState) readyState;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Invoke(_fid_UnregisterEvents);

            base.Dispose(disposing);
        }

        public unsafe void DecompressBrotliStream(byte[] compressedBuffer, uint compressedDataSize, byte[] decompressedBuffer, uint decompressedDataSize)
        {
            fixed (byte* pCompressedBuffer = compressedBuffer)
            fixed (byte* pDecompressedBuffer = decompressedBuffer)
            {
                    Invoke(_fid_DecompressBrotliStream, compressedDataSize, decompressedDataSize, (int)pCompressedBuffer, (int)pDecompressedBuffer);
            }
        }

        public unsafe void DecompressBrotliStream(Span<byte> compressedBuffer, uint compressedDataSize, Span<byte> decompressedBuffer, uint decompressedDataSize)
        {
            fixed (byte* pCompressedBuffer = compressedBuffer)
            fixed (byte* pDecompressedBuffer = decompressedBuffer)
            {
                    Invoke(_fid_DecompressBrotliStream, compressedDataSize, decompressedDataSize, (int)pCompressedBuffer, (int)pDecompressedBuffer);
            }
        }
    }
}
