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
    RequestAdapter1: function (uid, module, d)
    {
        var a = nkJSObject.GetObject(uid);
        var bi = module.HEAP32[(d+ 0)>>2];

        var pp = (bi >> 0) & 3;
        var fl = (bi >> 2) & 3;

        var options = {};
        if (pp !== 3)
            options.powerPreference = (pp === 1) ? 'high-performance'
                                    : (pp === 2) ? 'low-power'
                                    : undefined
                                    ;
        if (fl !== 3)
            options.featureLevel = (fl === 1) ? 'core'
                                 : (fl === 2) ? 'compatibility'
                                 : undefined
                                 ;

        var pr = a.requestAdapter(options);
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
    GetLimits: function (uid, module, d)
    {
        var adapter = nkJSObject.GetObject(uid);
        var pt = module.HEAP32[(d+ 0)>>2];

        nkGPUAdapter.WriteLimits(module, pt, adapter.limits);
    },
    WriteLimits: function (module, pt, limits)
    {
        module.HEAP32[(pt+ 0)>>2] = limits.maxTextureDimension1D;
        module.HEAP32[(pt+ 4)>>2] = limits.maxTextureDimension2D;
        module.HEAP32[(pt+ 8)>>2] = limits.maxTextureDimension3D;
        module.HEAP32[(pt+ 12)>>2] = limits.maxTextureArrayLayers;
        module.HEAP32[(pt+ 16)>>2] = limits.maxBindGroups;
        module.HEAP32[(pt+ 20)>>2] = limits.maxBindingsPerBindGroup;
        module.HEAP32[(pt+ 24)>>2] = limits.maxDynamicUniformBuffersPerPipelineLayout;
        module.HEAP32[(pt+ 28)>>2] = limits.maxDynamicStorageBuffersPerPipelineLayout;
        module.HEAP32[(pt+ 32)>>2] = limits.maxSampledTexturesPerShaderStage;
        module.HEAP32[(pt+ 36)>>2] = limits.maxSamplersPerShaderStage;
        module.HEAP32[(pt+ 40)>>2] = limits.maxStorageBuffersPerShaderStage;
        module.HEAP32[(pt+ 44)>>2] = limits.maxStorageTexturesPerShaderStage;
        module.HEAP32[(pt+ 48)>>2] = limits.maxUniformBuffersPerShaderStage;
        module.HEAP32[(pt+ 52)>>2] = limits.minUniformBufferOffsetAlignment;
        module.HEAP32[(pt+ 56)>>2] = limits.minStorageBufferOffsetAlignment;
        module.HEAP32[(pt+ 60)>>2] = limits.maxVertexBuffers;
        module.HEAP32[(pt+ 64)>>2] = limits.maxVertexAttributes;
        module.HEAP32[(pt+ 68)>>2] = limits.maxVertexBufferArrayStride;
        module.HEAP32[(pt+ 72)>>2] = limits.maxInterStageShaderVariables;
        module.HEAP32[(pt+ 76)>>2] = limits.maxColorAttachments;
        module.HEAP32[(pt+ 80)>>2] = limits.maxColorAttachmentBytesPerSample;
        module.HEAP32[(pt+ 84)>>2] = limits.maxComputeWorkgroupStorageSize;
        module.HEAP32[(pt+ 88)>>2] = limits.maxComputeInvocationsPerWorkgroup;
        module.HEAP32[(pt+ 92)>>2] = limits.maxComputeWorkgroupSizeX;
        module.HEAP32[(pt+ 96)>>2] = limits.maxComputeWorkgroupSizeY;
        module.HEAP32[(pt+ 100)>>2] = limits.maxComputeWorkgroupSizeZ;
        module.HEAP32[(pt+ 104)>>2] = limits.maxComputeWorkgroupsPerDimension;
        module.HEAP32[(pt+ 108)>>2] = limits.maxStorageBuffersInVertexStage ?? -1;
        module.HEAP32[(pt+ 112)>>2] = limits.maxStorageBuffersInFragmentStage ?? -1;
        module.HEAP32[(pt+ 116)>>2] = limits.maxStorageTexturesInVertexStage ?? -1;
        module.HEAP32[(pt+ 120)>>2] = limits.maxStorageTexturesInFragmentStage ?? -1;

        var dv = new DataView(module.HEAPU8.buffer);
        dv.setBigInt64(pt+ 124, BigInt(limits.maxUniformBufferBindingSize), true);
        dv.setBigInt64(pt+ 132, BigInt(limits.maxStorageBufferBindingSize), true);
        dv.setBigInt64(pt+ 140, BigInt(limits.maxBufferSize), true);
    },
    RequestDevice: function (uid, module, d)
    {
        var adapter = nkJSObject.GetObject(uid);

        var pr = adapter.requestDevice();
        return nkJSObject.RegisterObject(pr);
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

window.nkGPUCanvasContext =
{
};

window.nkGPUDevice =
{
    GetLimits: function (uid, module, d)
    {
        var device = nkJSObject.GetObject(uid);
        var pt = module.HEAP32[(d+ 0)>>2];

        nkGPUAdapter.WriteLimits(module, pt, device.limits);
    },
    Destroy: function (uid)
    {
        var device = nkJSObject.GetObject(uid);
        device.destroy();
    },
};
