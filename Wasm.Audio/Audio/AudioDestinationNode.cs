using System;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Audio
{
    public class AudioDestinationNode : AudioNode
    {
        internal AudioDestinationNode(int uid, BaseAudioContext context) : base(uid, context)
        {
        }

        public int MaxChannelCount
        {            
            get { return InvokeRet<int>("nkAudioDestinationNode.GetMaxChannelCount"); }
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
