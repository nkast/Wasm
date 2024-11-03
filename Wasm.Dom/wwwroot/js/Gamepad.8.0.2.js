window.nkGamepad =
{
    GetVibrationActuator: function(uid)
    {
        var gp = nkJSObject.GetObject(uid);

        if ('vibrationActuator' in gp)
        {
            var ha = gp.vibrationActuator;

            var uid = nkJSObject.GetUid(ha);
            if (uid !== -1)
                return uid;

            return nkJSObject.RegisterObject(ha);
        }
        else 
        return -1;
    },
    GetId: function (uid)
    {
        var gp = nkJSObject.GetObject(uid);
        return BINDING.js_to_mono_obj(gp.id);
    },
    GetIndex: function (uid, d)
    {
        var gp = nkJSObject.GetObject(uid);
        return gp.index;
    },
    GetConnected: function (uid, d)
    {
        var gp = nkJSObject.GetObject(uid);
        return gp.connected;
    },
    GetMapping: function (uid, d)
    {
        var gp = nkJSObject.GetObject(uid);
        return BINDING.js_to_mono_obj(gp.mapping);
    },
    GetTimestamp: function (uid, d)
    {
        var gp = nkJSObject.GetObject(uid);
        return Math.floor(gp.timestamp*1000);
    },
    GetButtons: function (uid, d)
    {
        var gp = nkJSObject.GetObject(uid);
        var buttons = gp.buttons;
        var btns = [];
        for (var i = 0; i < buttons.length; i++)
        {
            btns[i*3+0] = buttons[i].value;
            btns[i*3+1] = (buttons[i].pressed) ? 1 : 0;
            if (buttons[i].touched2 == undefined)
                btns[i*3+2] = 0;
            else
                btns[i*3+2] = (buttons[i].touched) ? 1 : 0;
        }
        var str = btns.toString();

        return BINDING.js_to_mono_obj(str);
    },
    GetAxes: function (uid, d)
    {
        var gp = nkJSObject.GetObject(uid);
        var axes = gp.axes;
        var str = axes.toString();

        return BINDING.js_to_mono_obj(str);
    }
};

window.nkGamepadHapticActuator =
{
    PlayEffect: function (uid, d)
    {
        var ha = nkJSObject.GetObject(uid);
        var sd = Module.HEAPF32[(d+ 0)>>2];
        var du = Module.HEAPF32[(d+ 4)>>2];
        var sm = Module.HEAPF32[(d+ 8)>>2];
        var wm = Module.HEAPF32[(d+12)>>2];
        var lt = Module.HEAPF32[(d+16)>>2];
        var rt = Module.HEAPF32[(d+20)>>2];

        if ('playEffect' in ha)
        {
            ha.playEffect("dual-rumble",
            {
                startDelay: sd,
                duration: du,
                strongMagnitude: sm,
                weakMagnitude: wm,
                leftTrigger: lt,
                rightTrigger: rt
            });
            return true;
        }
        return false;
    },
    Pulse: function (uid, d)
    {
        var ha = nkJSObject.GetObject(uid);
        var va = Module.HEAPF32[(d+ 0)>>2];
        var du = Module.HEAPF32[(d+ 4)>>2];

        if ('pulse' in ha)
        {
            ha.pulse(va, du);
            return true;
        }
        return false;
    },
    Reset: function (uid, d)
    {
        var ha = nkJSObject.GetObject(uid);

        if ('reset' in ha)
        {
            ha.reset();
            return true;
        }
        return false;
    },
};


