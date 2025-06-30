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
    GetSessionStorage: function(uid)
    {
        var w = nkJSObject.GetObject(uid);
        
        try 
        {
            var ss = w.sessionStorage;
            var ssuid = nkJSObject.RegisterObject(ss);            
            delete ss.nkUid;
            return ssuid;
        }
        catch (e)
        {
             return -1;
        }

    },
    GetLocalStorage: function(uid)
    {
        var w = nkJSObject.GetObject(uid);

        try 
        {
            var ls = w.localStorage;
            var lsuid = nkJSObject.RegisterObject(ls);
            delete ls.nkUid;
            return lsuid;
        }
        catch (e)
        {
             return -1;
        }
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
    GetDevicePixelRatio: function (uid)
    {
        var w = nkJSObject.GetObject(uid);
        return w.devicePixelRatio;
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

        window.addEventListener('keydown', (event) => {
            var char;
            switch (event.key) {
                case "Enter":
                    char = 13;
                    break;
                case "Backspace":
                    char = 8;
                    break;
                case "Tab":
                    char = 9;
                    break;
                case "Delete":
                    char = 127;
                    break;
                default:
                    char = (event.key.length == 1) ? event.key.charCodeAt(0) : 0;
                    break;
            }
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsWindowOnKeyDown', uid,
                char, event.keyCode, event.location);
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

window.nkStorage =
{
    GetLength: function (uid)
    {
        var st = nkJSObject.GetObject(uid);
        return st.length;
    },
    Clear: function (uid, d)
    {
        var st = nkJSObject.GetObject(uid);
        st.clear();
    },
    
    SetItem: function (uid, d)
    {
        var st = nkJSObject.GetObject(uid);
        var ke = nkJSObject.ReadString(d+ 0);
        var va = nkJSObject.ReadString(d+ 4);
        st.setItem(ke, va);
    },    
    GetItem: function (uid, d)
    {
        var st = nkJSObject.GetObject(uid);
        var ke = nkJSObject.ReadString(d+ 0);
        return st.getItem(ke);
    },    
    RemoveItem: function (uid, d)
    {
        var st = nkJSObject.GetObject(uid);
        var ke = nkJSObject.ReadString(d+ 0);
        st.removeItem(ke);
    },
};

window.nkMessagePort =
{
    Start: function (uid, d)
    {
        var mp = nkJSObject.GetObject(uid);
        mp.start();
    },

    Close: function (uid, d)
    {
        var mp = nkJSObject.GetObject(uid);
        mp.close();
    },

    PostMessagei: function (uid, d)
    {
        var mp = nkJSObject.GetObject(uid);
        var ms = Module.HEAP32[(d+ 0)>>2];

        mp.postMessage(ms);
    },
    PostMessagef64: function (uid, d)
    {
        var mp = nkJSObject.GetObject(uid);
        var ms = Module.HEAPF64[(d+ 0)>>3];

        mp.postMessage(ms);
    },
    PostMessageUInt8Array: function (uid, d)
    {
        var mp = nkJSObject.GetObject(uid);
        var id = Module.HEAP32[(d+ 0)>>2];
        var cn = Module.HEAP32[(d+ 4)>>2];
        var arr = Module.HEAP32[(d+ 8)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 1);
        //var arrLen = Blazor.platform.getArrayLength(arr);
        var ms = new Uint8Array(Module.HEAPU8.buffer, arrPtr+id, cn);

        var msCopy = new Uint8Array(ms);
        mp.postMessage(msCopy, [msCopy.buffer]);
    },

    RegisterEvents: function (uid)
    {
        var mp = nkJSObject.GetObject(uid);

        mp.onmessage = function (event)
        {
            var data = event.data;
            if (typeof data === 'number')
            {
                DotNet.invokeMethod('nkast.Wasm.Dom', 'JsMessagePortOnMessagef64', uid, data);
            }
            else if (data instanceof Uint8Array)
            {
                var aid = nkJSObject.RegisterObject(data);
                DotNet.invokeMethod('nkast.Wasm.Dom', 'JsMessagePortOnMessageUInt8Array', uid, aid);
            }
            else
            {
                throw new Error("Unsupported message type: " + typeof data);
            }
        };
    },
    UnregisterEvents: function (uid)
    {
        var mp = nkJSObject.GetObject(uid);
        mp.onmessage = null;
    }
};

