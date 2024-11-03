using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Audio
{
    public class AudioScheduledSourceNode : AudioNode
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        public event EventHandler OnEnded;


        internal AudioScheduledSourceNode(int uid, BaseAudioContext context) : base(uid, context)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this, true));
            Invoke("nkAudioScheduledSourceNode.RegisterEvents");
        }

        [JSInvokable]
        public static void JsAudioScheduledSourceNodeOnEnded(int uid)
        {
            AudioScheduledSourceNode bufferSource = FromUid(uid);
            if (bufferSource == null)
                return;

            var handler = bufferSource.OnEnded;
            if (handler != null)
                handler(bufferSource, EventArgs.Empty);
        }

        private static AudioScheduledSourceNode FromUid(int uid)
        {
            if (_uidMap.TryGetValue(uid, out var jsObjRef))
                if (_uidMap[uid].TryGetTarget(out var jsObj))
                    return (AudioScheduledSourceNode)jsObj;

            return null;
        }

        public void Start()
        {
            Invoke("nkAudioScheduledSourceNode.Start");
        }

        public void Stop()
        {
            Invoke("nkAudioScheduledSourceNode.Stop");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Invoke("nkAudioScheduledSourceNode.UnregisterEvents");
            _uidMap.Remove(Uid);

            base.Dispose(disposing);
        }
    }
}