﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Canvas.WebGL;
using nkast.Wasm.Dom;

namespace nkast.Wasm.XR
{
    public class XRSession : CachedJSObject<XRSession>
    {

        public event EventHandler<EventArgs> Ended;
        public event EventHandler<InputSourcesChangedEventArgs> InputSourcesChanged;

        public delegate void XRAnimationFrameCallback(TimeSpan time, XRFrame frame);

        int _animationFrameCallbackId;
        Dictionary<int, XRAnimationFrameCallback> _animationFrameCallbacks = new Dictionary<int, XRAnimationFrameCallback>();
        Dictionary<int, int> _animationFrameRequestHandles = new Dictionary<int, int>();

        public XRRenderState RenderState
        {
            get
            {
                int uid = InvokeRet<int>("nkXRSession.GetRenderState");
                XRRenderState renderState = XRRenderState.FromUid(uid);
                if (renderState != null)
                    return renderState;

                return new XRRenderState(uid);
            }
        }

        public XRInputSourceArray InputSources
        {
            get
            {
                int uid = InvokeRet<int>("nkXRSession.GetInputSources");

                XRInputSourceArray inputSourceArray = XRInputSourceArray.FromUid(uid);
                if (inputSourceArray != null)
                    return inputSourceArray;

                return new XRInputSourceArray(uid);
            }
        }

        internal XRSession(int uid) : base(uid)
        {
            Invoke("nkXRSession.RegisterEvents");
        }

        [JSInvokable]
        public static void JsXRSessionOnEnd(int uid)
        {
            XRSession xrSession = XRSession.FromUid(uid);
            if (xrSession == null)
                return;

            var handler = xrSession.Ended;
            if (handler != null)
                handler(xrSession, EventArgs.Empty);
        }

        [JSInvokable]
        public static void JsXRSessionOnInputSourcesChanged(int uid)
        {
            XRSession xrSession = XRSession.FromUid(uid);
            if (xrSession == null)
                return;

            var handler = xrSession.InputSourcesChanged;
            if (handler != null)
                handler(xrSession, InputSourcesChangedEventArgs.Empty);
        }

        [JSInvokable]
        public static void JsXRSessionOnAnimationFrame(int uid, int callbackId, double time, int xrFrameUid)
        {
            XRSession xrSession = XRSession.FromUid(uid);
            if (xrSession == null)
                return;

            xrSession.OnAnimationFrame(callbackId, TimeSpan.FromMilliseconds(time), xrFrameUid);
        }

        private void OnAnimationFrame(int callbackId, TimeSpan time, int xrFrameUid)
        {
            XRAnimationFrameCallback animationFrameCallback = _animationFrameCallbacks[callbackId];
            _animationFrameCallbacks.Remove(callbackId);
            _animationFrameRequestHandles.Remove(callbackId);

            XRFrame xrFrame = XRFrame.FromUid(xrFrameUid);
            if (xrFrame == null)
                xrFrame = new XRFrame(xrFrameUid);

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
            Invoke("nkXRSession.UpdateRenderState", attributes.BaseLayer.Uid);
        }

        public int RequestAnimationFrame(XRAnimationFrameCallback animationFrameCallback)
        {
            unchecked { _animationFrameCallbackId++; }
            int callbackId = _animationFrameCallbackId;

            int handle = InvokeRet<int, int>("nkXRSession.RequestAnimationFrame", callbackId);

            _animationFrameCallbacks.Add(callbackId, animationFrameCallback);
            _animationFrameRequestHandles.Add(callbackId, handle);

            return callbackId;
        }

        public void CancelAnimationFrame(int requestID)
        {
            int callbackId = requestID;
            requestID = _animationFrameRequestHandles[callbackId];

            _animationFrameCallbacks.Remove(callbackId);
            _animationFrameRequestHandles.Remove(callbackId);

            Invoke<int>("nkXRSession.CancelAnimationFrame", requestID);

            return;
        }

        internal int CreateWebGLLayer(IWebGLRenderingContext glContext)
        {
            int uid = InvokeRet<int, int>("nkXRSession.CreateWebGLLayer", ((JSObject)glContext).Uid);

            return uid;
        }

        internal int CreateWebGLLayer(IWebGLRenderingContext glContext, XRWebGLLayerOptions options)
        {
            int uid = InvokeRet<int, int, int>("nkXRSession.CreateWebGLLayer1", ((JSObject)glContext).Uid, options.ToBit());

            return uid;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Invoke("nkXRSession.UnregisterEvents");

            base.Dispose(disposing);
        }
    }

}
