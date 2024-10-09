
window.nkXRSystem =
{
    Create: function (uid)
    {
        var nv = nkJSObject.GetObject(uid);
        if ("xr" in nv)
            return nkJSObject.RegisterObject(nv.xr);
        else
            return nkJSObject.RegisterObject(null);
    },

    IsSessionSupported: function (uid, d)
    {
        var xr = nkJSObject.GetObject(uid);
        var md = Blazor.platform.readStringField(d, 0);

        var pr = xr.isSessionSupported(md);
        var uid = nkJSObject.RegisterObject(pr);
        nkPromise.RegisterEvents(pr, uid);

        return uid;
    },
    RequestSession: function (uid, d)
    {
        var xr = nkJSObject.GetObject(uid);
        var md = Blazor.platform.readStringField(d, 0);

        var pr = xr.requestSession(md);
        var uid = nkJSObject.RegisterObject(pr);
        nkPromise.RegisterEvents(pr, uid);

        return uid;
    },
};

window.nkXRSession =
{
    UpdateRenderState: function (uid, d)
    {
        var ss = nkJSObject.GetObject(uid);

        ss.updateRenderState(null);
    },
    RequestReferenceSpace: function (uid, d)
    {
        var ss = nkJSObject.GetObject(uid);
        var rs = Blazor.platform.readStringField(d, 0);

        var pr = ss.requestReferenceSpace(rs);
        var uid = nkJSObject.RegisterObject(pr);
        nkPromise.RegisterEvents(pr, uid);

        return uid;
    },
    RequestAnimationFrame: function (uid, d)
    {
        var ss = nkJSObject.GetObject(uid);

        var callback = nkXRSession.RequestAnimationFrameCallback;

        var handle = ss.requestAnimationFrame(callback);

        return handle;
    },
    RequestAnimationFrameCallback: function (time, xrFrame)
    {
        debugger;
        var xrFrameUid = nkJSObject.RegisterObject(xrFrame);
        var uid = nkJSObject.objectMap.indexOf(xrFrame.session);
        if (uid !== -1)
            return (uid + 1);

        DotNet.invokeMethod('nkast.Wasm.XR', 'JsXRSessionOnAnimationFrame', uid, time, xrFrameUid);
        nkJSObject.RegisterObject(pr.AsyncValue);
    },
};
