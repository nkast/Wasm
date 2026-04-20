window.nkCanvasGLContext =
{
    Enable: function(uid, d)
    {
        var module = Module;
        var gc  = nkJSObject.GetObject(uid);
        var cp = module.HEAP32[(d+ 0)>>2];
        gc.enable(cp);
    },

    Disable: function(uid, d)
    {
        var module = Module;
        var gc  = nkJSObject.GetObject(uid);
        var cp = module.HEAP32[(d+ 0)>>2];
        gc.disable(cp);
    },

    BlendEquationSeparate: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var cr = module.HEAP32[(d+ 0)>>2];
        var aa = module.HEAP32[(d+ 4)>>2];
        gc.blendEquationSeparate(cr, aa);
    },

    BlendFuncSeparate: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var sc = module.HEAP32[(d+ 0)>>2];
        var dc = module.HEAP32[(d+ 4)>>2];
        var sa = module.HEAP32[(d+ 8)>>2];
        var da = module.HEAP32[(d+12)>>2];
        gc.blendFuncSeparate(sc, dc, sa, da);
    },

    BlendColor: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var r = module.HEAPF32[(d+ 0)>>2];
        var g = module.HEAPF32[(d+ 4)>>2];
        var b = module.HEAPF32[(d+ 8)>>2];
        var a = module.HEAPF32[(d+12)>>2];
        gc.blendColor(r, g, b, a);
    },

    ColorMask: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var r = module.HEAP32[(d+ 0)>>2] !== 0;
        var g = module.HEAP32[(d+ 4)>>2] !== 0;
        var b = module.HEAP32[(d+ 8)>>2] !== 0;
        var a = module.HEAP32[(d+12)>>2] !== 0;
        gc.colorMask(r, g, b, a);
    },

    CullFace: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var cm = module.HEAP32[(d+ 0)>>2];
        gc.cullFace(cm);
    },

    FrontFace: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var wd = module.HEAP32[(d+ 0)>>2];
        gc.frontFace(wd);
    },

    PolygonOffset: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var fr = module.HEAPF32[(d+ 0)>>2];
        var us = module.HEAPF32[(d+ 4)>>2];
        gc.polygonOffset(fr, us);
    },

    DepthMask: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var en = module.HEAP32[(d+ 0)>>2] !== 0;
        gc.depthMask(en);
    },

    StencilMask: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var ms = module.HEAP32[(d+ 0)>>2];
        gc.stencilMask(ms);
    },

    DepthFunc: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var fc = module.HEAP32[(d+ 0)>>2];
        gc.depthFunc(fc);
    },

    StencilFunc: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var fc = module.HEAP32[(d+ 0)>>2];
        var rf = module.HEAP32[(d+ 4)>>2];
        var mk = module.HEAP32[(d+ 8)>>2];

        gc.stencilFunc(fc, rf, mk);
    },

    StencilOp: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var fl = module.HEAP32[(d+ 0)>>2];
        var zf = module.HEAP32[(d+ 4)>>2];
        var zp = module.HEAP32[(d+ 8)>>2];

        gc.stencilOp(fl, zf, zp);
    },

    Viewport: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var x = module.HEAP32[(d+ 0)>>2];
        var y = module.HEAP32[(d+ 4)>>2];
        var w = module.HEAP32[(d+ 8)>>2];
        var h = module.HEAP32[(d+12)>>2];
        gc.viewport(x, y, w, h);
    },
    
    DepthRange: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var zn = module.HEAPF32[(d+ 0)>>2];
        var zf = module.HEAPF32[(d+ 4)>>2];
        gc.depthRange(zn, zf);
    },

    Scissor: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var x = module.HEAP32[(d+ 0)>>2];
        var y = module.HEAP32[(d+ 4)>>2];
        var w = module.HEAP32[(d+ 8)>>2];
        var h = module.HEAP32[(d+12)>>2];
        gc.scissor(x, y, w, h);
    },

    ClearColor: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var r = module.HEAPF32[(d+ 0)>>2];
        var g = module.HEAPF32[(d+ 4)>>2];
        var b = module.HEAPF32[(d+ 8)>>2];
        var a = module.HEAPF32[(d+12)>>2];
        gc.clearColor(r, g, b, a);
    },
    ClearDepth: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var d = module.HEAPF32[(d+ 0)>>2];
        gc.clearDepth(d);
    },
    ClearStencil: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var s = module.HEAP32[(d+ 0)>>2];
        gc.clearStencil(s);
    },
    Clear: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bb = module.HEAP32[(d+ 0)>>2];
        gc.clear(bb);
    },
    
    GetParameterInt: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var pn = module.HEAP32[(d+ 0)>>2];
        return gc.getParameter(pn);
    },
    GetParameterString: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var pn = module.HEAP32[(d+ 0)>>2];
        return gc.getParameter(pn);
    },

    CreateTexture: function(uid, d)
    {
        var gc = nkJSObject.GetObject(uid);
        var tx = gc.createTexture();
        return nkJSObject.RegisterObject(tx);
    },

    DeleteTexture: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var txuid = module.HEAP32[(d+ 0)>>2];
        var tx = nkJSObject.GetObject(txuid);
        gc.deleteTexture(tx);
    },

    CreateShader: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var st = module.HEAP32[(d+ 0)>>2];
        var sh = gc.createShader(st);
        return nkJSObject.RegisterObject(sh);
    },

    DeleteShader: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bfuid = module.HEAP32[(d+ 0)>>2];
        var bf = nkJSObject.GetObject(bfuid);
        gc.deleteShader(bf);
    },

    CreateProgram: function(uid, d)
    {
        var gc = nkJSObject.GetObject(uid);
        var pg = gc.createProgram();
        return nkJSObject.RegisterObject(pg);
    },

    DeleteProgram: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var pguid = module.HEAP32[(d+ 0)>>2];
        var pg = nkJSObject.GetObject(pguid);
        gc.deleteProgram(pg);
    },

    CreateBuffer: function(uid, d)
    {
        var gc = nkJSObject.GetObject(uid);
        var bf = gc.createBuffer();
        return nkJSObject.RegisterObject(bf);
    },

    DeleteBuffer: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bfuid = module.HEAP32[(d+ 0)>>2];
        var bf = nkJSObject.GetObject(bfuid);
        gc.deleteBuffer(bf);
    },

    CreateFramebuffer: function(uid, d)
    {
        var gc = nkJSObject.GetObject(uid);
        var bf = gc.createFramebuffer();
        return nkJSObject.RegisterObject(bf);
    },

    DeleteFramebuffer: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bfuid = module.HEAP32[(d+ 0)>>2];
        var bf = nkJSObject.GetObject(bfuid);
        gc.deleteFramebuffer(bf);
    },
    
    CreateRenderbuffer: function(uid, d)
    {
        var gc = nkJSObject.GetObject(uid);
        var bf = gc.createRenderbuffer();
        return nkJSObject.RegisterObject(bf);
    },

    DeleteRenderbuffer: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bfuid = module.HEAP32[(d+ 0)>>2];
        var bf = nkJSObject.GetObject(bfuid);
        gc.deleteRenderbuffer(bf);
    },

    ShaderSource: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var shuid = module.HEAP32[(d+ 0)>>2];
        var sr = nkJSObject.ReadString(module, d+ 4);

        var sh = nkJSObject.GetObject(shuid);
        gc.shaderSource(sh, sr);
    },

    GetAttribLocation: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var pguid = module.HEAP32[(d+ 0)>>2];
        var nm = nkJSObject.ReadString(module, d+ 4);

        var pg = nkJSObject.GetObject(pguid);
        return gc.getAttribLocation(pg, nm);
    },

    GetUniformLocation: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var pguid = module.HEAP32[(d+ 0)>>2];
        var nm = nkJSObject.ReadString(module, d+ 4);

        var pg = nkJSObject.GetObject(pguid);
        var ul = gc.getUniformLocation(pg, nm);
        return nkJSObject.RegisterObject(ul);
    },

    Uniform1i: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var v0 = module.HEAP32[(d+ 4)>>2];

        var ul = nkJSObject.GetObject(uluid);
        gc.uniform1i(ul, v0);
    },

    Uniform2i: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var v0 = module.HEAP32[(d+ 4)>>2];
        var v1 = module.HEAP32[(d+ 8)>>2];
        
        var ul = nkJSObject.GetObject(uluid);
        gc.uniform2i(ul, v0, v1);
    },

    Uniform3i: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var v0 = module.HEAP32[(d+ 4)>>2];
        var v1 = module.HEAP32[(d+ 8)>>2];
        var v2 = module.HEAP32[(d+12)>>2];
        
        var ul = nkJSObject.GetObject(uluid);
        gc.uniform3i(ul, v0, v1, v2);
    },

    Uniform4i: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var v0 = module.HEAP32[(d+ 4)>>2];
        var v1 = module.HEAP32[(d+ 8)>>2];
        var v2 = module.HEAP32[(d+12)>>2];
        var v3 = module.HEAP32[(d+16)>>2];

        var ul = nkJSObject.GetObject(uluid);
        gc.uniform4i(ul, v0, v1, v2, v3);
    },

    Uniform1f: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var v0 = module.HEAPF32[(d+ 4)>>2];

        var ul = nkJSObject.GetObject(uluid);
        gc.uniform1f(ul, v0);
    },

    Uniform2f: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var v0 = module.HEAPF32[(d+ 4)>>2];
        var v1 = module.HEAPF32[(d+ 8)>>2];

        var ul = nkJSObject.GetObject(uluid);
        gc.uniform2f(ul, v0, v1);
    },

    Uniform3f: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var v0 = module.HEAPF32[(d+ 4)>>2];
        var v1 = module.HEAPF32[(d+ 8)>>2];
        var v2 = module.HEAPF32[(d+12)>>2];

        var ul = nkJSObject.GetObject(uluid);
        gc.uniform3f(ul, v0, v1, v2);
    },

    Uniform4f: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var v0 = module.HEAPF32[(d+ 4)>>2];
        var v1 = module.HEAPF32[(d+ 8)>>2];
        var v2 = module.HEAPF32[(d+12)>>2];
        var v3 = module.HEAPF32[(d+16)>>2];

        var ul = nkJSObject.GetObject(uluid);
        gc.uniform4f(ul, v0, v1, v2, v3);
    },

    Uniform1iv: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var st    = module.HEAP32[(d+ 4)>>2];
        var arr   = module.HEAP32[(d+ 8)>>2];
        var cn    = module.HEAP32[(d+12)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Int32Array(module.HEAPU8.buffer, arrPtr, cn * st / 4);

        var ul = nkJSObject.GetObject(uluid);
        gc.uniform1iv(ul, dt);
    },
    Uniform2iv: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var st    = module.HEAP32[(d+ 4)>>2];
        var arr   = module.HEAP32[(d+ 8)>>2];
        var cn    = module.HEAP32[(d+12)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Int32Array(module.HEAPU8.buffer, arrPtr, cn * st / 4);

        var ul = nkJSObject.GetObject(uluid);
        gc.uniform2iv(ul, dt);
    },
    Uniform3iv: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var st    = module.HEAP32[(d+ 4)>>2];
        var arr   = module.HEAP32[(d+ 8)>>2];
        var cn    = module.HEAP32[(d+12)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Int32Array(module.HEAPU8.buffer, arrPtr, cn * st / 4);

        var ul = nkJSObject.GetObject(uluid);
        gc.uniform3iv(ul, dt);
    },
    Uniform4iv: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var st    = module.HEAP32[(d+ 4)>>2];
        var arr   = module.HEAP32[(d+ 8)>>2];
        var cn    = module.HEAP32[(d+12)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Int32Array(module.HEAPU8.buffer, arrPtr, cn * st / 4);

        var ul = nkJSObject.GetObject(uluid);
        gc.uniform4iv(ul, dt);
    },

    Uniform1fv: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var st    = module.HEAP32[(d+ 4)>>2];
        var arr   = module.HEAP32[(d+ 8)>>2];
        var cn    = module.HEAP32[(d+12)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Float32Array(module.HEAPU8.buffer, arrPtr, cn * st / 4);

        var ul = nkJSObject.GetObject(uluid);
        gc.uniform1fv(ul, dt);
    },
    Uniform2fv: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var st    = module.HEAP32[(d+ 4)>>2];
        var arr   = module.HEAP32[(d+ 8)>>2];
        var cn    = module.HEAP32[(d+12)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Float32Array(module.HEAPU8.buffer, arrPtr, cn * st / 4);

        var ul = nkJSObject.GetObject(uluid);
        gc.uniform2fv(ul, dt);
    },
    Uniform3fv: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var st    = module.HEAP32[(d+ 4)>>2];
        var arr   = module.HEAP32[(d+ 8)>>2];
        var cn    = module.HEAP32[(d+12)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Float32Array(module.HEAPU8.buffer, arrPtr, cn * st / 4);

        var ul = nkJSObject.GetObject(uluid);
        gc.uniform3fv(ul, dt);
    },
    Uniform4fv: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var st    = module.HEAP32[(d+ 4)>>2];
        var arr   = module.HEAP32[(d+ 8)>>2];
        var cn    = module.HEAP32[(d+12)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Float32Array(module.HEAPU8.buffer, arrPtr, cn * st / 4);

        var ul = nkJSObject.GetObject(uluid);
        gc.uniform4fv(ul, dt);
    },

    UniformMatrix2fv: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var st = module.HEAP32[(d+ 4)>>2];
        var arr = module.HEAP32[(d+ 8)>>2];
        var cn  = module.HEAP32[(d+12)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Float32Array(module.HEAPU8.buffer, arrPtr, cn * st / 4);

        var ul = nkJSObject.GetObject(uluid);
        gc.uniformMatrix2fv(ul, false, dt);
    },
    UniformMatrix3fv: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var st = module.HEAP32[(d+ 4)>>2];
        var arr = module.HEAP32[(d+ 8)>>2];
        var cn  = module.HEAP32[(d+12)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Float32Array(module.HEAPU8.buffer, arrPtr, cn * st / 4);

        var ul = nkJSObject.GetObject(uluid);
        gc.uniformMatrix3fv(ul, false, dt);
    },
    UniformMatrix4fv: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var uluid = module.HEAP32[(d+ 0)>>2];
        var st = module.HEAP32[(d+ 4)>>2];
        var arr = module.HEAP32[(d+ 8)>>2];
        var cn  = module.HEAP32[(d+12)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Float32Array(module.HEAPU8.buffer, arrPtr, cn * st / 4);

        var ul = nkJSObject.GetObject(uluid);
        gc.uniformMatrix4fv(ul, false, dt);
    },
    
    CompileShader: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var shuid = module.HEAP32[(d+ 0)>>2];
        var sh = nkJSObject.GetObject(shuid);
        gc.compileShader(sh);
    },

    GetShaderParameter: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var shuid = module.HEAP32[(d+ 0)>>2];
        var pn = module.HEAP32[(d+ 4)>>2];

        var sh = nkJSObject.GetObject(shuid);
        return gc.getShaderParameter(sh, pn);
    },

    GetProgramParameter: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var pguid = module.HEAP32[(d+ 0)>>2];
        var pn = module.HEAP32[(d+ 4)>>2];

        var pg = nkJSObject.GetObject(pguid);
        return gc.getProgramParameter(pg, pn);
    },

    TexImage2D: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var tg = module.HEAP32[(d+ 0)>>2];
        var lv = module.HEAP32[(d+ 4)>>2];
        var it = module.HEAP32[(d+ 8)>>2];
        var wh = module.HEAP32[(d+12)>>2];
        var ht = module.HEAP32[(d+16)>>2];
        var ft = module.HEAP32[(d+20)>>2];
        var tp = module.HEAP32[(d+24)>>2];

        gc.texImage2D(tg, lv, it, wh, ht, 0, ft, tp, null);
    },

    TexImage2D1: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var tg = module.HEAP32[(d+ 0)>>2];
        var lv = module.HEAP32[(d+ 4)>>2];
        var it = module.HEAP32[(d+ 8)>>2];
        var wh = module.HEAP32[(d+12)>>2];
        var ht = module.HEAP32[(d+16)>>2];
        var ft = module.HEAP32[(d+20)>>2];
        var tp = module.HEAP32[(d+24)>>2];
        var st = module.HEAP32[(d+28)>>2];
        var arr = module.HEAP32[(d+32)>>2];
        var cn  = module.HEAP32[(d+36)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Uint8Array(module.HEAPU8.buffer, arrPtr, cn * st);

        gc.texImage2D(tg, lv, it, wh, ht, 0, ft, tp, dt);
    },

    TexImage2D2: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var tg = module.HEAP32[(d+ 0)>>2];
        var lv = module.HEAP32[(d+ 4)>>2];
        var it = module.HEAP32[(d+ 8)>>2];
        var ft = module.HEAP32[(d+12)>>2];
        var tp = module.HEAP32[(d+16)>>2];

        var vid= module.HEAP32[(d+20)>>2];
        var vi = nkJSObject.GetObject(vid);

        gc.texImage2D(tg, lv, it, ft, tp, vi);
    },

    TexSubImage2D1: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var tg = module.HEAP32[(d+ 0)>>2];
        var lv = module.HEAP32[(d+ 4)>>2];
        var xo = module.HEAP32[(d+ 8)>>2];
        var yo = module.HEAP32[(d+12)>>2];
        var wh = module.HEAP32[(d+16)>>2];
        var ht = module.HEAP32[(d+20)>>2];
        var ft = module.HEAP32[(d+24)>>2];
        var tp = module.HEAP32[(d+28)>>2];
        var st = module.HEAP32[(d+32)>>2];
        var arr = module.HEAP32[(d+36)>>2];
        var cn  = module.HEAP32[(d+40)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Uint8Array(module.HEAPU8.buffer, arrPtr, cn * st);

        gc.texSubImage2D(tg, lv, xo, yo, wh, ht, ft, tp, dt);
    },
  
    CompressedTexImage2D: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var tg = module.HEAP32[(d+ 0)>>2];
        var lv = module.HEAP32[(d+ 4)>>2];
        var it = module.HEAP32[(d+ 8)>>2];
        var wh = module.HEAP32[(d+12)>>2];
        var ht = module.HEAP32[(d+16)>>2];
        var st = module.HEAP32[(d+20)>>2];
        var arr = module.HEAP32[(d+24)>>2];
        var ix = module.HEAP32[(d+28)>>2];
        var ot = module.HEAP32[(d+32)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Uint8Array(module.HEAPU8.buffer, arrPtr + ix * st, ot * st);

        gc.compressedTexImage2D(tg, lv, it, wh, ht, 0, dt);
    },

    ReadPixels: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var x = module.HEAP32[(d+ 0)>>2];
        var y = module.HEAP32[(d+ 4)>>2];
        var w = module.HEAP32[(d+ 8)>>2];
        var h = module.HEAP32[(d+12)>>2];
        var ft = module.HEAP32[(d+16)>>2];
        var tp = module.HEAP32[(d+20)>>2];
        var st = module.HEAP32[(d+24)>>2];
        var arr = module.HEAP32[(d+28)>>2];
        var ix = module.HEAP32[(d+32)>>2];
        var ot = module.HEAP32[(d+36)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Uint8Array(module.HEAPU8.buffer, arrPtr + ix * st, ot * st);

        gc.readPixels(x, y, w, h, ft, tp, dt);
    },

    TexParameteri: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var tg = module.HEAP32[(d+ 0)>>2];
        var pn = module.HEAP32[(d+ 4)>>2];
        var pm = module.HEAP32[(d+ 8)>>2];
        gc.texParameteri(tg, pn, pm);
    },

    PixelStorei: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var pn = module.HEAP32[(d+ 0)>>2];
        var pm = module.HEAP32[(d+ 4)>>2];
        gc.pixelStorei(pn, pm);
    },

    BindTexture: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var tg = module.HEAP32[(d+ 0)>>2];
        var txuid = module.HEAP32[(d+ 4)>>2];
        var tx = (txuid != -1) ? nkJSObject.GetObject(txuid) : null;
        gc.bindTexture(tg, tx);
    },

    BindBuffer: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bt = module.HEAP32[(d+ 0)>>2];
        var bfuid = module.HEAP32[(d+ 4)>>2];
        var bf = nkJSObject.GetObject(bfuid);
        gc.bindBuffer(bt, bf);
    },
    
    BindFramebuffer: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bt = module.HEAP32[(d+ 0)>>2];
        var bfuid = module.HEAP32[(d+ 4)>>2];
        var bf = (bfuid != -1) ? nkJSObject.GetObject(bfuid) : null;
        gc.bindFramebuffer(bt, bf);
    },

    BindRenderbuffer: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bt = module.HEAP32[(d+ 0)>>2];
        var bfuid = module.HEAP32[(d+ 4)>>2];
        var bf = nkJSObject.GetObject(bfuid);
        gc.bindRenderbuffer(bt, bf);
    },
 
    FramebufferRenderbuffer: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var ft = module.HEAP32[(d+ 0)>>2];
        var ap = module.HEAP32[(d+ 4)>>2];
        var rt = module.HEAP32[(d+ 8)>>2];
        var rbuid = module.HEAP32[(d+12)>>2];
        var rb =  (rbuid != -1) ? nkJSObject.GetObject(rbuid) : null;
        gc.framebufferRenderbuffer(ft, ap, rt, rb);
    },
 
    FramebufferTexture2D: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var ft = module.HEAP32[(d+ 0)>>2];
        var ap = module.HEAP32[(d+ 4)>>2];
        var tt = module.HEAP32[(d+ 8)>>2];
        var tbuid = module.HEAP32[(d+12)>>2];
        var tb = nkJSObject.GetObject(tbuid);
        var lv = 0;
        gc.framebufferTexture2D(ft, ap, tt, tb, lv);
    },

    RenderbufferStorage: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bt = module.HEAP32[(d+ 0)>>2];
        var fm = module.HEAP32[(d+ 4)>>2];
        var w  = module.HEAP32[(d+ 8)>>2];
        var h  = module.HEAP32[(d+12)>>2];
        gc.renderbufferStorage(bt, fm, w, h);
    },

    CheckFramebufferStatus: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var ft = module.HEAP32[(d+ 0)>>2];
        return gc.checkFramebufferStatus(ft);
    },

    GenerateMipmap: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var tg = module.HEAP32[(d+ 0)>>2];
        gc.generateMipmap(tg);
    },

    AttachShader: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var pguid = module.HEAP32[(d+ 0)>>2];
        var shuid = module.HEAP32[(d+ 4)>>2];
        var pg = nkJSObject.GetObject(pguid);
        var sh = nkJSObject.GetObject(shuid);
        gc.attachShader(pg, sh);
    },

    GetProgramInfoLog: function(uid, d)
    {        
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var pguid = module.HEAP32[(d+ 0)>>2];
        var pg = nkJSObject.GetObject(pguid);
        return gc.getProgramInfoLog(pg);
    },

    GetShaderInfoLog: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var shuid = module.HEAP32[(d+ 0)>>2];
        var sh = nkJSObject.GetObject(shuid);
        return gc.getShaderInfoLog(sh);
    },

    LinkProgram: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var pguid = module.HEAP32[(d+ 0)>>2];
        var pg = nkJSObject.GetObject(pguid);
        gc.linkProgram(pg);
    },

    BufferData: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bt = module.HEAP32[(d+ 0)>>2];
        var sz = module.HEAP32[(d+ 4)>>2];
        var us = module.HEAP32[(d+ 8)>>2];
        gc.bufferData(bt, sz, us);
    },

    BufferData1: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bt = module.HEAP32[(d+ 0)>>2];
        var us = module.HEAP32[(d+ 4)>>2];
        var st = module.HEAP32[(d+ 8)>>2];
        var arr = module.HEAP32[(d+12)>>2];
        var cn  = module.HEAP32[(d+16)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Float32Array(module.HEAPU8.buffer, arrPtr, cn * st);

        gc.bufferData(bt, dt, us);
    },

    BufferSubData: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bt = module.HEAP32[(d+ 0)>>2];
        var of = module.HEAP32[(d+ 4)>>2];
        var si = module.HEAP32[(d+ 8)>>2];
        var ln = module.HEAP32[(d+12)>>2];
        var st = module.HEAP32[(d+16)>>2];
        var arr = module.HEAP32[(d+20)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Uint8Array(module.HEAPU8.buffer, arrPtr + si * st, ln * st);

        gc.bufferSubData(bt, of, dt);
    },

    VertexAttribPointer: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var ix = module.HEAP32[(d+ 0)>>2];
        var sz = module.HEAP32[(d+ 4)>>2];
        var tp = module.HEAP32[(d+ 8)>>2];
        var nr = module.HEAP32[(d+12)>>2] !== 0;
        var st = module.HEAP32[(d+16)>>2];
        var of = module.HEAP32[(d+20)>>2];
        gc.vertexAttribPointer(ix, sz, tp, nr, st, of);
    },

    EnableVertexAttribArray: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var ix = module.HEAP32[(d+ 0)>>2];
        gc.enableVertexAttribArray(ix);
    },

    DisableVertexAttribArray: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var ix = module.HEAP32[(d+ 0)>>2];
        gc.disableVertexAttribArray(ix);
    },

    UseProgram: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var pguid = module.HEAP32[(d+ 0)>>2];
        var pg = nkJSObject.GetObject(pguid);
        gc.useProgram(pg);
    },

    DrawArrays: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var md = module.HEAP32[(d+ 0)>>2];
        var of = module.HEAP32[(d+ 4)>>2];
        var ct = module.HEAP32[(d+ 8)>>2];

        gc.drawArrays(md, of, ct);
    },

    DrawElements: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var md = module.HEAP32[(d+ 0)>>2];
        var ct = module.HEAP32[(d+ 4)>>2];
        var tp = module.HEAP32[(d+ 8)>>2];
        var of = module.HEAP32[(d+12)>>2];

        gc.drawElements(md, ct, tp, of);
    },

    ActiveTexture: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var tu = module.HEAP32[(d+ 0)>>2];
        
        gc.activeTexture(tu);
    },

    Flush: function(uid, d)
    {
        var gc = nkJSObject.GetObject(uid);
        gc.flush();
    },
    Finish: function(uid, d)
    {
        var gc = nkJSObject.GetObject(uid);
        gc.finish();
    },
    
    IsContextLost: function(uid, d)
    {
        var gc = nkJSObject.GetObject(uid);

        return gc.isContextLost();
    },

    GetExtension: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var nm = nkJSObject.ReadString(module, d+ 0);

        return gc.getExtension(nm) !== null;
    },
    GetExtension1: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var nm = nkJSObject.ReadString(module, d+ 0);
        
        var ex = gc.getExtension(nm);

        return nkJSObject.RegisterObject(ex);
    },

    GetError: function(uid, d)
    {
        var gc = nkJSObject.GetObject(uid);
        return gc.getError();
    },
};

window.nkCanvasGL2Context =
{
    InvalidateFramebuffer: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var ft = module.HEAP32[(d+ 0)>>2];
        var si = module.HEAP32[(d+ 4)>>2];
        var ln = module.HEAP32[(d+ 8)>>2];
        var arr = module.HEAP32[(d+12)>>2];
        
        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        
        var at = new Uint32Array(module.HEAPU8.buffer, arrPtr + si*4, ln);
        var ar = Array.prototype.slice.call(at);

        gc.invalidateFramebuffer(ft, ar);
    },
    BlitFramebuffer: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var sX0 = module.HEAP32[(d+ 0)>>2];
        var sY0 = module.HEAP32[(d+ 4)>>2];
        var sX1 = module.HEAP32[(d+ 8)>>2];
        var sY1 = module.HEAP32[(d+12)>>2];
        var dX0 = module.HEAP32[(d+16)>>2];
        var dY0 = module.HEAP32[(d+20)>>2];
        var dX1 = module.HEAP32[(d+24)>>2];
        var dY1 = module.HEAP32[(d+28)>>2];
        var mk  = module.HEAP32[(d+32)>>2];
        var fr  = module.HEAP32[(d+36)>>2];

        gc.blitFramebuffer(
            sX0, sY0, sX1, sY1,
            dX0, dY0, dX1, dY1,
            mk, fr);
    },    
    ReadBuffer: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var ap = module.HEAP32[(d+ 0)>>2];

        gc.readBuffer(ap);

    },
    DrawBuffer: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var ap = module.HEAP32[(d+ 0)>>2];

        gc.drawBuffers([ap]);

    },
    DrawBuffers: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var si = module.HEAP32[(d+ 0)>>2];
        var ln = module.HEAP32[(d+ 4)>>2];
        var arr = module.HEAP32[(d+ 8)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);

        //debugger;
        var db = new Uint32Array(module.HEAPU8.buffer, arrPtr, ln);
        var ar = Array.prototype.slice.call(db);

        gc.drawBuffers(ar);

    },
    DrawRangeElements: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var md = module.HEAP32[(d+ 0)>>2];
        var st = module.HEAP32[(d+ 4)>>2];
        var en = module.HEAP32[(d+ 8)>>2];
        var ct = module.HEAP32[(d+12)>>2];
        var tp = module.HEAP32[(d+16)>>2];
        var of = module.HEAP32[(d+20)>>2];

        gc.drawRangeElements(md, st, en, ct, tp, of);
    },
    GetBufferSubData: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bt = module.HEAP32[(d+ 0)>>2];
        var of = module.HEAP32[(d+ 4)>>2];
        var st = module.HEAP32[(d+ 8)>>2];
        var arr = module.HEAP32[(d+12)>>2];
        var cn  = module.HEAP32[(d+16)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Uint8Array(module.HEAPU8.buffer, arrPtr, cn * st);

        gc.getBufferSubData(bt, of, dt);
    },
    GetBufferSubData1: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bt = module.HEAP32[(d+ 0)>>2];
        var of = module.HEAP32[(d+ 4)>>2];
        var si = module.HEAP32[(d+ 8)>>2];
        var st = module.HEAP32[(d+12)>>2];
        var arr = module.HEAP32[(d+16)>>2];
        var cn  = module.HEAP32[(d+20)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Uint8Array(module.HEAPU8.buffer, arrPtr, cn * st);

        gc.getBufferSubData(bt, of, dt, si*st);
    },
    GetBufferSubData2: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bt = module.HEAP32[(d+ 0)>>2];
        var of = module.HEAP32[(d+ 4)>>2];
        var si = module.HEAP32[(d+ 8)>>2];
        var ln = module.HEAP32[(d+12)>>2];
        var st = module.HEAP32[(d+16)>>2];
        var arr = module.HEAP32[(d+20)>>2];
        var cn  = module.HEAP32[(d+24)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var dt = new Uint8Array(module.HEAPU8.buffer, arrPtr, cn * st);

        gc.getBufferSubData(bt, of, dt, si*st, ln*st);
    },
    RenderbufferStorageMultisample: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var bt = module.HEAP32[(d+ 0)>>2];
        var sm = module.HEAP32[(d+ 4)>>2];
        var fm = module.HEAP32[(d+ 8)>>2];
        var w  = module.HEAP32[(d+12)>>2];
        var h  = module.HEAP32[(d+16)>>2];
        gc.renderbufferStorageMultisample(bt, sm, fm, w, h);
    },
    VertexAttribDivisor: function (uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var ix = module.HEAP32[(d + 0)>>2];
        var di = module.HEAP32[(d + 4)>>2];
        gc.vertexAttribDivisor(ix, di);
    },
    DrawElementsInstanced: function (uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var md = module.HEAP32[(d + 0)>>2];
        var ct = module.HEAP32[(d + 4)>>2];
        var tp = module.HEAP32[(d + 8)>>2];
        var of = module.HEAP32[(d +12)>>2];
        var ic = module.HEAP32[(d +16)>>2];
        gc.drawElementsInstanced(md, ct, tp, of, ic);
    },
    CreateQuery: function(uid)
    {
        var gc = nkJSObject.GetObject(uid);
        var q = gc.createQuery();
        return nkJSObject.RegisterObject(q);
    },
    DeleteQuery: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var quid = module.HEAP32[(d + 0)>>2];
        var q = nkJSObject.GetObject(quid);
        gc.deleteQuery(q);
    },
    IsQuery: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var quid = module.HEAP32[(d + 0)>>2];
        var q = nkJSObject.GetObject(quid);
        return gc.isQuery(q);
    },
    BeginQuery: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var t = module.HEAP32[(d + 0)>>2];
        var quid = module.HEAP32[(d + 4)>>2];
        var q = nkJSObject.GetObject(quid);
        gc.beginQuery(t, q);
    },
    EndQuery: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var t = module.HEAP32[(d + 0)>>2];
        gc.endQuery(t);
    },
    GetQuery: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var t = module.HEAP32[(d + 0)>>2];
        var p = module.HEAP32[(d + 4)>>2];
        var q = gc.getQuery(t, p);
        return (q?.nkUid != null) ? q.nkUid : -1;
    },
    GetQueryParameter: function(uid, d)
    {
        var module = Module;
        var gc = nkJSObject.GetObject(uid);
        var quid = module.HEAP32[(d + 0)>>2];
        var p = module.HEAP32[(d + 4)>>2];
        var q = nkJSObject.GetObject(quid);
        var r = gc.getQueryParameter(q, p);
        if (typeof r === 'boolean')
            r = r ? 1 : 0;
        return r;
    },
};

window.nkCanvasLoseContextExtension =
{
    LoseContext: function(uid, d)
    {
        var ex = nkJSObject.GetObject(uid);

        ex.loseContext();
    },
    RestoreContext: function(uid, d)
    {
        var ex = nkJSObject.GetObject(uid);

        ex.restoreContext();
    },
};
