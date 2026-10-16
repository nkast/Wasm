using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class GainNode : AudioNode
    {
        private readonly int _fid_GetGain;

        AudioParam _gain;

        internal GainNode(int uid, BaseAudioContext context) : base(uid, context)
        {
            _fid_GetGain = RegisterFunction("nkAudioGainNode.GetGain");
        }

        public AudioParam Gain
        {
            get
            {
                if (_gain == null)
                {
                    int uid = InvokeRetInt(_fid_GetGain);
                    _gain = new AudioParam(uid, this);
                }

                return _gain;
            }
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