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
            get { return InvokeRetInt(RegisterFunction("nkAudioNode.GetNumberOfInputs")); }
        }
        public int NumberOfOutputs
        {
            get { return InvokeRetInt(RegisterFunction("nkAudioNode.GetNumberOfOutputs")); }
        }
        public int ChannelCount
        {
            get { return InvokeRetInt(RegisterFunction("nkAudioNode.GetChannelCount")); }
        }
        public ChannelCountMode ChannelCountMode
        {
            get { return (ChannelCountMode)InvokeRetInt(RegisterFunction("nkAudioNode.GetChannelCountMode")); }
        }


        internal AudioNode(int uid, BaseAudioContext context) : base(uid)
        {
            _context = context;
        }

        public void Connect(AudioNode destination)
        {
            Invoke(RegisterFunction("nkAudioNode.Connect"), destination.Uid);
        }

        public void Disconnect()
        {
            Invoke(RegisterFunction("nkAudioNode.Disconnect"));
        }

        public void Disconnect(AudioNode destination)
        {
            Invoke(RegisterFunction("nkAudioNode.Disconnect1"), destination.Uid);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Disconnect();

            }

            _context = null;

            base.Dispose(disposing);
        }
    }
}