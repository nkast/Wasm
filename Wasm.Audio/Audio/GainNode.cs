using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class GainNode : AudioNode
    {
        AudioParam _gain;

        internal GainNode(int uid, BaseAudioContext context) : base(uid, context)
        {
        }

        public AudioParam Gain
        {
            get
            {
                if (_gain == null)
                {
                    int uid = InvokeRetInt("nkAudioGainNode.GetGain");
                    _gain = new AudioParam(uid, Context);
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