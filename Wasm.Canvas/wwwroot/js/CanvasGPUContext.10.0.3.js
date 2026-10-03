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
};
