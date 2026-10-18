window.nkGPU =
{
    Create: function (uid, module, d)
    {
        var nid = module.HEAP32[(d+ 0)>>2];

        var nv = nkJSObject.GetObject(nid);
        if ("gpu" in nv)
        {
            var gp = nv.gpu;
            var uid = nkJSObject.GetUid(gp);
            if (uid !== -1)
                return uid;

            return nkJSObject.RegisterObject(gp);
        }
        else
            return nkJSObject.RegisterObject(null);
    },
    RequestAdapter: function (uid, module, d)
    {
        var gp = nkJSObject.GetObject(uid);

        var pr = gp.requestAdapter();
        return nkJSObject.RegisterObject(pr);
    },
    RequestAdapter1: function (uid, module, d)
    {
        var gp = nkJSObject.GetObject(uid);
        var bi = module.HEAP32[(d+ 0)>>2];

        var pp = (bi >> 0) & 3;
        var fl = (bi >> 2) & 3;

        var op = {};
        if (pp !== 3)
            op.powerPreference = (pp === 1) ? 'high-performance'
                                    : (pp === 2) ? 'low-power'
                                    : undefined
                                    ;
        if (fl !== 3)
            op.featureLevel = (fl === 1) ? 'core'
                                 : (fl === 2) ? 'compatibility'
                                 : undefined
                                 ;

        var pr = gp.requestAdapter(op);
        return nkJSObject.RegisterObject(pr);
    },
    GetPreferredCanvasFormat: function (uid)
    {
        var gp = nkJSObject.GetObject(uid);

        var tf = gp.getPreferredCanvasFormat();
        return nkGPU.GetTextureFormatId(tf);
    },

    GetTextureFormatId: function (fm)
    {
        switch (fm)
        {
            case "rgba8unorm": return 1;
            case "bgra8unorm": return 2;
            case "rgba16float": return 3;
            default: throw new Error("Unknown GPUTextureFormat: " + fm);
        }
    },    
    GetTextureFormat: function (fm)
    {
        switch (fm)
        {
            case 1: return "rgba8unorm";
            case 2: return "bgra8unorm";
            case 3: return "rgba16float";
            default: throw new Error("Unknown GPUTextureFormat: " + fm);
        }
    },
};

window.nkGPUAdapter =
{
    GetInfo: function (uid)
    {
        var ad = nkJSObject.GetObject(uid);
        var ai = ad.info;
        var iid = nkJSObject.GetUid(ai);
        if (iid !== -1)
            return iid;

        return nkJSObject.RegisterObject(ai);
    },
    GetLimits: function (uid, module, d)
    {
        var ad = nkJSObject.GetObject(uid);
        var pt = module.HEAP32[(d+ 0)>>2];

        nkGPUAdapter.WriteLimits(module, pt, ad.limits);
    },
    WriteLimits: function (module, pt, lm)
    {
        module.HEAP32[(pt+   0)>>2] = lm.maxTextureDimension1D;
        module.HEAP32[(pt+   4)>>2] = lm.maxTextureDimension2D;
        module.HEAP32[(pt+   8)>>2] = lm.maxTextureDimension3D;
        module.HEAP32[(pt+  12)>>2] = lm.maxTextureArrayLayers;
        module.HEAP32[(pt+  16)>>2] = lm.maxBindGroups;
        module.HEAP32[(pt+  20)>>2] = lm.maxBindingsPerBindGroup;
        module.HEAP32[(pt+  24)>>2] = lm.maxDynamicUniformBuffersPerPipelineLayout;
        module.HEAP32[(pt+  28)>>2] = lm.maxDynamicStorageBuffersPerPipelineLayout;
        module.HEAP32[(pt+  32)>>2] = lm.maxSampledTexturesPerShaderStage;
        module.HEAP32[(pt+  36)>>2] = lm.maxSamplersPerShaderStage;
        module.HEAP32[(pt+  40)>>2] = lm.maxStorageBuffersPerShaderStage;
        module.HEAP32[(pt+  44)>>2] = lm.maxStorageTexturesPerShaderStage;
        module.HEAP32[(pt+  48)>>2] = lm.maxUniformBuffersPerShaderStage;
        module.HEAP32[(pt+  52)>>2] = lm.minUniformBufferOffsetAlignment;
        module.HEAP32[(pt+  56)>>2] = lm.minStorageBufferOffsetAlignment;
        module.HEAP32[(pt+  60)>>2] = lm.maxVertexBuffers;
        module.HEAP32[(pt+  64)>>2] = lm.maxVertexAttributes;
        module.HEAP32[(pt+  68)>>2] = lm.maxVertexBufferArrayStride;
        module.HEAP32[(pt+  72)>>2] = lm.maxInterStageShaderVariables;
        module.HEAP32[(pt+  76)>>2] = lm.maxColorAttachments;
        module.HEAP32[(pt+  80)>>2] = lm.maxColorAttachmentBytesPerSample;
        module.HEAP32[(pt+  84)>>2] = lm.maxComputeWorkgroupStorageSize;
        module.HEAP32[(pt+  88)>>2] = lm.maxComputeInvocationsPerWorkgroup;
        module.HEAP32[(pt+  92)>>2] = lm.maxComputeWorkgroupSizeX;
        module.HEAP32[(pt+  96)>>2] = lm.maxComputeWorkgroupSizeY;
        module.HEAP32[(pt+ 100)>>2] = lm.maxComputeWorkgroupSizeZ;
        module.HEAP32[(pt+ 104)>>2] = lm.maxComputeWorkgroupsPerDimension;
        module.HEAP32[(pt+ 108)>>2] = lm.maxStorageBuffersInVertexStage ?? -1;
        module.HEAP32[(pt+ 112)>>2] = lm.maxStorageBuffersInFragmentStage ?? -1;
        module.HEAP32[(pt+ 116)>>2] = lm.maxStorageTexturesInVertexStage ?? -1;
        module.HEAP32[(pt+ 120)>>2] = lm.maxStorageTexturesInFragmentStage ?? -1;

        var dv = new DataView(module.HEAPU8.buffer);
        dv.setBigInt64(pt+ 124, BigInt(lm.maxUniformBufferBindingSize), true);
        dv.setBigInt64(pt+ 132, BigInt(lm.maxStorageBufferBindingSize), true);
        dv.setBigInt64(pt+ 140, BigInt(lm.maxBufferSize), true);
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

        var rl = {};
        var v;

        v = module.HEAP32[(pt+ 0)>>2];
        if (v >= 0)
            rl.maxTextureDimension1D = v;
        v = module.HEAP32[(pt+ 4)>>2];
        if (v >= 0)
            rl.maxTextureDimension2D = v;
        v = module.HEAP32[(pt+ 8)>>2];
        if (v >= 0)
            rl.maxTextureDimension3D = v;
        v = module.HEAP32[(pt+ 12)>>2];
        if (v >= 0)
            rl.maxTextureArrayLayers = v;
        v = module.HEAP32[(pt+ 16)>>2];
        if (v >= 0)
            rl.maxBindGroups = v;
        v = module.HEAP32[(pt+ 20)>>2];
        if (v >= 0)
            rl.maxBindingsPerBindGroup = v;
        v = module.HEAP32[(pt+ 24)>>2];
        if (v >= 0)
            rl.maxDynamicUniformBuffersPerPipelineLayout = v;
        v = module.HEAP32[(pt+ 28)>>2];
        if (v >= 0)
            rl.maxDynamicStorageBuffersPerPipelineLayout = v;
        v = module.HEAP32[(pt+ 32)>>2];
        if (v >= 0)
            rl.maxSampledTexturesPerShaderStage = v;
        v = module.HEAP32[(pt+ 36)>>2];
        if (v >= 0)
            rl.maxSamplersPerShaderStage = v;
        v = module.HEAP32[(pt+ 40)>>2];
        if (v >= 0)
            rl.maxStorageBuffersPerShaderStage = v;
        v = module.HEAP32[(pt+ 44)>>2];
        if (v >= 0)
            rl.maxStorageTexturesPerShaderStage = v;
        v = module.HEAP32[(pt+ 48)>>2];
        if (v >= 0)
            rl.maxUniformBuffersPerShaderStage = v;
        v = module.HEAP32[(pt+ 52)>>2];
        if (v >= 0)
            rl.minUniformBufferOffsetAlignment = v;
        v = module.HEAP32[(pt+ 56)>>2];
        if (v >= 0)
            rl.minStorageBufferOffsetAlignment = v;
        v = module.HEAP32[(pt+ 60)>>2];
        if (v >= 0)
            rl.maxVertexBuffers = v;
        v = module.HEAP32[(pt+ 64)>>2];
        if (v >= 0)
            rl.maxVertexAttributes = v;
        v = module.HEAP32[(pt+ 68)>>2];
        if (v >= 0)
            rl.maxVertexBufferArrayStride = v;
        v = module.HEAP32[(pt+ 72)>>2];
        if (v >= 0)
            rl.maxInterStageShaderVariables = v;
        v = module.HEAP32[(pt+ 76)>>2];
        if (v >= 0)
            rl.maxColorAttachments = v;
        v = module.HEAP32[(pt+ 80)>>2];
        if (v >= 0)
            rl.maxColorAttachmentBytesPerSample = v;
        v = module.HEAP32[(pt+ 84)>>2];
        if (v >= 0)
            rl.maxComputeWorkgroupStorageSize = v;
        v = module.HEAP32[(pt+ 88)>>2];
        if (v >= 0)
            rl.maxComputeInvocationsPerWorkgroup = v;
        v = module.HEAP32[(pt+ 92)>>2];
        if (v >= 0)
            rl.maxComputeWorkgroupSizeX = v;
        v = module.HEAP32[(pt+ 96)>>2];
        if (v >= 0)
            rl.maxComputeWorkgroupSizeY = v;
        v = module.HEAP32[(pt+ 100)>>2];
        if (v >= 0)
            rl.maxComputeWorkgroupSizeZ = v;
        v = module.HEAP32[(pt+ 104)>>2];
        if (v >= 0)
            rl.maxComputeWorkgroupsPerDimension = v;
        v = module.HEAP32[(pt+ 108)>>2];
        if (v >= 0)
            rl.maxStorageBuffersInVertexStage = v;
        v = module.HEAP32[(pt+ 112)>>2];
        if (v >= 0)
            rl.maxStorageBuffersInFragmentStage = v;
        v = module.HEAP32[(pt+ 116)>>2];
        if (v >= 0)
            rl.maxStorageTexturesInVertexStage = v;
        v = module.HEAP32[(pt+ 120)>>2];
        if (v >= 0)
            rl.maxStorageTexturesInFragmentStage = v;

        var dv = new DataView(module.HEAPU8.buffer);
        var l;

        l = dv.getBigInt64(pt+ 124, true);
        if (l >= 0)
            rl.maxUniformBufferBindingSize = Number(l);
        l = dv.getBigInt64(pt+ 132, true);
        if (l >= 0)
            rl.maxStorageBufferBindingSize = Number(l);
        l = dv.getBigInt64(pt+ 140, true);
        if (l >= 0)
            rl.maxBufferSize = Number(l);

        var pr = ad.requestDevice({ requiredLimits: rl });

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
    Configure: function (uid, module, d)
    {
        var gc = nkJSObject.GetObject(uid);

        var pt = module.HEAP32[(d+ 0)>>2];

        var did = module.HEAP32[(pt+ 0)>>2];
        var fm = module.HEAP32[(pt+ 4)>>2];
        var am = module.HEAP32[(pt+ 8)>>2];
        var tm = module.HEAP32[(pt+ 12)>>2];
        var cs = module.HEAP32[(pt+ 16)>>2];
        var us = module.HEAP32[(pt+ 20)>>2];

        var dv = nkJSObject.GetObject(did);

        var cn = {};
        cn.device = dv;
        cn.format = nkGPU.GetTextureFormat(fm);

        if (am === 1)
            cn.alphaMode = "opaque";
        else if (am === 2)
            cn.alphaMode = "premultiplied";

        if (tm === 1)
            cn.toneMappingMode = "standard";
        else if (tm === 2)
            cn.toneMappingMode = "extended";

        if (cs === 1)
            cn.colorSpace = "srgb";
        else if (cs === 2)
            cn.colorSpace = "display-p3";

        if (us !== -1)
            cn.usage = us;

        gc.configure(cn);
    },
    GetConfiguration: function (uid, module, d)
    {
        var gc = nkJSObject.GetObject(uid);
        var pt = module.HEAP32[(d+ 0)>>2];

        var cfg = gc.getConfiguration();
        if (cfg === null)
            return false;

        var am = (cfg.alphaMode === "opaque") ? 1
               : (cfg.alphaMode === "premultiplied") ? 2
               : -1;
        var tm = (cfg.toneMapping.mode === "standard") ? 1
               : (cfg.toneMapping.mode === "extended") ? 2
               : -1;
        var cs = (cfg.colorSpace === "srgb") ? 1
               : (cfg.colorSpace === "display-p3") ? 2
               : -1;

        module.HEAP32[(pt+ 0)>>2] = nkJSObject.GetUid(cfg.device);
        module.HEAP32[(pt+ 4)>>2] = nkGPU.GetTextureFormatId(cfg.format);
        module.HEAP32[(pt+ 8)>>2] = am;
        module.HEAP32[(pt+ 12)>>2] = tm;
        module.HEAP32[(pt+ 16)>>2] = cs;
        module.HEAP32[(pt+ 20)>>2] = cfg.usage;
        return true;
    },
    Unconfigure: function (uid)
    {
        var gc = nkJSObject.GetObject(uid);
        gc.unconfigure();
    },
    GetCurrentTexture: function (uid)
    {
        var gc = nkJSObject.GetObject(uid);
        var tx = gc.getCurrentTexture();
        var tid = nkJSObject.GetUid(tx);
        if (tid !== -1)
            return tid;

        return nkJSObject.RegisterObject(tx);
    },
};

window.nkGPUTexture =
{
    Dimensions: [undefined, '1d', '2d', '3d'],
    Aspects: ['all', 'stencil-only', 'depth-only'],
    Swizzles: [undefined, 'r', 'g', 'b', 'a', '0', '1'],
    ViewDimensions: [undefined, '1d', '2d', '3d', 'cube', '2d-array', 'cube-array'],

    GetWidth: function (uid)
    {
        var tx = nkJSObject.GetObject(uid);
        return tx.width;
    },
    GetHeight: function (uid)
    {
        var tx = nkJSObject.GetObject(uid);
        return tx.height;
    },
    GetDepthOrArrayLayers: function (uid)
    {
        var tx = nkJSObject.GetObject(uid);
        return tx.depthOrArrayLayers;
    },
    GetMipLevelCount: function (uid)
    {
        var tx = nkJSObject.GetObject(uid);
        return tx.mipLevelCount;
    },
    GetSampleCount: function (uid)
    {
        var tx = nkJSObject.GetObject(uid);
        return tx.sampleCount;
    },
    GetDimension: function (uid)
    {
        var tx = nkJSObject.GetObject(uid);
        return nkGPUTexture.Dimensions.indexOf(tx.dimension);
    },
    GetFormat: function (uid)
    {
        var tx = nkJSObject.GetObject(uid);
        return nkGPU.GetTextureFormatId(tx.format);
    },
    GetUsage: function (uid)
    {
        var tx = nkJSObject.GetObject(uid);
        return tx.usage;
    },
    CreateView: function (uid)
    {
        var tx = nkJSObject.GetObject(uid);
        var vw = tx.createView();
        return nkJSObject.RegisterObject(vw);
    },
    CreateView1: function (uid, module, d)
    {
        var tx = nkJSObject.GetObject(uid);
        var bl = module.HEAP32[(d+ 0)>>2];
        var bm = module.HEAP32[(d+ 4)>>2];
        var as = module.HEAP32[(d+ 8)>>2];
        var sw = module.HEAP32[(d+12)>>2];
        var lc = module.HEAP32[(d+16)>>2];
        var dm = module.HEAP32[(d+20)>>2];
        var fm = module.HEAP32[(d+24)>>2];
        var mc = module.HEAP32[(d+28)>>2];
        var us = module.HEAP32[(d+32)>>2];

        var ds = { baseArrayLayer: bl, baseMipLevel: bm, aspect: nkGPUTexture.Aspects[as] };
        if (lc !== -1) ds.arrayLayerCount = lc;
        if (dm !== -1) ds.dimension = nkGPUTexture.ViewDimensions[dm];
        if (fm !== -1) ds.format = nkGPU.GetTextureFormat(fm);
        if (mc !== -1) ds.mipLevelCount = mc;
        if (us !== -1) ds.usage = us;

        if (sw !== -1)
        {
            var sz = '';
            for (var i = 0; i < 4; i++)
                sz += nkGPUTexture.Swizzles[(sw >> (i * 4)) & 0xF];
            ds.swizzle = sz;
        }
        var vw = tx.createView(ds);
        return nkJSObject.RegisterObject(vw);
    },
    Destroy: function (uid)
    {
        var tx = nkJSObject.GetObject(uid);
        tx.destroy();
    },

    ReadSize: function (module, pt)
    {
        var wh = module.HEAP32[(pt + 0) >> 2];
        var ht = module.HEAP32[(pt + 4) >> 2];
        var dp = module.HEAP32[(pt + 8) >> 2];

        return [wh, ht, dp];
    },
};

window.nkGPUDevice =
{
    GetLimits: function (uid, module, d)
    {
        var dv = nkJSObject.GetObject(uid);
        var pt = module.HEAP32[(d+ 0)>>2];

        nkGPUAdapter.WriteLimits(module, pt, dv.limits);
    },
    CreateCommandEncoder: function (uid)
    {
        var dv = nkJSObject.GetObject(uid);
        var ce = dv.createCommandEncoder();
        return nkJSObject.RegisterObject(ce);
    },
    CreateBuffer: function (uid, module, d)
    {
        var dv = nkJSObject.GetObject(uid);
        var size = module.HEAP32[(d+ 0)>>2];
        var usage = module.HEAP32[(d+ 4)>>2];
        var mac = module.HEAP32[(d+ 8)>>2];

        var bf = dv.createBuffer({ size: size, usage: usage, mappedAtCreation: (mac !== 0) });
        return nkJSObject.RegisterObject(bf);
    },
    CreateShaderModule: function (uid, module, d)
    {
        var dv = nkJSObject.GetObject(uid);
        var code = nkJSObject.ReadString(module, d+ 0);

        var sm = dv.createShaderModule({ code: code });
        return nkJSObject.RegisterObject(sm);
    },

    CreateBindGroup: function (uid, module, d)
    {
        var dv = nkJSObject.GetObject(uid);
        var duid = module.HEAP32[(d+ 0)>>2];
        var dc = nkJSObject.GetObject(duid);

        var bindGroup = dv.createBindGroup(dc);
        return nkJSObject.RegisterObject(bindGroup);
    },
    CreateRenderPipeline: function (uid, module, d)
    {
        var dv = nkJSObject.GetObject(uid);
        var duid = module.HEAP32[(d+ 0)>>2];
        var dc = nkJSObject.GetObject(duid);

        var pl = dv.createRenderPipeline(dc);
        return nkJSObject.RegisterObject(pl);
    },
    GetQueue: function (uid)
    {
        var dv = nkJSObject.GetObject(uid);
        var qu = dv.queue;
        var qid = nkJSObject.GetUid(qu);
        if (qid !== -1)
            return qid;

        return nkJSObject.RegisterObject(qu);
    },
    CreateTexture: function (uid, module, d)
    {
        var dv = nkJSObject.GetObject(uid);
        var fm = module.HEAP32[(d+ 0)>>2];
        var us = module.HEAP32[(d+ 4)>>2];
        var ps = module.HEAP32[(d+ 8)>>2];
        var ml = module.HEAP32[(d+12)>>2];
        var sc = module.HEAP32[(d+16)>>2];
        var dm = module.HEAP32[(d+20)>>2];

        var descriptor = {};
        descriptor.size = nkGPUTexture.ReadSize(module, ps);
        descriptor.format = nkGPU.GetTextureFormat(fm);
        descriptor.usage = us;
        descriptor.mipLevelCount = ml;
        descriptor.sampleCount = sc;
        descriptor.dimension = nkGPUTexture.Dimensions[dm];

        var tx = dv.createTexture(descriptor);
        return nkJSObject.RegisterObject(tx);
    },
        
    Destroy: function (uid)
    {
        var dv = nkJSObject.GetObject(uid);
        dv.destroy();
    },
};

window.nkGPUVertexState =
{
    Create: function (uid, module, d)
    {
        var vs = { };
        return nkJSObject.RegisterObject(vs);
    },
    SetModule: function (uid, module, d)
    {
        var vs = nkJSObject.GetObject(uid);
        var muid = module.HEAP32[(d+ 0)>>2];

        vs.module = nkJSObject.GetObject(muid);
    },
    SetEntryPoint: function (uid, module, d)
    {
        var vs = nkJSObject.GetObject(uid);
        var ep = nkJSObject.ReadString(module, d+ 0);

        vs.entryPoint = ep;
    },
    SetBuffers: function (uid, module, d)
    {
        var vs = nkJSObject.GetObject(uid);
        var buid = module.HEAP32[(d+ 0)>>2];

        if (buid !== -1)
            vs.buffers = nkJSObject.GetObject(buid);
        else
            delete vs.buffers;
    },
};

window.nkGPUColorTargetStateCollection =
{
    BlendOperations: [undefined, 'add', 'subtract', 'reverse-subtract', 'min', 'max'],
    BlendFactors: [undefined, 'zero', 'one', 'src', 'one-minus-src', 'src-alpha', 'one-minus-src-alpha', 'dst', 'one-minus-dst', 'dst-alpha', 'one-minus-dst-alpha', 'src-alpha-saturated', 'constant', 'one-minus-constant'],

    Create: function (uid, module, d)
    {
        var ar = [];
        return nkJSObject.RegisterObject(ar);
    },

    ReadTarget: function (module, pt)
    {
        var tf = module.HEAP32[(pt+ 0)>>2];
        var wm = module.HEAP32[(pt+ 4)>>2];
        var hb = module.HEAP32[(pt+ 8)>>2];

        var tg = { format: nkGPU.GetTextureFormat(tf) };
        if (wm !== -1)
            tg.writeMask = wm;
        if (hb !== 0)
        {
            tg.blend =
            {
                color: nkGPUColorTargetStateCollection.ReadBlendComponent(module, pt + 12),
                alpha: nkGPUColorTargetStateCollection.ReadBlendComponent(module, pt + 24),
            };
        }
        return tg;
    },
    WriteTarget: function (module, pt, tg)
    {
        var wm = (tg.writeMask === undefined) ? -1 : tg.writeMask;

        module.HEAP32[(pt+ 0)>>2] = nkGPU.GetTextureFormatId(tg.format);
        module.HEAP32[(pt+ 4)>>2] = wm;
        if (tg.blend === undefined)
        {
            module.HEAP32[(pt+ 8)>>2] = 0;
            nkGPUColorTargetStateCollection.WriteBlendComponent(module, pt + 12, { });
            nkGPUColorTargetStateCollection.WriteBlendComponent(module, pt + 24, { });
        }
        else
        {
            module.HEAP32[(pt+ 8)>>2] = 1;
            nkGPUColorTargetStateCollection.WriteBlendComponent(module, pt + 12, tg.blend.color);
            nkGPUColorTargetStateCollection.WriteBlendComponent(module, pt + 24, tg.blend.alpha);
        }
    },
    ReadBlendComponent: function (module, pt)
    {
        var op = module.HEAP32[(pt+ 0)>>2];
        var sb = module.HEAP32[(pt+ 4)>>2];
        var db = module.HEAP32[(pt+ 8)>>2];

        var component = { };
        if (op !== 0)
            component.operation = nkGPUColorTargetStateCollection.BlendOperations[op];
        if (sb !== 0)
            component.srcFactor = nkGPUColorTargetStateCollection.BlendFactors[sb];
        if (db !== 0)
            component.dstFactor = nkGPUColorTargetStateCollection.BlendFactors[db];
        return component;
    },
    WriteBlendComponent: function (module, pt, component)
    {
        var op = (component.operation === undefined) ? 0 : nkGPUColorTargetStateCollection.BlendOperations.indexOf(component.operation);
        var sb = (component.srcFactor === undefined) ? 0 : nkGPUColorTargetStateCollection.BlendFactors.indexOf(component.srcFactor);
        var db = (component.dstFactor === undefined) ? 0 : nkGPUColorTargetStateCollection.BlendFactors.indexOf(component.dstFactor);

        module.HEAP32[(pt+ 0)>>2] = op;
        module.HEAP32[(pt+ 4)>>2] = sb;
        module.HEAP32[(pt+ 8)>>2] = db;
    },
    Add: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var pt = module.HEAP32[(d+ 0)>>2];

        ar.push(nkGPUColorTargetStateCollection.ReadTarget(module, pt));
    },
    Insert: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = module.HEAP32[(d+ 0)>>2];
        var pt = module.HEAP32[(d+ 4)>>2];

        ar.splice(id, 0, nkGPUColorTargetStateCollection.ReadTarget(module, pt));
    },
    Set: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = module.HEAP32[(d+ 0)>>2];
        var pt = module.HEAP32[(d+ 4)>>2];

        ar[id] = nkGPUColorTargetStateCollection.ReadTarget(module, pt);
    },
    Get: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = module.HEAP32[(d+ 0)>>2];
        var pt = module.HEAP32[(d+ 4)>>2];

        nkGPUColorTargetStateCollection.WriteTarget(module, pt, ar[id]);
    },
    RemoveAt: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = module.HEAP32[(d+ 0)>>2];

        ar.splice(id, 1);
    },
    Clear: function (uid)
    {
        var ar = nkJSObject.GetObject(uid);
        ar.length = 0;
    },
};

window.nkGPUFragmentState =
{
    Create: function (uid, module, d)
    {
        var fs = { };
        fs.targets = [];
        return nkJSObject.RegisterObject(fs);
    },
    SetModule: function (uid, module, d)
    {
        var fs = nkJSObject.GetObject(uid);
        var muid = module.HEAP32[(d+ 0)>>2];

        fs.module = nkJSObject.GetObject(muid);
    },
    SetEntryPoint: function (uid, module, d)
    {
        var fs = nkJSObject.GetObject(uid);
        var ep = nkJSObject.ReadString(module, d+ 0);

        fs.entryPoint = ep;
    },
    SetTargets: function (uid, module, d)
    {
        var fs = nkJSObject.GetObject(uid);
        var tuid = module.HEAP32[(d+ 0)>>2];

        fs.targets = nkJSObject.GetObject(tuid);
    },
};

window.nkGPUBindGroupDescriptor =
{
    Create: function (uid, module, d)
    {
        var dc = { };
        dc.entries = [];
        return nkJSObject.RegisterObject(dc);
    },
    SetLayout: function (uid, module, d)
    {
        var dc = nkJSObject.GetObject(uid);
        var luid = module.HEAP32[(d+ 0)>>2];

        dc.layout = nkJSObject.GetObject(luid);
    },
    SetEntries: function (uid, module, d)
    {
        var dc = nkJSObject.GetObject(uid);
        var euid = module.HEAP32[(d+ 0)>>2];

        dc.entries = nkJSObject.GetObject(euid);
    }
};

window.nkGPUBindGroupEntryCollection =
{
    Create: function (uid, module, d)
    {
        var ar = [];
        return nkJSObject.RegisterObject(ar);
    },
    ReadEntry: function (module, ptr)
    {
        var bg = module.HEAP32[(ptr+ 0)>>2];
        var re = nkJSObject.GetObject(module.HEAP32[(ptr+ 4)>>2]);

        return { binding: bg, resource: re };
    },
    Add: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var ptr = module.HEAP32[(d+ 0)>>2];

        ar.push(nkGPUBindGroupEntryCollection.ReadEntry(module, ptr));
    },
    Insert: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = module.HEAP32[(d+ 0)>>2];
        var ptr = module.HEAP32[(d+ 4)>>2];

        ar.splice(id, 0, nkGPUBindGroupEntryCollection.ReadEntry(module, ptr));
    },
    Set: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = module.HEAP32[(d+ 0)>>2];
        var ptr = module.HEAP32[(d+ 4)>>2];

        ar[id] = nkGPUBindGroupEntryCollection.ReadEntry(module, ptr);
    },
    Get: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = module.HEAP32[(d+ 0)>>2];
        var ptr = module.HEAP32[(d+ 4)>>2];

        var en = ar[id];
        module.HEAP32[(ptr+ 0)>>2] = en.binding;
        module.HEAP32[(ptr+ 4)>>2] = en.resource.nkUid;
    },
};

window.nkGPURenderPipelineDescriptor =
{
    Create: function (uid, module, d)
    {
        var dc = { };
        dc.layout =  "auto";
        return nkJSObject.RegisterObject(dc);
    },
    SetVertex: function (uid, module, d)
    {
        var dc = nkJSObject.GetObject(uid);
        var vuid = module.HEAP32[(d+ 0)>>2];

        dc.vertex = nkJSObject.GetObject(vuid);
    },
    SetFragment: function (uid, module, d)
    {
        var dc = nkJSObject.GetObject(uid);
        var fuid = module.HEAP32[(d+ 0)>>2];

        if (fuid !== -1)
            dc.fragment = nkJSObject.GetObject(fuid);
        else
            delete dc.fragment;
    },
    SetPrimitive: function (uid, module, d)
    {
        var dc = nkJSObject.GetObject(uid);
        var pt = module.HEAP32[(d+ 0)>>2];

        if (pt === 0)
        {
            delete dc.primitive;
            return;
        }

        var tp = module.HEAP32[(pt+ 0)>>2];
        var ff = module.HEAP32[(pt+ 4)>>2];
        var cm = module.HEAP32[(pt+ 8)>>2];
        var ud = module.HEAP32[(pt+ 12)>>2];
        var si = module.HEAP32[(pt+ 16)>>2];

        var tps = ["triangle-list", "line-list", "point-list", "triangle-strip", "line-strip"];
        var ffs = ["ccw", "cw"];
        var cms = ["none", "front", "back"];
        var sis = [undefined, "uint16", "uint32"];

        var pr = { };
        pr.topology = tps[tp];
        pr.frontFace = ffs[ff];
        pr.cullMode = cms[cm];
        pr.unclippedDepth = (ud !== 0);
        if (si !== 0)
            pr.stripIndexFormat = sis[si];

        dc.primitive = pr;
    },
};

window.nkGPURenderPassDescriptor =
{
    Create: function (uid, module, d)
    {
        var dc = { };
        dc.colorAttachments = [];
        return nkJSObject.RegisterObject(dc);
    },
    SetMaxDrawCount: function (uid, module, d)
    {
        var dc = nkJSObject.GetObject(uid);
        var mdc = module.HEAP32[(d+ 0)>>2];

        if (mdc !== -1)
            dc.maxDrawCount = mdc;
        else
            delete dc.maxDrawCount;
    },
    SetColorAttachments: function (uid, module, d)
    {
        var dc = nkJSObject.GetObject(uid);
        var cuid = module.HEAP32[(d+ 0)>>2];

        dc.colorAttachments = nkJSObject.GetObject(cuid);
    },
};

window.nkGPUVertexBufferLayout =
{
    stepModes: ["vertex", "instance"],
    vertexFormats: [null, "float32", "float32x2", "float32x3", "float32x4"],

    Create: function (uid, module, d)
    {
        var vl = { };
        vl.arrayStride = 0;
        vl.stepMode = nkGPUVertexBufferLayout.stepModes[0];
        vl.attributes = [];
        return nkJSObject.RegisterObject(vl);
    },
    SetStepMode: function (uid, module, d)
    {
        var vl = nkJSObject.GetObject(uid);
        var sm = module.HEAP32[(d+ 0)>>2];

        vl.stepMode = nkGPUVertexBufferLayout.stepModes[sm];
    },
    GetStepMode: function (uid)
    {
        var vl = nkJSObject.GetObject(uid);

        return nkGPUVertexBufferLayout.stepModes.indexOf(vl.stepMode);
    },
    SetArrayStride: function (uid, module, d)
    {
        var vl = nkJSObject.GetObject(uid);
        var stride = module.HEAP32[(d+ 0)>>2];

        vl.arrayStride = stride;
    },
    GetArrayStride: function (uid)
    {
        var vl = nkJSObject.GetObject(uid);

        return vl.arrayStride;
    },
    SetAttributes: function (uid, module, d)
    {
        var vl = nkJSObject.GetObject(uid);
        var auid = module.HEAP32[(d+ 0)>>2];

        vl.attributes = nkJSObject.GetObject(auid);
    },
    GetAttributes: function (uid)
    {
        var vl = nkJSObject.GetObject(uid);

        var auid = nkJSObject.GetUid(vl.attributes);
        if (auid !== -1)
            return auid;

        return nkJSObject.RegisterObject(vl.attributes);
    },
};

window.nkGPUVertexAttributeCollection =
{
    Create: function (uid, module, d)
    {
        var ar = [];
        return nkJSObject.RegisterObject(ar);
    },
    ReadAttribute: function (module, pt)
    {
        var fm = module.HEAP32[(pt+ 0)>>2];
        var sl = module.HEAP32[(pt+ 4)>>2];
        var dv = new DataView(module.HEAPU8.buffer);
        var of = dv.getBigInt64(pt+ 8, true);

        var at = { };
        at.format = nkGPUVertexBufferLayout.vertexFormats[fm];
        at.shaderLocation = sl;
        at.offset = Number(of);
        return at;
    },
    Add: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var pt = module.HEAP32[(d+ 0)>>2];

        ar.push(nkGPUVertexAttributeCollection.ReadAttribute(module, pt));
    },
    Insert: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = module.HEAP32[(d+ 0)>>2];
        var pt = module.HEAP32[(d+ 4)>>2];

        ar.splice(id, 0, nkGPUVertexAttributeCollection.ReadAttribute(module, pt));
    },
    Set: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = module.HEAP32[(d+ 0)>>2];
        var pt = module.HEAP32[(d+ 4)>>2];

        ar[id] = nkGPUVertexAttributeCollection.ReadAttribute(module, pt);
    },
    Get: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = module.HEAP32[(d+ 0)>>2];
        var pt = module.HEAP32[(d+ 4)>>2];

        var at = ar[id];
        var dv = new DataView(module.HEAPU8.buffer);
        module.HEAP32[(pt+ 0)>>2] = nkGPUVertexBufferLayout.vertexFormats.indexOf(at.format);
        module.HEAP32[(pt+ 4)>>2] = at.shaderLocation;
        dv.setBigInt64(pt+ 8, BigInt(at.offset), true);
    },
};

window.nkGPUVertexBufferLayoutCollection =
{
    Create: function (uid, module, d)
    {
        var ar = [];
        return nkJSObject.RegisterObject(ar);
    },
};

window.nkGPUColorAttachmentCollection =
{
    Create: function (uid, module, d)
    {
        var ar = [];
        return nkJSObject.RegisterObject(ar);
    },
    ReadColorAttachment: function (module, arrPtr)
    {
        var data = new Int32Array(module.HEAPU8.buffer, arrPtr, 8);
        var fdata = new Float32Array(module.HEAPU8.buffer, arrPtr, 8);

        var vw = nkJSObject.GetObject(data[0]);
        var lo = data[1];
        var so = data[2];
        var ds = data[7];

        var ca = {};
        ca.view = vw;
        ca.clearValue = { r: fdata[3], g: fdata[4], b: fdata[5], a: fdata[6] };
        if (ds !== -1)
            ca.depthSlice = ds;
        if (lo === 1)
            ca.loadOp = "load";
        else if (lo === 2)
            ca.loadOp = "clear";
        if (so === 1)
            ca.storeOp = "store";
        else if (so === 2)
            ca.storeOp = "discard";
        return ca;
    },
    Add: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var arrPtr = module.HEAP32[(d+ 0)>>2];

        ar.push(nkGPUColorAttachmentCollection.ReadColorAttachment(module, arrPtr));
    },
    Insert: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = module.HEAP32[(d+ 0)>>2];
        var arrPtr = module.HEAP32[(d+ 4)>>2];

        ar.splice(id, 0, nkGPUColorAttachmentCollection.ReadColorAttachment(module, arrPtr));
    },
    Set: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = module.HEAP32[(d+ 0)>>2];
        var arrPtr = module.HEAP32[(d+ 4)>>2];

        ar[id] = nkGPUColorAttachmentCollection.ReadColorAttachment(module, arrPtr);
    },
    Get: function (uid, module, d)
    {
        var ar = nkJSObject.GetObject(uid);
        var id = module.HEAP32[(d+ 0)>>2];
        var arrPtr = module.HEAP32[(d+ 4)>>2];

        var data = new Int32Array(module.HEAPU8.buffer, arrPtr, 8);
        var fdata = new Float32Array(module.HEAPU8.buffer, arrPtr, 8);

        var ca = ar[id];
        var cv = ca.clearValue;
        data[0] = ca.view.nkUid;
        data[1] = (ca.loadOp === "load") ? 1 : (ca.loadOp === "clear") ? 2 : 0;
        data[2] = (ca.storeOp === "store") ? 1 : (ca.storeOp === "discard") ? 2 : 0;
        fdata[3] = cv.r;
        fdata[4] = cv.g;
        fdata[5] = cv.b;
        fdata[6] = cv.a;
        data[7] = (ca.depthSlice !== undefined) ? ca.depthSlice : -1;
    },
};

window.nkGPUCommandEncoder =
{
    BeginRenderPass: function (uid, module, d)
    {
        var ce = nkJSObject.GetObject(uid);
        var dcuid = module.HEAP32[(d+ 0)>>2];

        var dc = nkJSObject.GetObject(dcuid);
        var rp = ce.beginRenderPass(dc);
        return nkJSObject.RegisterObject(rp);
    },
    Finish: function (uid)
    {
        var ce = nkJSObject.GetObject(uid);
        var cb = ce.finish();
        return nkJSObject.RegisterObject(cb);
    },
};

window.nkGPUQueue =
{
    Submit: function (uid, module, d)
    {
        var qu = nkJSObject.GetObject(uid);
        var cuid = module.HEAP32[(d+ 0)>>2];

        var cb = nkJSObject.GetObject(cuid);
        qu.submit([cb]);
    },
    WriteBuffer: function (uid, module, d)
    {
        var qu = nkJSObject.GetObject(uid);
        var buid = module.HEAP32[(d+ 0)>>2];
        var offset = module.HEAP32[(d+ 4)>>2];
        var ptr = module.HEAP32[(d+ 8)>>2];
        var size = module.HEAP32[(d+ 12)>>2];

        var bf = nkJSObject.GetObject(buid);
        var data = new Uint8Array(module.HEAPU8.buffer, ptr, size);
        qu.writeBuffer(bf, offset, data);
    },
    WriteTexture: function (uid, module, d)
    {
        var qu = nkJSObject.GetObject(uid);
        var tuid = module.HEAP32[(d+ 0)>>2];
        var mip = module.HEAP32[(d+ 4)>>2];
        var asp = module.HEAP32[(d+ 8)>>2];
        var po = module.HEAP32[(d+12)>>2];
        var ptr = module.HEAP32[(d+16)>>2];
        var size = module.HEAP32[(d+20)>>2];
        var offset = module.HEAP32[(d+24)>>2];
        var bpr = module.HEAP32[(d+28)>>2];
        var rpi = module.HEAP32[(d+32)>>2];
        var ps = module.HEAP32[(d+36)>>2];

        var ox = module.HEAP32[(po+ 0)>>2];
        var oy = module.HEAP32[(po+ 4)>>2];
        var oz = module.HEAP32[(po+ 8)>>2];

        var tx = nkJSObject.GetObject(tuid);
        var data = new Uint8Array(module.HEAPU8.buffer, ptr, size);

        var destination = {};
        destination.texture = tx;
        destination.mipLevel = mip;
        destination.origin = [ox, oy, oz];
        destination.aspect = nkGPUTexture.Aspects[asp];

        var layout = {};
        layout.offset = offset;
        if (bpr !== -1) layout.bytesPerRow = bpr;
        if (rpi !== -1) layout.rowsPerImage = rpi;

        var size = nkGPUTexture.ReadSize(module, ps);

        qu.writeTexture(destination, data, layout, size);
    },
};

window.nkGPURenderPassEncoder =
{
    SetVertexBuffer: function (uid, module, d)
    {
        var pe = nkJSObject.GetObject(uid);
        var sl = module.HEAP32[(d+ 0)>>2];
        var buid = module.HEAP32[(d+ 4)>>2];

        var bf = nkJSObject.GetObject(buid);
        pe.setVertexBuffer(sl, bf);
    },
    SetVertexBuffer1: function (uid, module, d)
    {
        var pe = nkJSObject.GetObject(uid);
        var sl = module.HEAP32[(d+ 0)>>2];
        var buid = module.HEAP32[(d+ 4)>>2];
        var of = module.HEAP32[(d+ 8)>>2];
        var sz = module.HEAP32[(d+ 12)>>2];

        var bf = nkJSObject.GetObject(buid);
        pe.setVertexBuffer(sl, bf, of, sz);
    },
    SetPipeline: function (uid, module, d)
    {
        var pe = nkJSObject.GetObject(uid);
        var puid = module.HEAP32[(d+ 0)>>2];

        var pl = nkJSObject.GetObject(puid);
        pe.setPipeline(pl);
    },
    SetBindGroup: function (uid, module, d)
    {
        var pe = nkJSObject.GetObject(uid);
        var index = module.HEAP32[(d+ 0)>>2];
        var buid = module.HEAP32[(d+ 4)>>2];

        var bg = nkJSObject.GetObject(buid);
        pe.setBindGroup(index, bg);
    },
    Draw: function (uid, module, d)
    {
        var pe = nkJSObject.GetObject(uid);
        var vc = module.HEAP32[(d+ 0)>>2];
        var ic = module.HEAP32[(d+ 4)>>2];
        var fv = module.HEAP32[(d+ 8)>>2];
        var fi = module.HEAP32[(d+ 12)>>2];

        pe.draw(vc, ic, fv, fi);
    },
    End: function (uid)
    {
        var pe = nkJSObject.GetObject(uid);
        pe.end();
    },
};

window.nkGPUBuffer =
{
    Destroy: function (uid)
    {
        var bf = nkJSObject.GetObject(uid);
        bf.destroy();
    },
};

window.nkGPURenderPipeline =
{
    GetBindGroupLayout: function (uid, module, d)
    {
        var pl = nkJSObject.GetObject(uid);
        var id = module.HEAP32[(d+ 0)>>2];

        var lo = pl.getBindGroupLayout(id);
        var loid = nkJSObject.GetUid(lo);
        if (loid !== -1)
            return loid;

        return nkJSObject.RegisterObject(lo);
    },
};
