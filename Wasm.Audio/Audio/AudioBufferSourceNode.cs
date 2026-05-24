using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioBufferSourceNode : AudioScheduledSourceNode
    {
        AudioParam _playbackRate;

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
            get { return InvokeRetBool("nkAudioBufferSourceNode.GetLoop"); }
            set { Invoke("nkAudioBufferSourceNode.SetLoop", value ? 1 : 0); }
        }

        public AudioParam PlaybackRate
        {
            get
            {
                if (_playbackRate == null)
                {
                    int uid = InvokeRetInt("nkAudioBufferSourceNode.GetPlaybackRate");
                    _playbackRate = new AudioParam(uid, this);
                }

                return _playbackRate;
            }
        }

        public void Start(double when = 0d, double offset = 0d, double duration = 0d)
        {
            Invoke("nkAudioBufferSourceNode.Start", when, offset, duration);
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