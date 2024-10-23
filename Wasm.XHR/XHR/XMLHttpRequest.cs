using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XHR
{
    public class XMLHttpRequest : JSObject
    { 
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        public event EventHandler Load;
        public event EventHandler Error;

        public XMLHttpRequest() : base(Register())
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this));
            Invoke("nkXHR.RegisterEvents");
        }

        private static int Register()
        {
            WebAssemblyJSRuntime runtime = new WasmJSRuntime();
            int uid = runtime.InvokeUnmarshalled<int>("nkXHR.Create");
            return uid;
        }

        public static XMLHttpRequest FromUid(int uid)
        {
            if (XMLHttpRequest._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
            {
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (XMLHttpRequest)jsObj;
                else
                    XMLHttpRequest._uidMap.Remove(uid);
            }

            return null;
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
            _uidMap.Remove(Uid);

            base.Dispose(disposing);
        }

        internal sealed class WasmJSRuntime : WebAssemblyJSRuntime
        {
        }
    }
}
