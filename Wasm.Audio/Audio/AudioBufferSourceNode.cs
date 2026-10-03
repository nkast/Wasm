using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioBufferSourceNode : AudioScheduledSourceNode
    {
        private readonly int _fid_SetBuffer;
        private readonly int _fid_GetLoop;
        private readonly int _fid_SetLoop;
        private readonly int _fid_GetPlaybackRate;
        private readonly int _fid_Start;

        AudioParam _playbackRate;

        internal AudioBufferSourceNode(int uid, BaseAudioContext context) : base(uid, context)
        {
            _fid_SetBuffer = RegisterFunction("nkAudioBufferSourceNode.SetBuffer");
            _fid_GetLoop = RegisterFunction("nkAudioBufferSourceNode.GetLoop");
            _fid_SetLoop = RegisterFunction("nkAudioBufferSourceNode.SetLoop");
            _fid_GetPlaybackRate = RegisterFunction("nkAudioBufferSourceNode.GetPlaybackRate");
            _fid_Start = RegisterFunction("nkAudioBufferSourceNode.Start");
        }

        public AudioBuffer Buffer
        {
            get { throw new NotImplementedException(); }
            set { Invoke(_fid_SetBuffer, value.Uid); }
        }

        public bool Loop
        {
            get { return InvokeRetBool(_fid_GetLoop); }
            set { Invoke(_fid_SetLoop, value ? 1 : 0); }
        }

        public AudioParam PlaybackRate
        {
            get
            {
                if (_playbackRate == null)
                {
                    int uid = InvokeRetInt(_fid_GetPlaybackRate);
                    _playbackRate = new AudioParam(uid, this);
                }

                return _playbackRate;
            }
        }

        public void Start(double when = 0d, double offset = 0d, double duration = 0d)
        {
            Invoke(_fid_Start, when, offset, duration);
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