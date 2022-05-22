using System;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Audio
{
    public class AudioBufferSourceNode : AudioScheduledSourceNode
    {

        internal AudioBufferSourceNode(int uid, BaseAudioContext context) : base(uid, context)
        {
        }

        public AudioBuffer Buffer
        {
            get { throw new NotImplementedException(); }
            set { Invoke("nkAudioBufferSourceNode.SetBuffer", value.Uid); }
        }

        public bool Loop
        {
            get { return InvokeRet<bool>("nkAudioBufferSourceNode.GetLoop"); }
            set { Invoke("nkAudioBufferSourceNode.SetLoop", value ? 1 : 0); }
        }

        protected override void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (disposing)
            {

            }

            base.Dispose(disposing);
        }
    }
}