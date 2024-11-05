window.nkJSObject =
{
    objectMap: [],
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
            uid++;
            obj.nkUid = uid;
            return uid;
        }
        else
        {
            var uid = nkJSObject.emptySlots.pop();

            if (nkJSObject.objectMap[uid] !== undefined)
                throw "slot allready used";

            nkJSObject.objectMap[uid] = obj;
            uid++;
            obj.nkUid = uid;
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
        if (obj !== null)
        {
            if ('nkUid' in obj)
                return obj.nkUid;
        }

        return -1;
    },
    DisposeObject: function(uid)
    {
        uid--;

        var obj = nkJSObject.objectMap[uid];   
                
        if (obj === undefined)
            throw "obj is undefined";
        if (obj.nkUid !== (uid+1))
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

        var ob = pr.AsyncValue;
        var uid = nkJSObject.GetUid(ob);
        if (uid !== -1)
            return uid;

        return nkJSObject.RegisterObject(ob);
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
