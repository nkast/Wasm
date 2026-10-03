using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class OscillatorNode : AudioScheduledSourceNode
    {
        private readonly int _fid_GetFrequency;
        private readonly int _fid_GetType;
        private readonly int _fid_SetType;

        AudioParam _frequency;

        internal OscillatorNode(int uid, BaseAudioContext context) : base(uid, context)
        {
            _fid_GetFrequency = RegisterFunction("nkAudioOscillatorNode.GetFrequency");
            _fid_GetType = RegisterFunction("nkAudioOscillatorNode.GetType");
            _fid_SetType = RegisterFunction("nkAudioOscillatorNode.SetType");
        }

        public AudioParam Frequency
        {
            get
            {
                if (_frequency == null)
                {
                    int uid = InvokeRetInt(_fid_GetFrequency);
                    _frequency = new AudioParam(uid, this);
                }

                return _frequency;
            }
        }

        public OscillatorType Type
        {
            get { return (OscillatorType)InvokeRetInt(_fid_GetType); }
            set { Invoke(_fid_SetType, (int)value); }
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