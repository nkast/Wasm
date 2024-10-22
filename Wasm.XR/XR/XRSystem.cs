using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRSystem : JSObject
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        public static XRSystem FromNavigator(Navigator navigator)
        {
            WebAssemblyJSRuntime runtime = new WasmJSRuntime();
            int uid = runtime.InvokeUnmarshalled<int, int>("nkXRSystem.Create", navigator.Uid);
            if (uid == -1)
                return null;

            XRSystem xrsystem = XRSystem.FromUid(uid);
            if (xrsystem != null)
                return xrsystem;

            return new XRSystem(navigator, uid);

        }

        internal XRSystem(Navigator navigator, int uid) : base(uid)
        {
            //_navigator = navigator;
        }

        public static XRSystem FromUid(int uid)
        {
            if (XRSystem._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
            {
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (XRSystem)jsObj;
                else
                    XRSystem._uidMap.Remove(uid);
            }

            return null;
        }

        public Task<bool> IsSessionSupportedAsync(string mode)
        {
            int uid = InvokeRet<string, int>("nkXRSystem.IsSessionSupported", mode);

            PromiseBoolean promise = new PromiseBoolean(uid);
            return promise.GetTask();
        }

        public Task<XRSession> RequestSessionAsync(string mode)
        {
            int uid = InvokeRet<string, int>("nkXRSystem.RequestSession", mode);

            PromiseJSObject<XRSession> promise = new PromiseJSObject<XRSession>(uid, (int newuid) => new XRSession(newuid) );
            return promise.GetTask();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            _uidMap.Remove(Uid);

            base.Dispose(disposing);
        }

        internal sealed class WasmJSRuntime : WebAssemblyJSRuntime
        {
        }
    }

}
