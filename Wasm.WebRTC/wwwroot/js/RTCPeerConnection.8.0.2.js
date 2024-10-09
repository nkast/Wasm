window.nkRTCPeerConnection =
{
    Create: function () {
        var pc = new RTCPeerConnection();
        return nkJSObject.RegisterObject(pc);
    },

    CreateDataChannel: function (uid, d) {
        var bs = nkJSObject.GetObject(uid);
        return bs.createDataChannel(d);
    },
    RegisterEvents: function (uid) {
        var bs = nkJSObject.GetObject(uid);

        bs.ondatachannel = function (event) {
            DotNet.invokeMethod('nkast.Wasm.WebRTC', 'JsHandleOnDataChannel', uid);
        };
    },
    UnregisterEvents: function (uid) {
        var bs = nkJSObject.GetObject(uid);
        bs.ondatachannel = null;
    }
}