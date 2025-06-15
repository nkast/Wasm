using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using nkast.Wasm.Input;
using nkast.Wasm.WebStorage;

namespace nkast.Wasm.Dom
{
    public class Window : JSObject
    {
        static Window _current;
        private Document _document;
        private Navigator _navigator;
        private Storage _sessionStorage;
        private Storage _localStorage;

        public delegate void AnimationFrameCallback(TimeSpan time);
        public delegate void TimeoutCallback();
        public delegate void IntervalCallback();

        int _animationFrameCallbackId;
        Dictionary<int, AnimationFrameCallback> _animationFrameCallbacks = new Dictionary<int, AnimationFrameCallback>();
        Dictionary<int, int> _animationFrameRequestHandles = new Dictionary<int, int>();

        int _timeoutCallbackId;
        Dictionary<int, TimeoutCallback> _timeoutCallbacks = new Dictionary<int, TimeoutCallback>();
        Dictionary<int, int> _timeoutHandles = new Dictionary<int, int>();

        int _intervalCallbackId;
        Dictionary<int, IntervalCallback> _intervalCallbacks = new Dictionary<int, IntervalCallback>();
        Dictionary<int, int> _intervalHandles = new Dictionary<int, int>();

        public delegate void OnResizeDelegate(object sender);
        public delegate void OnMouseMoveDelegate(object sender, int x, int y);
        public delegate void OnMouseDownDelegate(object sender, int x, int y, int buttons);
        public delegate void OnMouseUpDelegate(object sender, int x, int y, int buttons);
        public delegate void OnMouseWheelDelegate(object sender, int deltaX, int deltaY, int deltaZ, int deltaMode);
        public delegate void OnKeyDownDelegate(object sender, char key, int keyCode, int location);
        public delegate void OnKeyUpDelegate(object sender, char key, int keyCode, int location);

        public delegate void OnTouchStartDelegate(object sender, float x, float y, int identifier);
        public delegate void OnTouchMoveDelegate(object sender, float x, float y, int identifier);
        public delegate void OnTouchEndDelegate(object sender, float x, float y, int identifier);
        public delegate void OnTouchCancelDelegate(object sender);

        public delegate void OnGamepadConnectedDelegate(object sender, int index);
        public delegate void OnGamepadDisconnectedDelegate(object sender, int index);


        public OnResizeDelegate OnResize;
        public OnResizeDelegate OnFocus;
        public OnResizeDelegate OnBlur;
        public OnMouseMoveDelegate OnMouseMove;
        public OnMouseDownDelegate OnMouseDown;
        public OnMouseUpDelegate OnMouseUp;
        public OnMouseWheelDelegate OnMouseWheel;
        public OnKeyDownDelegate OnKeyDown;
        public OnKeyUpDelegate OnKeyUp;

        public OnTouchStartDelegate OnTouchStart;
        public OnTouchMoveDelegate OnTouchMove;
        public OnTouchEndDelegate OnTouchEnd;
        public OnTouchCancelDelegate OnTouchCancel;

        public OnGamepadConnectedDelegate OnGamepadConnected;
        public OnGamepadDisconnectedDelegate OnGamepadDisconnected;

        public static Window Current
        { 
            get
            {
                if (_current == null)
                {
                    int uid = JSObject.StaticInvokeRetInt("nkJSObject.GetWindow");
                    _current = new Window(uid);
                }

                return _current;
            }
        }

        public Document Document
        {
            get
            {
                if (_document == null)
                {
                    int uid = InvokeRetInt("nkWindow.GetDocument");
                    _document = new Document(this, uid);
                }

                return _document;
            }
        }

        public Navigator Navigator
        {
            get
            {
                if (_navigator == null)
                {
                    int uid = InvokeRetInt("nkWindow.GetNavigator");
                    _navigator = new Navigator(this, uid);
                }

                return _navigator;
            }
        }

        public int InnerWidth
        {
            get { return InvokeRetInt("nkWindow.GetInnerWidth"); }
        }

        public int InnerHeight
        {
            get { return InvokeRetInt("nkWindow.GetInnerHeight"); }
        }

        public bool IsSecureContext
        {
            get { return InvokeRetBool("nkWindow.GetIsSecureContext"); }
        }

        public Storage SessionStorage
        {
            get
            {
                if (_sessionStorage == null)
                {
                    int uid = InvokeRetInt("nkWindow.GetSessionStorage");
                    if (uid == -1)
                        return null;
                    _sessionStorage = new Storage(uid);
                }

                return _sessionStorage;
            }
        }
        public Storage LocalStorage
        {
            get
            {

                if (_localStorage == null)
                {
                    int uid = InvokeRetInt("nkWindow.GetLocalStorage");
                    if (uid == -1)
                        return null;
                    _localStorage = new Storage(uid);
                }

                return _localStorage;
            }
        }

        private Window(int uid) : base(uid)
        {
            Invoke("nkWindow.RegisterEvents");
        }
        
        private static Window WindowFromUid(int uid)
        {
            if (_current.Uid != uid)
                throw new InvalidOperationException("Invalid uid");
            return _current;
        }

        [JSInvokable]
        public static void JsWindowOnAnimationFrame(int uid, int callbackId, double time)
        {
            Window wnd = WindowFromUid(uid);
            wnd.OnAnimationFrame(callbackId, TimeSpan.FromMilliseconds(time));
        }

        private void OnAnimationFrame(int callbackId, TimeSpan time)
        {
            AnimationFrameCallback animationFrameCallback = _animationFrameCallbacks[callbackId];
            _animationFrameCallbacks.Remove(callbackId);
            _animationFrameRequestHandles.Remove(callbackId);

            animationFrameCallback(time);
        }

        public int RequestAnimationFrame(AnimationFrameCallback animationFrameCallback)
        {
            unchecked { _animationFrameCallbackId++; }
            int callbackId = _animationFrameCallbackId;

            int handle = InvokeRetInt<int>("nkWindow.RequestAnimationFrame", callbackId);

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

            Invoke<int>("nkWindow.CancelAnimationFrame", requestID);

            return;
        }

        [JSInvokable]
        public static void JsWindowOnTimeout(int uid, int callbackId)
        {
            Window wnd = WindowFromUid(uid);
            wnd.OnTimeout(callbackId);
        }

        private void OnTimeout(int callbackId)
        {
            TimeoutCallback timeoutCallback = _timeoutCallbacks[callbackId];
            _timeoutCallbacks.Remove(callbackId);
            _timeoutHandles.Remove(callbackId);

            timeoutCallback();
        }

        public int SetTimeout(TimeoutCallback timeoutCallback)
        {
            unchecked { _timeoutCallbackId++; }
            int callbackId = _timeoutCallbackId;

            int handle = InvokeRetInt<int, int>("nkWindow.SetTimeout", callbackId, 0);

            _timeoutCallbacks.Add(callbackId, timeoutCallback);
            _timeoutHandles.Add(callbackId, handle);

            return callbackId;
        }

        public int SetTimeout(TimeoutCallback timeoutCallback, TimeSpan delay)
        {
            unchecked { _timeoutCallbackId++; }
            int callbackId = _timeoutCallbackId;

            int handle = InvokeRetInt<int, int>("nkWindow.SetTimeout", callbackId, (int)delay.TotalMilliseconds);

            _timeoutCallbacks.Add(callbackId, timeoutCallback);
            _timeoutHandles.Add(callbackId, handle);

            return callbackId;
        }

        public void ClearTimeout(int timeoutID)
        {
            int callbackId = timeoutID;
            timeoutID = _timeoutHandles[callbackId];

            _timeoutCallbacks.Remove(callbackId);
            _timeoutHandles.Remove(callbackId);

            Invoke<int>("nkWindow.ClearTimeout", timeoutID);
        }

        [JSInvokable]
        public static void JsWindowOnInterval(int uid, int intervalId)
        {
            Window wnd = WindowFromUid(uid);
            wnd.OnInterval(intervalId);
        }

        private void OnInterval(int intervalId)
        {
            IntervalCallback intervalCallback = _intervalCallbacks[intervalId];

            intervalCallback();
        }

        public int SetInterval(IntervalCallback intervalCallback)
        {
            unchecked { _intervalCallbackId++; }
            int intervalId = _intervalCallbackId;

            int handle = InvokeRetInt<int, int>("nkWindow.SetInterval", intervalId, 0);

            _intervalCallbacks.Add(intervalId, intervalCallback);
            _intervalHandles.Add(intervalId, handle);

            return intervalId;
        }

        public int SetInterval(IntervalCallback intervalCallback, TimeSpan delay)
        {
            unchecked { _intervalCallbackId++; }
            int intervalId = _intervalCallbackId;

            int handle = InvokeRetInt<int, int>("nkWindow.SetInterval", intervalId, (int)delay.TotalMilliseconds);

            _intervalCallbacks.Add(intervalId, intervalCallback);
            _intervalHandles.Add(intervalId, handle);

            return intervalId;
        }

        public void ClearInterval(int intervalID)
        {
            int callbackId = intervalID;
            intervalID = _intervalHandles[callbackId];

            _intervalCallbacks.Remove(callbackId);
            _intervalHandles.Remove(callbackId);

            Invoke<int>("nkWindow.ClearInterval", intervalID);
        }

        [JSInvokable]
        public static void JsWindowOnResize(int uid)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnResize;
            if (handler != null)
                handler(wnd);
        }

        [JSInvokable]
        public static void JsWindowOnFocus(int uid)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnFocus;
            if (handler != null)
                handler(wnd);
        }

        [JSInvokable]
        public static void JsWindowOnBlur(int uid)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnBlur;
            if (handler != null)
                handler(wnd);
        }

        [JSInvokable]
        public static void JsWindowOnMouseMove(int uid, int x, int y)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnMouseMove;
            if (handler != null)
                handler(wnd, x, y);
        }

        [JSInvokable]
        public static void JsWindowOnMouseDown(int uid, int x, int y, int buttons)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnMouseDown;
            if (handler != null)
                handler(wnd, x, y, buttons);
        }

        [JSInvokable]
        public static void JsWindowOnMouseUp(int uid, int x, int y, int buttons)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnMouseUp;
            if (handler != null)
                handler(wnd, x, y, buttons);
        }

        [JSInvokable]
        public static void JsWindowOnMouseWheel(int uid, float deltaX, float deltaY, float deltaZ, int deltaMode)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnMouseWheel;
            if (handler != null)
                handler(wnd, (int)deltaX, (int)deltaY, (int)deltaZ, deltaMode);
        }

        [JSInvokable]
        public static void JsWindowOnTouchStart(int uid, float x, float y, int identifier)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnTouchStart;
            if (handler != null)
                handler(wnd, x, y, identifier);
        }

        [JSInvokable]
        public static void JsWindowOnTouchMove(int uid, float x, float y, int identifier)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnTouchMove;
            if (handler != null)
                handler(wnd, x, y, identifier);
        }

        [JSInvokable]
        public static void JsWindowOnTouchEnd(int uid, float x, float y, int identifier)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnTouchEnd;
            if (handler != null)
                handler(wnd, x, y, identifier);
        }

        [JSInvokable]
        public static void JsWindowOnTouchCancel(int uid)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnTouchCancel;
            if (handler != null)
                handler(wnd);
        }

        [JSInvokable]
        public static void JsWindowOnKeyDown(int uid, int key, int keyCode, int location)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnKeyDown;
            if (handler != null)
                handler(wnd, (char)key, keyCode, location);
        }

        [JSInvokable]
        public static void JsWindowOnKeyUp(int uid, int key, int keyCode, int location)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnKeyUp;
            if (handler != null)
                handler(wnd, (char)key, keyCode, location);
        }

        [JSInvokable]
        public static void JsWindowGamepadConnected(int uid, int index)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnGamepadConnected;
            if (handler != null)
                handler(wnd, index);
        }

        [JSInvokable]
        public static void JsWindowGamepadDisconnected(int uid, int index)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnGamepadDisconnected;
            if (handler != null)
                handler(wnd, index);
        }
    }
}
