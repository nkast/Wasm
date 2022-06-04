window.nkJSObject =
{
    objectMap: [],
    RegisterObject: function(obj)
    {
        if (obj == null)
            return -1;
        if (nkJSObject.objectMap.indexOf(obj) != -1)
            throw "object already registered";

        nkJSObject.objectMap.push(obj);
        var uid = nkJSObject.objectMap.indexOf(obj);
        return uid;
    },
    GetObject: function(uid)
    {
        return nkJSObject.objectMap[uid];
    },
    DisposeObject: function(uid)
    {
        delete nkJSObject.objectMap[uid];
    },
    GetWindow: function()
    {
        return nkJSObject.RegisterObject(window);
    }
}
