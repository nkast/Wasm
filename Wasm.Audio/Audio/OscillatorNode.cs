using System;
using nkast.Wasm.Dom;

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
                    int uid = InvokeRet<int>("nkAudioOscillatorNode.GetFrequency");
                    _frequency = new AudioParam(uid, Context);
                }

                return _frequency;
            }
        }

        public OscillatorType Type
        {
            get
            {
                string str = InvokeRet<string>("nkAudioOscillatorNode.GetType");
                return Enum.Parse<OscillatorType>(str, true);
            }
            set { Invoke("nkCanvas2dContext.SetType", value.ToString().ToLower()); }
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