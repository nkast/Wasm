using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioDestinationNode : AudioNode
    {
        private readonly int _fid_GetMaxChannelCount;

        internal AudioDestinationNode(int uid, BaseAudioContext context) : base(uid, context)
        {
            _fid_GetMaxChannelCount = RegisterFunction("nkAudioDestinationNode.GetMaxChannelCount");
        }

        public int MaxChannelCount
        {            
            get { return InvokeRetInt(_fid_GetMaxChannelCount); }
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
