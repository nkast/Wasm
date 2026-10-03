window.nkGPU =
{
    Create: function (uid, module, d)
    {
        var nid = module.HEAP32[(d+ 0)>>2];

        var nv = nkJSObject.GetObject(nid);
        if ("gpu" in nv)
        {
            var gpu = nv.gpu;
            var uid = nkJSObject.GetUid(gpu);
            if (uid !== -1)
                return uid;

            return nkJSObject.RegisterObject(gpu);
        }
        else
            return nkJSObject.RegisterObject(null);
    },
    RequestAdapter: function (uid, module, d)
    {
        var gpu = nkJSObject.GetObject(uid);

        var pr = gpu.requestAdapter();
        return nkJSObject.RegisterObject(pr);
    },
};

window.nkGPUAdapter =
{
    GetInfo: function (uid)
    {
        var adapter = nkJSObject.GetObject(uid);
        var info = adapter.info;
        var iid = nkJSObject.GetUid(info);
        if (iid !== -1)
            return iid;

        return nkJSObject.RegisterObject(info);
    },
};
window.nkGPUAdapterInfo =
{
    GetDevice: function (uid)
    {
        return nkJSObject.GetObject(uid).device;
    },
    GetDescription: function (uid)
    {
        return nkJSObject.GetObject(uid).description;
    },
    GetVendor: function (uid)
    {
        return nkJSObject.GetObject(uid).vendor;
    },
    GetArchitecture: function (uid)
    {
        return nkJSObject.GetObject(uid).architecture;
    },
};
