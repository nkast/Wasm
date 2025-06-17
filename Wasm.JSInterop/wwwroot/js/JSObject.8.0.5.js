window.nkJSObject =
{
    objectMap: [null],
    emptySlots: [],
    RegisterObject: function(obj)
    {
        if (obj == null)
            return -1;
        if ('nkUid' in obj)
            throw "object already registered";
            
        // debug check
        //if (nkJSObject.objectMap.indexOf(obj) != -1)
        //    throw "object already registered";

        if (nkJSObject.emptySlots.length == 0)
        {
            nkJSObject.objectMap.push(obj);
            var uid = nkJSObject.objectMap.lastIndexOf(obj);
            obj.nkUid = uid;
            return uid;
        }
        else
        {
            var uid = nkJSObject.emptySlots.pop();

            if (nkJSObject.objectMap[uid] !== undefined)
                throw "slot allready used";

            nkJSObject.objectMap[uid] = obj;
            obj.nkUid = uid;
            return uid;
        }
    },
    GetObject: function(uid)
    {
        return nkJSObject.objectMap[uid];
    },
    GetUid: function(obj)
    {
        if (obj !== null)
        {
            if ('nkUid' in obj)
                return obj.nkUid;
        }

        return -1;
    },
    DisposeObject: function(uid)
    {
        var obj = nkJSObject.objectMap[uid];   
                
        if (obj === undefined)
            throw "obj is undefined";
        if (obj.nkUid !== uid)
            throw "invalid nkUid";

        delete obj.nkUid;
        delete nkJSObject.objectMap[uid];
        nkJSObject.emptySlots.push(uid);
    },
    GetWindow: function()
    {
        return nkJSObject.RegisterObject(window);
    },

    ReadString: function(d)
    {
        const pt = Module.HEAP32[(d)>>2];
        var str = BINDING.conv_string(pt);
        return str;
    },

    funcMap: [null],
    utf16Decoder: new TextDecoder("utf-16le"),
    ToJSString: function (pidentifier, length)
    {   
        const memory = new Uint16Array(Module.HEAPU16.buffer, pidentifier, length);
        return nkJSObject.utf16Decoder.decode(memory);
    },
    JSRegisterFunction: function (pidentifier, length)
    {
        const identifier = nkJSObject.ToJSString(pidentifier, length);

        const parts = identifier.split('.');

        let target = globalThis;
        for (let i = 0; i < parts.length - 1; i++)
        {
            target = target[parts[i]];
        }

        const functionName = parts[parts.length - 1];

        const func = target[functionName].bind(target);
        nkJSObject.funcMap.push(func);
        var fid = nkJSObject.funcMap.lastIndexOf(func);

        return fid;
    },
    JSInvoke0Int: function(fid)
    {
        let func = nkJSObject.funcMap[fid];
        return func();
    },
    JSInvoke1Void: function(fid, uid)
    {
        let func = nkJSObject.funcMap[fid];
        func(uid);
    },
    JSInvoke1Bool: function(fid, uid)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid);
    },
    JSInvoke1Int: function(fid, uid)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid);
    },
    JSInvoke1Float: function(fid, uid)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid);
    },
    JSInvoke1String: function(fid, uid)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid);
    },
    JSInvoke2Void: function(fid, uid, d)
    {
        let func = nkJSObject.funcMap[fid];
        func(uid, d);
    },
    JSInvoke2Bool: function(fid, uid, d)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid, d);
    },
    JSInvoke2Int: function(fid, uid, d)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid, d);
    },
    JSInvoke2Float: function(fid, uid, d)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid, d);
    },
    JSInvoke2String: function(fid, uid, d)
    {
        let func = nkJSObject.funcMap[fid];
        return func(uid, d);
    },
}

window.nkJSArray =
{
    GetLength: function (uid, d)
    {
        var ar = nkJSObject.GetObject(uid);
        return ar.length;
    },
    GetItem: function (uid, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = Module.HEAP32[(d + 0 >> 2)];

        var it = ar[id];
        var uid = nkJSObject.GetUid(it);
        if (uid !== -1)
            return uid;

        return nkJSObject.RegisterObject(it);
    },
};

window.nkPromise =
{
    GetValueBoolean: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);
        return pr.AsyncValue;
    },
    GetValueJSObject: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);

        var ob = pr.AsyncValue;
        var uid = nkJSObject.GetUid(ob);
        if (uid !== -1)
            return uid;

        return nkJSObject.RegisterObject(ob);
    },
    GetErrorType: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);

        if (pr.Error instanceof DOMException)
        {
            switch (pr.Error.name)
            {
                case "InvalidStateError":
                    return 11;
                case "NotSupportedError":
                    return 12;
                case "SecurityError":
                    return 13;
                case "NotAllowedError":
                    return 14;
                case "AbortError":
                    return 15;

                default:
                    return 10;
            }
        }
        else if (pr.Error instanceof Error)
        {
            return 2;
        }
        else if (typeof pr.Error === "string")
        {
            return 1;
        }
        else
        {
            return 0;
        }
    },
    GetErrorMessage: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);

        if (pr.Error instanceof DOMException)
        {
            return pr.Error.message;
        }
        else if (pr.Error instanceof Error)
        {
            return pr.Error.message;
        }
        else if (typeof pr.Error === "string")
        {
            return pr.Error;
        }
        else
        {
            return "Unknown Error";
        }
    },

    RegisterEvents: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);

        pr.then((value) =>
        {
            pr.AsyncValue = value;
            DotNet.invokeMethod('nkast.Wasm.JSInterop', 'JsPromiseOnCompleted', uid);
        }
        ).catch((error) =>
        {
            pr.Error = error;
            DotNet.invokeMethod('nkast.Wasm.JSInterop', 'JsPromiseOnError', uid);
        });
    },
};
