using System;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Audio
{
    public class AudioWorkletNode : AudioNode
    {
        internal AudioWorkletNode(int uid, BaseAudioContext context) : base(uid, context)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            base.Dispose(disposing);
        }
    }
}
