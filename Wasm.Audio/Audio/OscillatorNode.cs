using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class OscillatorNode : AudioScheduledSourceNode
    {
        AudioParam _frequency;

        internal OscillatorNode(int uid, BaseAudioContext context) : base(uid, context)
        {
        }

        public AudioParam Frequency
        {
            get
            {
                if (_frequency == null)
                {
                    int uid = InvokeRetInt(RegisterFunction("nkAudioOscillatorNode.GetFrequency"));
                    _frequency = new AudioParam(uid, this);
                }

                return _frequency;
            }
        }

        public OscillatorType Type
        {
            get { return (OscillatorType)InvokeRetInt(RegisterFunction("nkAudioOscillatorNode.GetType")); }
            set { Invoke(RegisterFunction("nkAudioOscillatorNode.SetType"), (int)value); }
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