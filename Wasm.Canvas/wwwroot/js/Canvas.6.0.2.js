window.nkCanvas =
{
    GetWidth: function(uid)
    {
        var c = nkJSObject.GetObject(uid);
        return c.width;
    },
    SetWidth: function(uid, width)
    {
        var c = nkJSObject.GetObject(uid);
        c.width = width;
    },
    GetHeight: function (uid)
    {
        var c = nkJSObject.GetObject(uid);
        return c.height;
    },
    SetHeight: function(uid, height)
    {
        var c = nkJSObject.GetObject(uid);
        c.height = height;
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