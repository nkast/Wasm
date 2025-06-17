using System;

namespace nkast.Wasm.Audio
{
    public class MediaStreamSourceNode : AudioNode
    {

        internal MediaStreamSourceNode(int uid, BaseAudioContext context) : base(uid, context)
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
