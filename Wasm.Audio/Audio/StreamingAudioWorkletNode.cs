using Microsoft.JSInterop;
using nkast.Wasm.Dom;
using System;
using System.Collections.Generic;

namespace nkast.Wasm.Audio
{
    public partial class StreamingAudioWorkletNode : AudioNode
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        internal StreamingAudioWorkletNode(int uid, BaseAudioContext context) : base(uid, context)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this));
        }

        [JSInvokable]
        public static void JsStreamingAudioWorkletNodeBufferConsumed(int uid, int remaining)
        {
            if (!_uidMap.TryGetValue(uid, out var jsObjRef))
            {
                return;
            }
            if (!jsObjRef.TryGetTarget(out var jsObj))
            {
                return;
            }

            StreamingAudioWorkletNode node = (StreamingAudioWorkletNode)jsObj;
            node.QueuedBufferCount = remaining;

            return;
        }

        public void SubmitBuffer(float[] buffer)
        {
            this.QueuedBufferCount++;
            Invoke("nkDynamicSoundEffectNode.SubmitBuffer", buffer);
        }

        public void ClearBuffers()
        {
            this.QueuedBufferCount = 0;
            Invoke("nkDynamicSoundEffectNode.ClearBuffers");
        }

        public void Stop()
        {
            this.QueuedBufferCount = 0;
            Invoke("nkDynamicSoundEffectNode.Stop");
        }

        public int QueuedBufferCount
        {
            get;
            private set;
        }

        protected override void Dispose(bool disposing)
        {
            _uidMap.Remove(this.Uid);

            if (disposing)
            {

            }

            base.Dispose(disposing);
        }
    }
}