using System;
using System.Collections.Generic;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioScheduledSourceNode : AudioNode
    {
        private readonly int _fid_RegisterEvents;
        private readonly int _fid_Start;
        private readonly int _fid_Stop;
        private readonly int _fid_UnregisterEvents;

        public event EventHandler OnEnded;


        internal AudioScheduledSourceNode(int uid, BaseAudioContext context) : base(uid, context)
        {
            _fid_RegisterEvents = RegisterFunction("nkAudioScheduledSourceNode.RegisterEvents");
            _fid_Start = RegisterFunction("nkAudioScheduledSourceNode.Start");
            _fid_Stop = RegisterFunction("nkAudioScheduledSourceNode.Stop");
            _fid_UnregisterEvents = RegisterFunction("nkAudioScheduledSourceNode.UnregisterEvents");
            Invoke(_fid_RegisterEvents);
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
            Invoke(_fid_Start);
        }

        public void Stop()
        {
            Invoke(_fid_Stop);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Invoke(_fid_UnregisterEvents);

            base.Dispose(disposing);
        }
    }
}