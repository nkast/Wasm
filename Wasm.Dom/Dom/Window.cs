using System;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using nkast.Wasm.Input;

namespace nkast.Wasm.Dom
{
    public class Window : JSObject
    {
        static Window _current;
        private Document _document;
        private Navigator _navigator;

        public delegate void OnResizeDelegate(object sender);
        public delegate void OnMouseMoveDelegate(object sender, int x, int y);
        public delegate void OnMouseDownDelegate(object sender, int x, int y, int buttons);
        public delegate void OnMouseUpDelegate(object sender, int x, int y, int buttons);
        public delegate void OnMouseWheelDelegate(object sender, int deltaX, int deltaY, int deltaZ, int deltaMode);
        public delegate void OnKeyDownDelegate(object sender, char key, int keyCode);
        public delegate void OnKeyUpDelegate(object sender, char key, int keyCode);

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
                    WebAssemblyJSRuntime runtime = new WasmJSRuntime();
                    int uid = runtime.InvokeUnmarshalled<int>("nkJSObject.GetWindow");
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
                    int uid = InvokeRet<int>("nkWindow.GetDocument");
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
                    int uid = InvokeRet<int>("nkWindow.GetNavigator");
                    _navigator = new Navigator(this, uid);
                }

                return _navigator;
            }
        }

        public int InnerWidth
        {
            get { return InvokeRet<int>("nkWindow.GetInnerWidth"); }
        }

        public int InnerHeight
        {
            get { return InvokeRet<int>("nkWindow.GetInnerHeight"); }
        }

        public bool IsSecureContext
        {
            get { return InvokeRet<bool>("nkWindow.GetIsSecureContext"); }
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
        public static void JsWindowOnKeyDown(int uid, int key, int keyCode)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnKeyDown;
            if (handler != null)
                handler(wnd, (char)key, keyCode);
        }

        [JSInvokable]
        public static void JsWindowOnKeyUp(int uid, int key, int keyCode)
        {
            Window wnd = WindowFromUid(uid);
            var handler = wnd.OnKeyUp;
            if (handler != null)
                handler(wnd, (char)key, keyCode);
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
