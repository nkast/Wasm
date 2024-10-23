window.nkXHR =
{
    Create: function()
    {
        var xhr = new XMLHttpRequest();
        return nkJSObject.RegisterObject(xhr);
    },

    Open: function(uid,d)
    {
        var xhr = nkJSObject.GetObject(uid);
        var md = nkJSObject.ReadString(d+ 0);
        var ul = nkJSObject.ReadString(d+ 4);
        var ac = Module.HEAP32[(d+ 8)>>2] !== 0;
        xhr.open(md,ul,ac);
    },
    OverrideMimeType: function(uid,d)
    {
        var xhr = nkJSObject.GetObject(uid);
        var mt = nkJSObject.ReadString(d+ 0);
        xhr.overrideMimeType(mt);
    },
    SetRequestHeader: function(uid,d)
    {
        var xhr = nkJSObject.GetObject(uid);
        var hd = nkJSObject.ReadString(d+ 0);
        var vl = nkJSObject.ReadString(d+ 4);
        xhr.setRequestHeader(hd,vl);
    },
    Send: function (uid, d)
    {
        var xhr = nkJSObject.GetObject(uid);
        xhr.send(null);
    },
    GetStatus: function(uid,d)
    {
        var xhr = nkJSObject.GetObject(uid);
        var status = xhr.status;
        return status;
    },
    GetResponseText: function(uid,d)
    {
        var xhr = nkJSObject.GetObject(uid);
        var responseText = xhr.responseText;
        return BINDING.js_to_mono_obj(responseText);
    },
    GetReadyState: function(uid,d)
    {
        var xhr = nkJSObject.GetObject(uid);
        var readyState = xhr.readyState;
        return readyState;
    },
    
    RegisterEvents: function (uid)
    {
        var bs = nkJSObject.GetObject(uid);

        bs.onload = function(event)
        {
            DotNet.invokeMethod('nkast.Wasm.XHR', 'JsXMLHttpRequestOnLoad', uid);
        };
        bs.onerror = function(event)
        {
            DotNet.invokeMethod('nkast.Wasm.XHR', 'JsXMLHttpRequestOnError', uid);
        };
    },
    UnregisterEvents: function (uid)
    {
        var me = nkJSObject.GetObject(uid);
        me.onload = null;
        me.onerror = null;
    },
};
