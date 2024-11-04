window.nkWindow =
{
    GetDocument: function(uid)
    {
        var w = nkJSObject.GetObject(uid);
        var d = w.document;
        return nkJSObject.RegisterObject(d);
    },
    GetNavigator: function(uid)
    {
        var w = nkJSObject.GetObject(uid);
        var n = w.navigator;
        return nkJSObject.RegisterObject(n);
    },
    GetInnerWidth: function(uid)
    {
        var w = nkJSObject.GetObject(uid);
        return w.innerWidth;
    },
    GetInnerHeight: function(uid)
    {
        var w = nkJSObject.GetObject(uid);
        return w.innerHeight;
    },
    GetIsSecureContext: function (uid)
    {
        var w = nkJSObject.GetObject(uid);
        return w.isSecureContext;
    },
    RequestAnimationFrame: function (uid, d)
    {
        var w  = nkJSObject.GetObject(uid);
        var ci = Module.HEAP32[(d + 0) >> 2];

        var callback = nkWindow.RequestAnimationFrameCallback;
        var handle = w.requestAnimationFrame((time) =>
        {
            callback(uid, time, ci);
        });

        return handle;
    },
    RequestAnimationFrameCallback: function (uid, time, ci)
    {
        DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnAnimationFrame', uid, ci, time);
    },
    CancelAnimationFrame: function (uid, d)
    {
        var w  = nkJSObject.GetObject(uid);
        var rq = Module.HEAP32[(d + 0) >> 2];

        w.cancelAnimationFrame(rq);
    },    
    SetTimeout: function (uid, d)
    {
        var w  = nkJSObject.GetObject(uid);
        var ci = Module.HEAP32[(d+ 0)>>2];
        var dl = Module.HEAP32[(d+ 4)>>2];

        var callback = nkWindow.TimeoutCallback;
        var hd = w.setTimeout(() =>
        {
            callback(uid, ci);
        }, dl);

        return hd;
    },
    TimeoutCallback: function (uid, ci)
    {
        DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnTimeout', uid, ci);
    },
    ClearTimeout: function (uid, d)
    {
        var w  = nkJSObject.GetObject(uid);
        var hd = Module.HEAP32[(d+ 0)>>2];

        w.clearTimeout(hd);
    },

    SetInterval: function (uid, d)
    {
        var w  = nkJSObject.GetObject(uid);
        var ci = Module.HEAP32[(d+ 0)>>2];
        var dl = Module.HEAP32[(d+ 4)>>2];

        var callback = nkWindow.IntervalCallback;
        var hd = w.setInterval(() =>
        {
            callback(uid, ci);
        }, dl);

        return hd;
    },
    IntervalCallback: function (uid, ci)
    {
        DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnInterval', uid, ci);
    },
    ClearInterval: function (uid, d)
    {
        var w  = nkJSObject.GetObject(uid);
        var hd = Module.HEAP32[(d+ 0)>>2];

        w.clearInterval(hd);
    },

    RegisterEvents: function(uid)
    {
        var w = nkJSObject.GetObject(uid);

        window.addEventListener('resize', (event) =>
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnResize', uid);
        });
        window.addEventListener('focus', (event) =>
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnFocus', uid);
        });
        window.addEventListener('blur', (event) =>
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnBlur', uid);
        });

        window.addEventListener('mousemove', (event) =>
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnMouseMove', uid,
                event.clientX, event.clientY);
        });
        window.addEventListener('mousedown', (event) =>
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnMouseDown', uid,
                event.clientX, event.clientY, event.buttons);
        });
        window.addEventListener('mouseup', (event) =>
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnMouseUp', uid,
                event.clientX, event.clientY, event.buttons);
        });
        window.addEventListener('mousewheel', (event) =>
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnMouseWheel', uid,
                event.deltaX, event.deltaY, event.deltaZ,  event.deltaMode);
        });

        window.addEventListener('keydown', (event) =>
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnKeyDown', uid,
                event.key.charCodeAt(0), event.keyCode, event.location);
        });
        window.addEventListener('keyup', (event) =>
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnKeyUp', uid,
                event.key.charCodeAt(0), event.keyCode, event.location);
        });

        window.addEventListener('touchstart', (event) =>
        {
            event.preventDefault();
            for (var i = 0; i < event.changedTouches.length; i++)
            {
                var touch = event.changedTouches[i];
                DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnTouchStart', uid,
                    touch.clientX, touch.clientY, touch.identifier);
            }
        });

        window.addEventListener('touchmove', (event) =>
        {
            event.preventDefault();
            for (var i = 0; i < event.changedTouches.length; i++)
            {
                var touch = event.changedTouches[i];
                DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnTouchMove', uid,
                    touch.clientX, touch.clientY, touch.identifier);
            }
        });

        window.addEventListener('touchend', (event) =>
        {
            event.preventDefault();
            for (var i = 0; i < event.changedTouches.length; i++)
            {
                var touch = event.changedTouches[i];
                DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnTouchEnd', uid,
                    touch.clientX, touch.clientY, touch.identifier);
            }
        });

        window.addEventListener('touchcancel', (event) =>
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnTouchCancel', uid);
        });

        window.addEventListener("gamepadconnected", (event) =>
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowGamepadConnected', uid,
                event.gamepad.index);
        });
        window.addEventListener("gamepaddisconnected", (event) =>
        {
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowGamepadDisconnected', uid,
                event.gamepad.index);
        });
        
    }
};
