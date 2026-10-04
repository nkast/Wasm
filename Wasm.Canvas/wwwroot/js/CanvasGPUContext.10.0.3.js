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
        var gpu = nkJSObject.GetObject(uid);
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

        var pr = gpu.requestAdapter(options);
        return nkJSObject.RegisterObject(pr);
    },
};

window.nkGPUAdapter =
{
    GetInfo: function (uid)
    {
        var ad = nkJSObject.GetObject(uid);
        var info = ad.info;
        var iid = nkJSObject.GetUid(info);
        if (iid !== -1)
            return iid;

        return nkJSObject.RegisterObject(info);
    },
    GetLimits: function (uid, module, d)
    {
        var ad = nkJSObject.GetObject(uid);
        var pt = module.HEAP32[(d+ 0)>>2];

        nkGPUAdapter.WriteLimits(module, pt, ad.limits);
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
        var ad = nkJSObject.GetObject(uid);

        var pr = ad.requestDevice();
        return nkJSObject.RegisterObject(pr);
    },
    RequestDevice1: function (uid, module, d)
    {
        var ad = nkJSObject.GetObject(uid);
        var pt = module.HEAP32[(d+ 0)>>2];

        var requiredLimits = {};
        var v;

        v = module.HEAP32[(pt+ 0)>>2];
        if (v >= 0)
            requiredLimits.maxTextureDimension1D = v;
        v = module.HEAP32[(pt+ 4)>>2];
        if (v >= 0)
            requiredLimits.maxTextureDimension2D = v;
        v = module.HEAP32[(pt+ 8)>>2];
        if (v >= 0)
            requiredLimits.maxTextureDimension3D = v;
        v = module.HEAP32[(pt+ 12)>>2];
        if (v >= 0)
            requiredLimits.maxTextureArrayLayers = v;
        v = module.HEAP32[(pt+ 16)>>2];
        if (v >= 0)
            requiredLimits.maxBindGroups = v;
        v = module.HEAP32[(pt+ 20)>>2];
        if (v >= 0)
            requiredLimits.maxBindingsPerBindGroup = v;
        v = module.HEAP32[(pt+ 24)>>2];
        if (v >= 0)
            requiredLimits.maxDynamicUniformBuffersPerPipelineLayout = v;
        v = module.HEAP32[(pt+ 28)>>2];
        if (v >= 0)
            requiredLimits.maxDynamicStorageBuffersPerPipelineLayout = v;
        v = module.HEAP32[(pt+ 32)>>2];
        if (v >= 0)
            requiredLimits.maxSampledTexturesPerShaderStage = v;
        v = module.HEAP32[(pt+ 36)>>2];
        if (v >= 0)
            requiredLimits.maxSamplersPerShaderStage = v;
        v = module.HEAP32[(pt+ 40)>>2];
        if (v >= 0)
            requiredLimits.maxStorageBuffersPerShaderStage = v;
        v = module.HEAP32[(pt+ 44)>>2];
        if (v >= 0)
            requiredLimits.maxStorageTexturesPerShaderStage = v;
        v = module.HEAP32[(pt+ 48)>>2];
        if (v >= 0)
            requiredLimits.maxUniformBuffersPerShaderStage = v;
        v = module.HEAP32[(pt+ 52)>>2];
        if (v >= 0)
            requiredLimits.minUniformBufferOffsetAlignment = v;
        v = module.HEAP32[(pt+ 56)>>2];
        if (v >= 0)
            requiredLimits.minStorageBufferOffsetAlignment = v;
        v = module.HEAP32[(pt+ 60)>>2];
        if (v >= 0)
            requiredLimits.maxVertexBuffers = v;
        v = module.HEAP32[(pt+ 64)>>2];
        if (v >= 0)
            requiredLimits.maxVertexAttributes = v;
        v = module.HEAP32[(pt+ 68)>>2];
        if (v >= 0)
            requiredLimits.maxVertexBufferArrayStride = v;
        v = module.HEAP32[(pt+ 72)>>2];
        if (v >= 0)
            requiredLimits.maxInterStageShaderVariables = v;
        v = module.HEAP32[(pt+ 76)>>2];
        if (v >= 0)
            requiredLimits.maxColorAttachments = v;
        v = module.HEAP32[(pt+ 80)>>2];
        if (v >= 0)
            requiredLimits.maxColorAttachmentBytesPerSample = v;
        v = module.HEAP32[(pt+ 84)>>2];
        if (v >= 0)
            requiredLimits.maxComputeWorkgroupStorageSize = v;
        v = module.HEAP32[(pt+ 88)>>2];
        if (v >= 0)
            requiredLimits.maxComputeInvocationsPerWorkgroup = v;
        v = module.HEAP32[(pt+ 92)>>2];
        if (v >= 0)
            requiredLimits.maxComputeWorkgroupSizeX = v;
        v = module.HEAP32[(pt+ 96)>>2];
        if (v >= 0)
            requiredLimits.maxComputeWorkgroupSizeY = v;
        v = module.HEAP32[(pt+ 100)>>2];
        if (v >= 0)
            requiredLimits.maxComputeWorkgroupSizeZ = v;
        v = module.HEAP32[(pt+ 104)>>2];
        if (v >= 0)
            requiredLimits.maxComputeWorkgroupsPerDimension = v;
        v = module.HEAP32[(pt+ 108)>>2];
        if (v >= 0)
            requiredLimits.maxStorageBuffersInVertexStage = v;
        v = module.HEAP32[(pt+ 112)>>2];
        if (v >= 0)
            requiredLimits.maxStorageBuffersInFragmentStage = v;
        v = module.HEAP32[(pt+ 116)>>2];
        if (v >= 0)
            requiredLimits.maxStorageTexturesInVertexStage = v;
        v = module.HEAP32[(pt+ 120)>>2];
        if (v >= 0)
            requiredLimits.maxStorageTexturesInFragmentStage = v;

        var dv = new DataView(module.HEAPU8.buffer);
        var l;

        l = dv.getBigInt64(pt+ 124, true);
        if (l >= 0)
            requiredLimits.maxUniformBufferBindingSize = Number(l);
        l = dv.getBigInt64(pt+ 132, true);
        if (l >= 0)
            requiredLimits.maxStorageBufferBindingSize = Number(l);
        l = dv.getBigInt64(pt+ 140, true);
        if (l >= 0)
            requiredLimits.maxBufferSize = Number(l);

        var pr = ad.requestDevice({ requiredLimits: requiredLimits });

        return nkJSObject.RegisterObject(pr);
    },
};
window.nkGPUAdapterInfo =
{
    GetDevice: function (uid)
    {
        var ai = nkJSObject.GetObject(uid);
        return ai.device;
    },
    GetDescription: function (uid)
    {
        var ai = nkJSObject.GetObject(uid);
        return ai.description;
    },
    GetVendor: function (uid)
    {
        var ai = nkJSObject.GetObject(uid);
        return ai.vendor;
    },
    GetArchitecture: function (uid)
    {
        var ai = nkJSObject.GetObject(uid);
        return ai.architecture;
    },
};

window.nkGPUCanvasContext =
{
};

window.nkGPUDevice =
{
    GetLimits: function (uid, module, d)
    {
        var dv = nkJSObject.GetObject(uid);
        var pt = module.HEAP32[(d+ 0)>>2];

        nkGPUAdapter.WriteLimits(module, pt, dv.limits);
    },
    Destroy: function (uid)
    {
        var dv = nkJSObject.GetObject(uid);
        dv.destroy();
    },
};
