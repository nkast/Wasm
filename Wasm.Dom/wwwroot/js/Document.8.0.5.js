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

window.nkInput =
{
    Create: function (uid)
    {
        var it = document.createElement("input");
        return nkJSObject.RegisterObject(it);
    },
    GetType: function (uid)
    {
        var it = nkJSObject.GetObject(uid);

        var ty = it.type;
        switch (ty)
        {
            case "button": return 1;
            case "submit": return 2;

            case "text": return 3;
            case "password":return 4;
            case "hidden":return 5;

            case "checkbox": return 6;
            case "radio": return 7;
            case "file": return 8;

            default:
                throw new Error("Invalid input type: " + ty);
        }
    },
    SetType: function (uid, d)
    {
        var it = nkJSObject.GetObject(uid);
        var ty = Module.HEAP32[(d+ 0)>>2];

        switch (ty)
        {
            case 1: it.type = "button"; break;
            case 2: it.type = "submit"; break;
            case 3: it.type = "text"; break;
            case 4: it.type = "password"; break;
            case 5: it.type = "hidden"; break;
            case 6: it.type = "checkbox"; break;
            case 7: it.type = "radio"; break;
            case 8: it.type = "file"; break;

            default:
                throw new Error("Invalid input type: " + ty);
        }
    },
    GetValue: function (uid)
    {
        var it = nkJSObject.GetObject(uid);
        return it.value;
    },
    SetValue: function (uid, d)
    {
        var it = nkJSObject.GetObject(uid);
        var va = nkJSObject.ReadString(d+ 0);
        it.value = va;
    },
    GetValueAsNumber: function (uid)
    {
        var it = nkJSObject.GetObject(uid);
        return it.valueAsNumber;
    },
    SetValueAsNumber: function (uid, d)
    {
        var it = nkJSObject.GetObject(uid);
        var va = Module.HEAPF64[(d+ 0)>>3];
        it.valueAsNumber = va;
    },
};
