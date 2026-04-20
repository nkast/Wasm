window.nkCanvas2dContext =
{
    GetFillStyle: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.fillStyle;
    },
    SetFillStyle: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        dc.fillStyle = nkJSObject.ReadString(module, d+ 0);
    },

    GetStrokeStyle: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.strokeStyle;
    },
    SetStrokeStyle: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        dc.strokeStyle = nkJSObject.ReadString(module, d+ 0);
    },

    GetFont: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.font;
    },
    SetFont: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        dc.font = nkJSObject.ReadString(module, d+ 0);
    },

    GetTextAlign: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.textAlign;
    },
    SetTextAlign: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        dc.textAlign = nkJSObject.ReadString(module, d+ 0);
    },

    GetTextBaseline: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.textBaseline;
    },
    SetTextBaseline: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        dc.textBaseline = nkJSObject.ReadString(module, d+ 0);
    },

    GetLineWidth: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.lineWidth;
    },
    SetLineWidth: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        dc.lineWidth = module.HEAPF32[(d+ 0)>>2];
    },

    GetLineCap: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.lineCap;
    },
    SetLineCap: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        dc.lineCap = nkJSObject.ReadString(module, d+ 0);
    },

    GetMiterLimit: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.miterLimit;
    },
    SetMiterLimit: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        dc.miterLimit = module.HEAPF32[(d+ 0)>>2];
    },

    GetGlobalAlpha: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.globalAlpha;
    },
    SetGlobalAlpha: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        dc.globalAlpha = module.HEAPF32[(d+ 0)>>2];
    },

    GetGlobalCompositeOperation: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.globalCompositeOperation;
    },
    SetGlobalCompositeOperation: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        dc.globalCompositeOperation = nkJSObject.ReadString(module, d+ 0);
    },

    GetImageSmoothingEnabled: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.imageSmoothingEnabled;
    },
    SetImageSmoothingEnabled: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var s = module.HEAP32[(d+ 0)>>2];
        dc.imageSmoothingEnabled = s !== 0;
    },

    GetShadowBlur: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.shadowBlur;
    },
    SetShadowBlur: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        dc.shadowBlur = module.HEAPF32[(d+ 0)>>2];
    },

    GetShadowColor: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.shadowColor;
    },
    SetShadowColor: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        dc.shadowColor = nkJSObject.ReadString(module, d+ 0);
    },

    GetShadowOffsetX: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.shadowOffsetX;
    },
    SetShadowOffsetX: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        dc.shadowOffsetX = module.HEAPF32[(d+ 0)>>2];
    },

    GetShadowOffsetY: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        return dc.shadowOffsetY;
    },
    SetShadowOffsetY: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        dc.shadowOffsetY = module.HEAPF32[(d+ 0)>>2];
    },

    ClearRect: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var x = module.HEAPF32[(d+ 0)>>2];
        var y = module.HEAPF32[(d+ 4)>>2];
        var w = module.HEAPF32[(d+ 8)>>2];
        var h = module.HEAPF32[(d+12)>>2];
        dc.clearRect(x, y, w, h);
    },
    FillRect: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var x = module.HEAPF32[(d+ 0)>>2];
        var y = module.HEAPF32[(d+ 4)>>2];
        var w = module.HEAPF32[(d+ 8)>>2];
        var h = module.HEAPF32[(d+12)>>2];
        dc.fillRect(x, y, w, h);
    },
    StrokeRect: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var x = module.HEAPF32[(d+ 0)>>2];
        var y = module.HEAPF32[(d+ 4)>>2];
        var w = module.HEAPF32[(d+ 8)>>2];
        var h = module.HEAPF32[(d+12)>>2];
        dc.strokeRect(x, y, w, h);
    },

    DrawImage: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var id = nkJSObject.ReadString(module, d+ 0);
        var dx = module.HEAPF32[(d+ 4)>>2];
        var dy = module.HEAPF32[(d+ 8)>>2];
        var img = document.getElementById(id);
        dc.drawImage(img, dx, dy);
    },

    DrawImage1: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var id = nkJSObject.ReadString(module, d+ 0);
        var dx = module.HEAPF32[(d+ 4)>>2];
        var dy = module.HEAPF32[(d+ 8)>>2];
        var dw = module.HEAPF32[(d+12)>>2];
        var dh = module.HEAPF32[(d+16)>>2];
        var img = document.getElementById(id);
        dc.drawImage(img, dx, dy, dw, dh);
    },

    DrawImage2: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var id = nkJSObject.ReadString(module, d+ 0);
        var sx = module.HEAPF32[(d+ 4)>>2];
        var sy = module.HEAPF32[(d+ 8)>>2];
        var sw = module.HEAPF32[(d+12)>>2];
        var sh = module.HEAPF32[(d+16)>>2];
        var dx = module.HEAPF32[(d+20)>>2];
        var dy = module.HEAPF32[(d+24)>>2];
        var dw = module.HEAPF32[(d+28)>>2];
        var dh = module.HEAPF32[(d+32)>>2];
        var img = document.getElementById(id);
        dc.drawImage(img, sx, sy, sw, sh, dx, dy, dw, dh);
    },

    FillText: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var t = nkJSObject.ReadString(module, d+ 0);
        var x = module.HEAPF32[(d+ 4)>>2];
        var y = module.HEAPF32[(d+ 8)>>2];
        dc.fillText(t, x, y);
    },
    FillText1: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var t = nkJSObject.ReadString(module, d+ 0);
        var x = module.HEAPF32[(d+ 4)>>2];
        var y = module.HEAPF32[(d+ 8)>>2];
        var w = module.HEAPF32[(d+12)>>2];
        dc.fillText(t, x, y, w);
    },
    StrokeText: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var t = nkJSObject.ReadString(module, d+ 0);
        var x = module.HEAPF32[(d+ 4)>>2];
        var y = module.HEAPF32[(d+ 8)>>2];
        dc.strokeText(t, x, y);
    },
    StrokeText1: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var t = nkJSObject.ReadString(module, d+ 0);
        var x = module.HEAPF32[(d+ 4)>>2];
        var y = module.HEAPF32[(d+ 8)>>2];
        var w = module.HEAPF32[(d+12)>>2];
        dc.strokeText(t, x, y, w);
    },
    MeasureText: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var t = nkJSObject.ReadString(module, d+ 0);
        var m = dc.measureText(t);
        return m.width;
    },

    GetLineDash: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        var ds = dc.getLineDash();
        return ds.toString();
    },
    SetLineDash: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var arr = module.HEAP32[(d+ 0)>>2];
        var cn  = module.HEAP32[(d+ 4)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);

        var ja = [];
        for (var ind = 0; ind < cn; ind++)
        {
            var aiv = module.HEAPF32[((arrPtr+0)>>2)+ind];
            ja.push(aiv);
        }
        dc.setLineDash(ja);
    },

    BeginPath: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        dc.beginPath();
    },
    ClosePath: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        dc.closePath();
    },

    IsPointInPath: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var x = module.HEAPF32[(d+ 0)>>2];
        var y = module.HEAPF32[(d+ 4)>>2];
        var e = module.HEAP32[(d+ 8)>>2];
        return dc.isPointInPath(x, y, e === 0 ? "nonzero" : "evenodd");
    },
    IsPointInStroke: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var x = module.HEAPF32[(d+ 0)>>2];
        var y = module.HEAPF32[(d+ 4)>>2];
        return dc.isPointInStroke(x, y);
    },

    MoveTo: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var x = module.HEAPF32[(d+ 0)>>2];
        var y = module.HEAPF32[(d+ 4)>>2];
        dc.moveTo(x, y);
    },
    LineTo: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var x = module.HEAPF32[(d+ 0)>>2];
        var y = module.HEAPF32[(d+ 4)>>2];
        dc.lineTo(x, y);
    },
    BezierCurveTo: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var cp1X = module.HEAPF32[(d+ 0)>>2];
        var cp1Y = module.HEAPF32[(d+ 4)>>2];
        var cp2X = module.HEAPF32[(d+ 8)>>2];
        var cp2Y = module.HEAPF32[(d+12)>>2];
        var x    = module.HEAPF32[(d+16)>>2];
        var y    = module.HEAPF32[(d+20)>>2];
        dc.bezierCurveTo(cp1X, cp1Y, cp2X, cp2Y, x, y);
    },
    QuadraticCurveTo: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var cpX = module.HEAPF32[(d+ 0)>>2];
        var cpY = module.HEAPF32[(d+ 4)>>2];
        var x = module.HEAPF32[(d+ 8)>>2];
        var y = module.HEAPF32[(d+12)>>2];
        dc.quadraticCurveTo(cpX, cpY, x, y);
    },
    Arc: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var x = module.HEAPF32[(d+ 0)>>2];
        var y = module.HEAPF32[(d+ 4)>>2];
        var r = module.HEAPF32[(d+ 8)>>2];
        var s = module.HEAPF32[(d+12)>>2];
        var e = module.HEAPF32[(d+16)>>2];
        var a = module.HEAP32[(d+20)>>2];
        dc.arc(x, y, r, s, e, a !== 0);
    },
    ArcTo: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var x1 = module.HEAPF32[(d+ 0)>>2];
        var y1 = module.HEAPF32[(d+ 4)>>2];
        var x2 = module.HEAPF32[(d+ 8)>>2];
        var y2 = module.HEAPF32[(d+12)>>2];
        var r  = module.HEAPF32[(d+16)>>2];
        dc.arcTo(x1, y1, x2, y2, r);
    },
    Rect: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var x = module.HEAPF32[(d+ 0)>>2];
        var y = module.HEAPF32[(d+ 4)>>2];
        var w = module.HEAPF32[(d+ 8)>>2];
        var h = module.HEAPF32[(d+12)>>2];
        dc.rect(x, y, w, h);
    },
    Ellipse: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var x  = module.HEAPF32[(d+ 0)>>2];
        var y  = module.HEAPF32[(d+ 4)>>2];
        var rx = module.HEAPF32[(d+ 8)>>2];
        var ry = module.HEAPF32[(d+12)>>2];
        var ro = module.HEAPF32[(d+16)>>2];
        var s  = module.HEAPF32[(d+20)>>2];
        var e  = module.HEAPF32[(d+24)>>2];
        var a  = module.HEAP32[(d+28)>>2];
        dc.ellipse(x, y, rx, ry, ro, s, e, a !== 0);
    },

    Fill: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        dc.fill();
    },
    Stroke: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        dc.stroke();
    },
    Clip: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        dc.clip();
    },

    Rotate: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        dc.rotate(module.HEAPF32[(d+ 0)>>2]);
    },
    Scale: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var x = module.HEAPF32[(d+ 0)>>2];
        var y = module.HEAPF32[(d+ 4)>>2];
        dc.scale(x, y);
    },
    Translate: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var x = module.HEAPF32[(d+ 0)>>2];
        var y = module.HEAPF32[(d+ 4)>>2];
        dc.translate(x, y);
    },
    Transform: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var m11 = module.HEAPF32[(d+ 0)>>2];
        var m12 = module.HEAPF32[(d+ 4)>>2];
        var m21 = module.HEAPF32[(d+ 8)>>2];
        var m22 = module.HEAPF32[(d+12)>>2];
        var dx  = module.HEAPF32[(d+16)>>2];
        var dy  = module.HEAPF32[(d+20)>>2];
        dc.transform(m11, m12, m21, m22, dx, dy);
    },
    SetTransform: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var m11 = module.HEAPF32[(d+ 0)>>2];
        var m12 = module.HEAPF32[(d+ 4)>>2];
        var m21 = module.HEAPF32[(d+ 8)>>2];
        var m22 = module.HEAPF32[(d+12)>>2];
        var dx  = module.HEAPF32[(d+16)>>2];
        var dy  = module.HEAPF32[(d+20)>>2];
        dc.setTransform(m11, m12, m21, m22, dx, dy);
    },
    GetTransform: function(uid, d)
    {
        var module = Module;
        var dc = nkJSObject.GetObject(uid);
        var pt = module.HEAP32[(d+ 0)>>2];

        var mx = dc.getTransform();

        module.HEAPF32[(pt+ 0)>>2] = mx.m11;
        module.HEAPF32[(pt+ 4)>>2] = mx.m12;
        module.HEAPF32[(pt+ 8)>>2] = 0;
        module.HEAPF32[(pt+12)>>2] = 0;

        module.HEAPF32[(pt+16)>>2] = mx.m21;
        module.HEAPF32[(pt+20)>>2] = mx.m22;
        module.HEAPF32[(pt+24)>>2] = 0;
        module.HEAPF32[(pt+28)>>2] = 0;

        module.HEAPF32[(pt+32)>>2] = 0;
        module.HEAPF32[(pt+36)>>2] = 0;
        module.HEAPF32[(pt+40)>>2] = 1;
        module.HEAPF32[(pt+44)>>2] = 0;

        module.HEAPF32[(pt+48)>>2] = mx.m41;
        module.HEAPF32[(pt+52)>>2] = mx.m42;
        module.HEAPF32[(pt+56)>>2] = 0;
        module.HEAPF32[(pt+60)>>2] = 1;

        return;
    },

    Save: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        dc.save();
    },
    Restore: function(uid)
    {
        var dc = nkJSObject.GetObject(uid);
        dc.restore();
    }
};