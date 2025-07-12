window.nkAudioContext =
{
    Create: function()
    {
        var ac = new AudioContext();         
        return nkJSObject.RegisterObject(ac);
    },
    Create1: function(sampleRate)
    {
        var options = {};
        if (sampleRate != 0)
            options.sampleRate = sampleRate;
        var ac = new AudioContext(options);
        return nkJSObject.RegisterObject(ac);
    },
    
    Close: function(uid,d)
    {
        var ac = nkJSObject.GetObject(uid);
        ac.close();
    },

    CreateMediaStreamSource: function (uid, d)
    {
        var ac = nkJSObject.GetObject(uid);
        var sid = Module.HEAP32[(d + 0) >> 2];
        var ms = nkJSObject.GetObject(sid);
        var sn = ac.createMediaStreamSource(ms);
        return nkJSObject.RegisterObject(sn);
    },
};

window.nkAudioBaseContext =
{
    GetSampleRate: function (uid, d)
    {
        var ac = nkJSObject.GetObject(uid);
        var sr = ac.sampleRate;
        return sr;
    },
    GetDestination: function (uid, d)
    {
        var ac = nkJSObject.GetObject(uid);
        var ds = ac.destination;        
        return  nkJSObject.RegisterObject(ds);
    },
    GetListener: function (uid, d)
    {
        var ac = nkJSObject.GetObject(uid);
        var lr = ac.listener;
        return  nkJSObject.RegisterObject(lr);
    },
    GetAudioWorklet: function (uid, d)
    {
        var ac = nkJSObject.GetObject(uid);

        var aw = ac.audioWorklet;

        var uid = nkJSObject.GetUid(aw);
        if (uid !== -1)
            return uid;

        return nkJSObject.RegisterObject(aw);
    },
    
    GetState: function (uid)
    {
        var ac = nkJSObject.GetObject(uid);
        switch (ac.state)
        {
            case "suspended": return 1;
            case "running": return 2;
            case "closed": return 3;
            default:
                throw new Error("Unknown AudioContext state: " + ac.state);
        }
    },

    CreateBuffer: function (uid, d)
    {
        var ac = nkJSObject.GetObject(uid);
        var nc = Module.HEAP32[(d+ 0)>>2];
        var le = Module.HEAP32[(d+ 4)>>2];
        var sr = Module.HEAP32[(d+ 8)>>2];
        var ab = ac.createBuffer(nc, le, sr);
        return  nkJSObject.RegisterObject(ab);
    },
    CreateBufferSource: function (uid, d)
    {
        var ac = nkJSObject.GetObject(uid);
        var bs = ac.createBufferSource();
        return nkJSObject.RegisterObject(bs);
    },
    CreateOscillator: function (uid, d)
    {
        var ac = nkJSObject.GetObject(uid);
        var os = ac.createOscillator();
        return nkJSObject.RegisterObject(os);
    },
    CreateGain: function (uid, d)
    {
        var ac = nkJSObject.GetObject(uid);
        var gn = ac.createGain();         
        return nkJSObject.RegisterObject(gn);
    },
    CreateStereoPanner: function (uid, d)
    {
        var ac = nkJSObject.GetObject(uid);
        var sp = ac.createStereoPanner();
        return nkJSObject.RegisterObject(sp);
    },
    CreateMediaElementSource: function (uid, d)
    {
        var ac = nkJSObject.GetObject(uid);
        var mid= Module.HEAP32[(d+ 0)>>2];
        var me = nkJSObject.GetObject(mid);
        var ms = ac.createMediaElementSource(me);
        return nkJSObject.RegisterObject(ms);
    },
    CreateWorklet: function (uid, d)
    {
        var ac = nkJSObject.GetObject(uid);
        var na = nkJSObject.ReadString(d + 0);

        var wn = new AudioWorkletNode(ac, na);

        return nkJSObject.RegisterObject(wn);
    }
};

window.nkAudioBuffer =
{
    CopyToChannel: function (uid, d)
    {
        var ab = nkJSObject.GetObject(uid);
        var cn = Module.HEAP32[(d+ 0)>>2];
        var arr = Module.HEAP32[(d+ 4)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var arrLen = Blazor.platform.getArrayLength(arr);
        var sr = new Float32Array(Module.HEAPU8.buffer, arrPtr, arrLen);

        ab.copyToChannel(sr, cn);
    },    
    GetSampleRate: function (uid, d)
    {
        var ab = nkJSObject.GetObject(uid);
        var sr = ab.sampleRate;
        return sr;
    },
    GetLength: function (uid, d)
    {
        var ab = nkJSObject.GetObject(uid);
        var ln = ab.length;
        return ln;
    },
    GetNumberOfChannels: function (uid, d)
    {
        var ab = nkJSObject.GetObject(uid);
        var nc = ab.numberOfChannels;
        return nc;
    }
};

window.nkAudioListener =
{
    SetPositionX: function (uid, d)
    {
        var lr = nkJSObject.GetObject(uid);
        var px = Module.HEAP32[(d+ 0)>>2];
        lr.positionX = px;
    },
    SetPositionY: function (uid, d)
    {
        var lr = nkJSObject.GetObject(uid);
        var py = Module.HEAP32[(d+ 0)>>2];
        lr.positionY = py;
    },
    SetPositionZ: function (uid, d)
    {
        var lr = nkJSObject.GetObject(uid);
        var pz = Module.HEAP32[(d+ 0)>>2];
        lr.positionZ = pz;
    },

    SetForwardX: function (uid, d)
    {
        var lr = nkJSObject.GetObject(uid);
        var fx = Module.HEAP32[(d+ 0)>>2];
        lr.forwardX = fx;
    },
    SetForwardY: function (uid, d)
    {
        var lr = nkJSObject.GetObject(uid);
        var fy = Module.HEAP32[(d+ 0)>>2];
        lr.forwardY = fy;
    },
    SetForwardZ: function (uid, d)
    {
        var lr = nkJSObject.GetObject(uid);
        var fz = Module.HEAP32[(d+ 0)>>2];
        lr.forwardZ = fz;
    },

    SetUpX: function (uid, d)
    {
        var lr = nkJSObject.GetObject(uid);
        var ux = Module.HEAP32[(d+ 0)>>2];
        lr.upX = ux;
    },
    SetUpY: function (uid, d)
    {
        var lr = nkJSObject.GetObject(uid);
        var uy = Module.HEAP32[(d+ 0)>>2];
        lr.upY = uy;
    },
    SetUpZ: function (uid, d)
    {
        var lr = nkJSObject.GetObject(uid);
        var uz = Module.HEAP32[(d+ 0)>>2];
        lr.upZ = uz;
    }
};

window.nkAudioBufferSourceNode =
{
    SetBuffer: function (uid, d)
    {
        var bs = nkJSObject.GetObject(uid);
        var bid= Module.HEAP32[(d+ 0)>>2];
        var ab = nkJSObject.GetObject(bid);
        bs.buffer = ab;
    },

    GetLoop: function (uid, d)
    {
        var bs = nkJSObject.GetObject(uid);
        return bs.loop;
    },
    SetLoop: function (uid, d)
    {
        var bs = nkJSObject.GetObject(uid);
        var lp = Module.HEAP32[(d+ 0)>>2];
        bs.loop = lp !== 0;
    },
    GetPlaybackRate: function (uid, d)
    {
        var bs = nkJSObject.GetObject(uid);
        var pr = bs.playbackRate;
        return nkJSObject.RegisterObject(pr);
    }
};

window.nkAudioScheduledSourceNode =
{
    Start: function (uid, d)
    {
        var bs = nkJSObject.GetObject(uid);
        bs.start();
    },
    Stop: function (uid, d)
    {
        var bs = nkJSObject.GetObject(uid);
        bs.stop();
    },
    
    RegisterEvents: function (uid)
    {
        var bs = nkJSObject.GetObject(uid);

        bs.onended = function(event)
        {
            DotNet.invokeMethod('nkast.Wasm.Audio', 'JsAudioScheduledSourceNodeOnEnded', uid);
        };
    },    
    UnregisterEvents: function (uid)
    {
        var bs = nkJSObject.GetObject(uid);
        bs.onended = null;
    }
};

window.nkAudioDestinationNode =
{
    GetMaxChannelCount: function (uid, d)
    {
        var gn = nkJSObject.GetObject(uid);
        return gn.maxChannelCount;
    }
};

window.nkAudioNode =
{
    GetNumberOfInputs: function (uid, d)
    {
        var an = nkJSObject.GetObject(uid);
        return an.numberOfInputs;
    },
    GetNumberOfOutputs: function (uid, d)
    {
        var an = nkJSObject.GetObject(uid);
        return an.numberOfOutputs;
    },
    GetChannelCount: function (uid, d)
    {
        var an = nkJSObject.GetObject(uid);
        return an.channelCount;
    },
    GetChannelCountMode: function (uid, d)
    {
        var an = nkJSObject.GetObject(uid);
        switch (an.channelCountMode)
        {
            case "max": return 1;
            case "clamped-max": return 2;
            case "explicit": return 3;
            default:
                throw new Error("Unknown channelCountMode: " + an.channelCountMode);
        }
    },
    Connect: function (uid, d)
    {
        var an = nkJSObject.GetObject(uid);
        var did= Module.HEAP32[(d+ 0)>>2];
        var ds = nkJSObject.GetObject(did);
        an.connect(ds);
    },
    Disconnect: function (uid, d)
    {
        var an = nkJSObject.GetObject(uid);
        an.disconnect();
    },
    Disconnect1: function (uid, d)
    {
        var an = nkJSObject.GetObject(uid);
        var did = Module.HEAP32[(d+ 0)>>2];
        var ds = nkJSObject.GetObject(did);
        an.disconnect(ds);
    },
};

window.nkAudioOscillatorNode =
{
    GetFrequency: function (uid, d)
    {
        var os = nkJSObject.GetObject(uid);
        var ap = os.frequency;
        return  nkJSObject.RegisterObject(ap);
    },

    GetType: function (uid)
    {
        var os = nkJSObject.GetObject(uid);
        switch (os.type)
        {
            case "sine": return 1;
            case "square": return 2;
            case "sawtooth": return 3;
            case "triangle": return 4;
            case "custom": return 5;
            default:
                throw new Error("Unknown OscillatorNode type: " + os.type);
        }
    },
    SetType: function (uid, d)
    {
        var os = nkJSObject.GetObject(uid);
        var ty = Module.HEAP32[(d+ 0)>>2];
        switch (ty)
        {
            case 1: os.type = "sine"; break;
            case 2: os.type = "square"; break;
            case 3: os.type = "sawtooth"; break;
            case 4: os.type = "triangle"; break;
            case 5:
                throw new Error("You never set type to custom manually; instead, use the setPeriodicWave().");
            default:
                throw new Error("Unknown OscillatorNode type: " + ty);
        }
    }
};

window.nkAudioGainNode =
{
    GetGain: function (uid, d)
    {
        var gn = nkJSObject.GetObject(uid);
        var ap = gn.gain;
        return nkJSObject.RegisterObject(ap);
    }
};

window.nkAudioStereoPannerNode =
{
    GetPan: function (uid, d)
    {
        var sp = nkJSObject.GetObject(uid);
        var ap = sp.pan;
        return nkJSObject.RegisterObject(ap);
    }
};

window.nkAudioWorkletNode =
{
    GetPort: function (uid, d)
    {
        var wn = nkJSObject.GetObject(uid);

        var po = wn.port;

        var uid = nkJSObject.GetUid(po);
        if (uid !== -1)
            return uid;

        return nkJSObject.RegisterObject(po);
    },
    GetParameters: function (uid, d)
    {
        var wn = nkJSObject.GetObject(uid);

        var pa = wn.parameters;

        var uid = nkJSObject.GetUid(pa);
        if (uid !== -1)
            return uid;

        return nkJSObject.RegisterObject(pa);
    }
};

window.nkAudioParam =
{  
    GetDefaultValue: function (uid, d)
    {
        var ap = nkJSObject.GetObject(uid);
        return ap.defaultValue;
    },
    GetMinValue: function (uid, d)
    {
        var ap = nkJSObject.GetObject(uid);
        return ap.minValue;
    },
    GetMaxValue: function (uid, d)
    {
        var ap = nkJSObject.GetObject(uid);
        return ap.maxValue;
    },

    GetValue: function (uid, d)
    {
        var ap = nkJSObject.GetObject(uid);
        return ap.value;
    },
    SetValue: function (uid, d)
    {
        var ap = nkJSObject.GetObject(uid);
        var vl = Module.HEAPF32[(d+ 0)>>2];
        ap.value = vl;
    },
    
    SetValueAtTime: function (uid, d)
    {
        var ap = nkJSObject.GetObject(uid);
        var vl = Module.HEAPF32[(d+ 0)>>2];
        var st = Module.HEAPF32[(d+ 4)>>2];
        ap.setValueAtTime(vl, st);
    },
    LinearRampToValueAtTime: function (uid, d)
    {
        var ap = nkJSObject.GetObject(uid);
        var vl = Module.HEAPF32[(d+ 0)>>2];
        var et = Module.HEAPF32[(d+ 4)>>2];
        ap.linearRampToValueAtTime(vl, et);
    },
    ExponentialRampToValueAtTime: function (uid, d)
    {
        var ap = nkJSObject.GetObject(uid);
        var vl = Module.HEAPF32[(d+ 0)>>2];
        var et = Module.HEAPF32[(d+ 4)>>2];
        ap.exponentialRampToValueAtTime(vl, et);
    },
    SetTargetAtTime: function (uid, d)
    {
        var ap = nkJSObject.GetObject(uid);
        var tg = Module.HEAPF32[(d+ 0)>>2];
        var st = Module.HEAPF32[(d+ 4)>>2];
        var tc = Module.HEAPF32[(d+ 8)>>2];
        ap.setTargetAtTime(tg, st, tc);
    },
    SetValueCurveAtTime: function (uid, d)
    {
        var ap = nkJSObject.GetObject(uid);
        var st = Module.HEAPF32[(d+ 0)>>2];
        var dt = Module.HEAPF32[(d+ 4)>>2];
        var arr = Module.HEAP32[(d+ 8)>>2];

        var arrPtr = Blazor.platform.getArrayEntryPtr(arr, 0, 4);
        var arrLen = Blazor.platform.getArrayLength(arr);
        var vs = new Float32Array(Module.HEAPU8.buffer, arrPtr, arrLen);

        ap.setValueCurveAtTime(vs, st, dt);
    },

    CancelScheduledValues: function (uid, d)
    {
        var ap = nkJSObject.GetObject(uid);
        var st = Module.HEAPF32[(d+ 0)>>2];
        ap.cancelScheduledValues(st);
    }
};

window.nkAudioParamMap =
{
    GetSize: function (uid, d)
    {
        var pm = nkJSObject.GetObject(uid);
        return pm.size;
    },
    Get: function (uid, d)
    {
        var pm = nkJSObject.GetObject(uid);
        var ky = nkJSObject.ReadString(d + 0);

        var ap = pm.get(ky);

        var uid = nkJSObject.GetUid(ap);
        if (uid !== -1)
            return uid;

        return nkJSObject.RegisterObject(ap);
    },
};

window.nkAudioWorklet =
{
    AddModule: function (uid, d)
    {
        var aw = nkJSObject.GetObject(uid);
        var mu = nkJSObject.ReadString(d + 0);

        var pr = aw.addModule(mu);
        return nkJSObject.RegisterObject(pr);
    },
};

window.nkMediaDevices =
{
    Create: function (uid)
    {
        var nv = nkJSObject.GetObject(uid);
        if ("mediaDevices" in nv)
        {
            var md = nv.mediaDevices;
            var uid = nkJSObject.GetUid(md);
            if (uid !== -1)
                return uid;

            return nkJSObject.RegisterObject(md);
        }
        else
            return nkJSObject.RegisterObject(null);
    },

    GetUserMedia: function (uid, d)
    {
        var md = nkJSObject.GetObject(uid);
        var bi = Module.HEAP32[(d+ 0)>>2];

        var au = (bi >>  0) & 3;
        var vi = (bi >>  2) & 3;

        var constraints = {};
        if (au != 3)
            constraints.audio = au !== 0;
        if (vi != 3)
            constraints.video = vi !== 0;

        var pr = md.getUserMedia(constraints);
        return nkJSObject.RegisterObject(pr);
    },
};
