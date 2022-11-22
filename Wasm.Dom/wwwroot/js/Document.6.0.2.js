window.nkDocument =
{
    GetTitle: function (uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.title;
    },
    SetTitle: function (uid, title)
    {
        var dc = nkJSObject.GetObject(uid);
        dc.title = title;
    },
    GetElementById: function (uid, elementId)
    {
        var dc = nkJSObject.GetObject(uid);
        var el = dc.getElementById(elementId);
        return nkJSObject.RegisterObject(el);
    }
};
