window.nkRTCDataChannel =
{
    RegisterEvents: function (uid) {
        var bs = nkJSObject.GetObject(uid);

        bs.onopen = function (event) {
            DotNet.invokeMethod('nkast.Wasm.WebRTC', 'JsRTCDataChannelOnOpen', uid);
        };
    },
    UnregisterEvents: function (uid) {
        var bs = nkJSObject.GetObject(uid);
        bs.onopen = null;
    }
}