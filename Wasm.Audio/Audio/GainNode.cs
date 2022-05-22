using System;
using nkast.Wasm.Dom;

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
                    int uid = InvokeRet<int>("nkAudioGainNode.GetGain");
                    _gain = new AudioParam(uid, Context);
                }

                return _gain;
            }
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