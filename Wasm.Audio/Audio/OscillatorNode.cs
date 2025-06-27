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
                    int uid = InvokeRetInt("nkAudioOscillatorNode.GetFrequency");
                    _frequency = new AudioParam(uid);
                }

                return _frequency;
            }
        }

        public OscillatorType Type
        {
            get { return (OscillatorType)InvokeRetInt("nkAudioOscillatorNode.GetType"); }
            set { Invoke("nkAudioOscillatorNode.SetType", (int)value); }
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