window.nkXHR =
{
    Create: function()
    {
        var xhr = new XMLHttpRequest();
        return nkJSObject.RegisterObject(xhr);
    },

    Open: function(uid, d)
    {
        var module = Module;
        var xhr = nkJSObject.GetObject(uid);
        var md = nkJSObject.ReadString(module, d+ 0);
        var ul = nkJSObject.ReadString(module, d+ 4);
        var ac = module.HEAP32[(d+ 8)>>2] !== 0;
        xhr.open(md,ul,ac);
    },
    OverrideMimeType: function(uid, d)
    {
        var module = Module;
        var xhr = nkJSObject.GetObject(uid);
        var mt = nkJSObject.ReadString(module, d+ 0);
        xhr.overrideMimeType(mt);
    },
    SetRequestHeader: function(uid, d)
    {
        var module = Module;
        var xhr = nkJSObject.GetObject(uid);
        var hd = nkJSObject.ReadString(module, d+ 0);
        var vl = nkJSObject.ReadString(module, d+ 4);
        xhr.setRequestHeader(hd,vl);
    },
    Send: function(uid, d)
    {
        var xhr = nkJSObject.GetObject(uid);
        xhr.send(null);
    },
    GetStatus: function(uid, d)
    {
        var xhr = nkJSObject.GetObject(uid);
        var status = xhr.status;
        return status;
    },
    GetResponseText: function(uid, d)
    {
        var xhr = nkJSObject.GetObject(uid);
        return xhr.responseText;
    },
    GetReadyState: function(uid, d)
    {
        var xhr = nkJSObject.GetObject(uid);
        var readyState = xhr.readyState;
        return readyState;
    },

    DecompressBrotliStream: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var cl = module.HEAP32[(d+ 0)>>2];
        var dl = module.HEAP32[(d+ 4)>>2];
        var carr = module.HEAP32[(d+ 8)>>2];
        var darr = module.HEAP32[(d+12)>>2];

        var carrPtr = Blazor.platform.getArrayEntryPtr(carr, 0, 4);
        var darrPtr = Blazor.platform.getArrayEntryPtr(darr, 0, 4);
        
        var cdt = new Int8Array(module.HEAPU8.buffer, carrPtr, cl);
        var ddt = new Int8Array(module.HEAPU8.buffer, darrPtr, dl);
        
        var decompressedArray = BrotliDecode(cdt);
        ddt.set(decompressedArray);
    },
    
    RegisterEvents: function(uid)
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
    UnregisterEvents: function(uid)
    {
        var me = nkJSObject.GetObject(uid);
        me.onload = null;
        me.onerror = null;
    },
};
