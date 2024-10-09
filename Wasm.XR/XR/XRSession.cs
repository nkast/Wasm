using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRSession : JSObject
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        Action<float, XRFrame> _lastAnimationFrameCallback;

        public XRSession(int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this));
        }


        [JSInvokable]
        public static void JsXRSessionOnAnimationFrame(int uid, float time, int xrFrameUid)
        {
            if (!_uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                return;
            if (!_uidMap[uid].TryGetTarget(out JSObject jsObj))
                return;

            XRSession xrSession = (XRSession)jsObj;
            xrSession.OnAnimationFrame(time, xrFrameUid);

        }

        private void OnAnimationFrame(float time, int xrFrameUid)
        {
            XRFrame xrFrame = new XRFrame(xrFrameUid, this);

            Action<float, XRFrame> animationFrameCallback = _lastAnimationFrameCallback;
            _lastAnimationFrameCallback = null;
            animationFrameCallback(time, xrFrame);
        }

        public Task<XRReferenceSpace> RequestReferenceSpace(string referenceSpaceType)
        {
            int uid = InvokeRet<string, int>("nkXRSession.RequestReferenceSpace", referenceSpaceType);

            PromiseJSObject<XRReferenceSpace> promise = new PromiseJSObject<XRReferenceSpace>(uid,
                (int newuid) =>
                {
                    return new XRReferenceSpace(newuid);
                });
            return promise.GetTask();
        }

        public void UpdateRenderState()
        {
            Invoke("nkXRSession.UpdateRenderState");
        }

        public int RequestAnimationFrame(Action<float, XRFrame> animationFrameCallback)
        {
            //if (_lastAnimationFrameCallback != null)
             //   return;
                    //throw new Exception("RequestAnimationFrame allready called.");

            _lastAnimationFrameCallback = animationFrameCallback;

            int handle = InvokeRet<int>("nkXRSession.RequestAnimationFrame");
            return handle;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _uidMap.Remove(Uid);

            base.Dispose(disposing);
        }
    }

}
