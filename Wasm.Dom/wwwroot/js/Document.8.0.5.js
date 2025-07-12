window.nkDocument =
{
    GetTitle: function (uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.title;
    },
    SetTitle: function (uid, d)
    {
        var dc = nkJSObject.GetObject(uid);
        var tl = nkJSObject.ReadString(d+ 0);
        dc.title = tl;
    },
    GetElementById: function (uid, d)
    {
        var dc = nkJSObject.GetObject(uid);
        var id = nkJSObject.ReadString(d+ 0);
        var el = dc.getElementById(id);
        return nkJSObject.RegisterObject(el);
    },
    HasFocus: function (uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.hasFocus();
    }
};

window.nkElement =
{
    GetClientWidth: function(uid)
    {
        var e = nkJSObject.GetObject(uid);
        return e.clientWidth;
    },
    GetClientHeight: function (uid)
    {
        var e = nkJSObject.GetObject(uid);
        return e.clientHeight;
    },
};

window.nkHTMLElement =
{
    GetStyle: function (uid, d)
    {
        var he = nkJSObject.GetObject(uid);

        var st = he.style;

        var uid = nkJSObject.GetUid(st);
        if (uid !== -1)
            return uid;

        return nkJSObject.RegisterObject(st);
    },
    Focus: function (uid, d)
    {
        var he = nkJSObject.GetObject(uid);
        var st = he.focus();
    },
    Blur: function (uid, d)
    {
        var he = nkJSObject.GetObject(uid);
        var st = he.blur();
    },
};

window.nkStyleDeclaration =
{
    GetPropertyValue: function (uid, d)
    {
        var st = nkJSObject.GetObject(uid);
        var pr = nkJSObject.ReadString(d + 0);

        return st.getPropertyValue(pr);
    },

    SetProperty: function (uid, d)
    {
        var st = nkJSObject.GetObject(uid);
        var pr = nkJSObject.ReadString(d + 0);
        var va = nkJSObject.ReadString(d + 4);

        st.setProperty(pr, va);
    },
};
