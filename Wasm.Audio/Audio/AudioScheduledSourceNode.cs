using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Audio
{
    public class AudioScheduledSourceNode : AudioNode
    {
        public event EventHandler OnEnded;


        internal AudioScheduledSourceNode(int uid, BaseAudioContext context) : base(uid, context)
        {
            Invoke("nkAudioScheduledSourceNode.RegisterEvents");
        }

        [JSInvokable]
        public static void JsAudioScheduledSourceNodeOnEnded(int uid)
        {
            AudioScheduledSourceNode bufferSource = AudioScheduledSourceNode.FromUid<AudioScheduledSourceNode>(uid);
            if (bufferSource == null)
                return;

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
            if (disposing)
            {

            }

            Invoke("nkAudioScheduledSourceNode.UnregisterEvents");

            base.Dispose(disposing);
        }
    }
}