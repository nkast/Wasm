window.nkCanvas =
{
    GetWidth: function(uid)
    {
        var c = nkJSObject.GetObject(uid);
        return c.width;
    },
    SetWidth: function(uid,d)
    {
        var c = nkJSObject.GetObject(uid);
        c.width = Blazor.platform.readInt32Field(d, 0);
    },
    GetHeight: function (uid)
    {
        var c = nkJSObject.GetObject(uid);
        return c.height;
    },
    SetHeight: function(uid,d)
    {
        var c = nkJSObject.GetObject(uid);
        c.height = Blazor.platform.readInt32Field(d, 0);
    },
    Create2DContext: function (uid)
    {
        var c = nkJSObject.GetObject(uid);
        var cx = c.getContext("2d");
        return nkJSObject.RegisterObject(cx);
    },
    CreateWebGLContext: function (uid)
    {
        var c = nkJSObject.GetObject(uid);
        var glx = c.getContext("webgl");
        return nkJSObject.RegisterObject(glx);
    },

};