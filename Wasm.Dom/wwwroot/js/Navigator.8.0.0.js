window.nkNavigator =
{
    GetUserAgent: function (uid)
    {
        var nv = nkJSObject.GetObject(uid);
        return BINDING.js_to_mono_obj(nv.userAgent);
    },
    GetMaxTouchPoints: function (uid)
    {
        var nv = nkJSObject.GetObject(uid);
        return nv.maxTouchPoints;
    }
};
