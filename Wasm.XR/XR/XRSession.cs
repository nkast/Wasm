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

        public event EventHandler<EventArgs> Ended;
        public event EventHandler<InputSourcesChangedEventArgs> InputSourcesChanged;

        Action<float, XRFrame> _lastAnimationFrameCallback;

        public XRRenderState RenderState
        {
            get
            {
                int uid = InvokeRet<int>("nkXRSession.GetRenderState");
                if (XRRenderState._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                {
                    if (jsObjRef.TryGetTarget(out JSObject jsObj))
                        return (XRRenderState)jsObj;
                    else
                        XRRenderState._uidMap.Remove(uid);
                }

                return new XRRenderState(uid);
            }
        }

        public XRInputSourceArray InputSources
        {
            get
            {
                int uid = InvokeRet<int>("nkXRSession.GetInputSources");

                return new XRInputSourceArray(uid);
            }
        }

        public XRSession(int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this));
            Invoke("nkXRSession.RegisterEvents");
        }

        [JSInvokable]
        public static void JsXRSessionOnEnd(int uid)
        {
            if (!_uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                return;
            if (!_uidMap[uid].TryGetTarget(out JSObject jsObj))
                return;

            XRSession xrSession = (XRSession)jsObj;

            var handler = xrSession.Ended;
            if (handler != null)
                handler(xrSession, EventArgs.Empty);
        }

        [JSInvokable]
        public static void JsXRSessionOnInputSourcesChanged(int uid)
        {
            if (!_uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                return;
            if (!_uidMap[uid].TryGetTarget(out JSObject jsObj))
                return;

            XRSession xrSession = (XRSession)jsObj;

            var handler = xrSession.InputSourcesChanged;
            if (handler != null)
                handler(xrSession, InputSourcesChangedEventArgs.Empty);
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

        public Task End()
        {
            int uid = InvokeRet<int>("nkXRSession.End");

            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        public void UpdateRenderState(RenderStateAttributes attributes)
        {
            if (attributes == null)
                throw new ArgumentNullException(nameof(attributes));

            Invoke("nkXRSession.UpdateRenderState", attributes.BaseLayer.Uid);
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

            Invoke("nkXRSession.UnregisterEvents");
            _uidMap.Remove(Uid);

            base.Dispose(disposing);
        }
    }

}
