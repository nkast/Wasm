using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioDestinationNode : AudioNode
    {
        internal AudioDestinationNode(int uid, BaseAudioContext context) : base(uid, context)
        {
        }

        public int MaxChannelCount
        {            
            get { return InvokeRetInt(RegisterFunction("nkAudioDestinationNode.GetMaxChannelCount")); }
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
