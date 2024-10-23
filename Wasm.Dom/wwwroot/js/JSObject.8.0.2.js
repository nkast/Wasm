window.nkJSObject =
{
    objectMap: [],
    emptySlots: [],
    RegisterObject: function(obj)
    {
        if (obj == null)
            return -1;
        if (nkJSObject.objectMap.indexOf(obj) != -1)
            throw "object already registered";

        if (nkJSObject.emptySlots.length == 0)
        {
            nkJSObject.objectMap.push(obj);
            var uid = nkJSObject.objectMap.lastIndexOf(obj);
            uid++;
            return uid;
        }
        else
        {
            var uid = nkJSObject.emptySlots.pop();
            nkJSObject.objectMap[uid] = obj;
            uid++;
            return uid;
        }
    },
    GetObject: function(uid)
    {
        uid--;
        return nkJSObject.objectMap[uid];
    },
    GetUid: function(obj)
    {
        var uid = nkJSObject.objectMap.indexOf(obj);
        if (uid !== -1)
        {
            uid++;
            return uid;
        }
        else
            return -1;
    },
    DisposeObject: function(uid)
    {
        uid--;
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
}

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
        return nkJSObject.RegisterObject(pr.AsyncValue);
    },
    GetErrorMessage: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);
        var mg = pr.Error.message;

        return BINDING.js_to_mono_obj(mg);
    },

    RegisterEvents: function (uid)
    {
        var pr = nkJSObject.GetObject(uid);

        pr.then((value) =>
        {
            pr.AsyncValue = value;
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsPromiseOnCompleted', uid);
        }
        ).catch((error) =>
        {
            pr.Error = error;
            DotNet.invokeMethod('nkast.Wasm.Dom', 'JsPromiseOnError', uid);
        });
    },
};
