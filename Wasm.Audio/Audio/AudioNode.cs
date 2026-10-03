using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioNode : CachedJSObject<AudioNode>
    {
        private readonly int _fid_GetNumberOfInputs;
        private readonly int _fid_GetNumberOfOutputs;
        private readonly int _fid_GetChannelCount;
        private readonly int _fid_GetChannelCountMode;
        private readonly int _fid_Connect;
        private readonly int _fid_Disconnect;
        private readonly int _fid_Disconnect1;

        BaseAudioContext _context;

        protected BaseAudioContext Context { get { return _context; } }

        public int NumberOfInputs
        {
            get { return InvokeRetInt(_fid_GetNumberOfInputs); }
        }
        public int NumberOfOutputs
        {
            get { return InvokeRetInt(_fid_GetNumberOfOutputs); }
        }
        public int ChannelCount
        {
            get { return InvokeRetInt(_fid_GetChannelCount); }
        }
        public ChannelCountMode ChannelCountMode
        {
            get { return (ChannelCountMode)InvokeRetInt(_fid_GetChannelCountMode); }
        }


        internal AudioNode(int uid, BaseAudioContext context) : base(uid)
        {
            _fid_GetNumberOfInputs = RegisterFunction("nkAudioNode.GetNumberOfInputs");
            _fid_GetNumberOfOutputs = RegisterFunction("nkAudioNode.GetNumberOfOutputs");
            _fid_GetChannelCount = RegisterFunction("nkAudioNode.GetChannelCount");
            _fid_GetChannelCountMode = RegisterFunction("nkAudioNode.GetChannelCountMode");
            _fid_Connect = RegisterFunction("nkAudioNode.Connect");
            _fid_Disconnect = RegisterFunction("nkAudioNode.Disconnect");
            _fid_Disconnect1 = RegisterFunction("nkAudioNode.Disconnect1");
            _context = context;
        }

        public void Connect(AudioNode destination)
        {
            Invoke(_fid_Connect, destination.Uid);
        }

        public void Disconnect()
        {
            Invoke(_fid_Disconnect);
        }

        public void Disconnect(AudioNode destination)
        {
            Invoke(_fid_Disconnect1, destination.Uid);
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