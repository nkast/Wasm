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
            _uidMap.Add(Uid, new WeakReference<JSObject>(this));
            Invoke("nkAudioScheduledSourceNode.RegisterEvents");
        }

        [JSInvokable]
        public static void JsAudioScheduledSourceNodeOnEnded(int uid)
        {
            if (!_uidMap.TryGetValue(uid, out var jsObjRef))
                return;
            if (!_uidMap[uid].TryGetTarget(out var jsObj))
                return;

            AudioScheduledSourceNode bufferSource = (AudioScheduledSourceNode)jsObj;

            var handler = bufferSource.OnEnded;
            if (handler != null)
                handler(bufferSource, EventArgs.Empty);
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
            if (IsDisposed)
                return;

            if (disposing)
            {

            }

            Invoke("nkAudioScheduledSourceNode.UnregisterEvents");
            _uidMap.Remove(Uid);

            base.Dispose(disposing);
        }
    }
}