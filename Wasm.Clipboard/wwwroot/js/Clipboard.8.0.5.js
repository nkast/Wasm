
window.nkClipboard =
{
    Create: function(uid)
    {
        var nv = nkJSObject.GetObject(uid);
        if ('clipboard' in nv)
        {
            var cb = nv.clipboard;
            var uid = nkJSObject.GetUid(cb);
            if (uid !== -1)
                return uid;

            return nkJSObject.RegisterObject(cb);
        }
        else
            return nkJSObject.RegisterObject(null);
    },

    ReadText: function (uid, d)
    {
        var cb = nkJSObject.GetObject(uid);

        var pr = cb.readText();
        return nkJSObject.RegisterObject(pr);
    },
    WriteText: function (uid, d)
    {
        var cb = nkJSObject.GetObject(uid);
        var ct = nkJSObject.ReadString(d+ 0);

        var pr = cb.writeText(ct);
        return nkJSObject.RegisterObject(pr);
    },
};


