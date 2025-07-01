using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioNode : CachedJSObject<AudioNode>
    {
        BaseAudioContext _context;

        protected BaseAudioContext Context { get { return _context; } }

        public int NumberOfInputs
        {
            get { return InvokeRetInt("nkAudioNode.GetNumberOfInputs"); }
        }
        public int NumberOfOutputs
        {
            get { return InvokeRetInt("nkAudioNode.GetNumberOfOutputs"); }
        }
        public int ChannelCount
        {
            get { return InvokeRetInt("nkAudioNode.GetChannelCount"); }
        }
        public ChannelCountMode ChannelCountMode
        {
            get { return (ChannelCountMode)InvokeRetInt("nkAudioNode.GetChannelCountMode"); }
        }


        internal AudioNode(int uid, BaseAudioContext context) : base(uid)
        {
            _context = context;
        }

        public void Connect(AudioNode destination)
        {
            Invoke("nkAudioNode.Connect", destination.Uid);
        }

        public void Disconnect(AudioNode destination)
        {
            Invoke("nkAudioNode.Disconnect", destination.Uid);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _context = null;

            base.Dispose(disposing);
        }
    }
}