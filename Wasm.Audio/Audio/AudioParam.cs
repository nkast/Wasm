using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioParam : JSObject
    {
        AudioNode _audioNode;

        internal AudioParam(int uid, AudioNode audioNode) : base(uid)
        {
            _audioNode = audioNode;
        }

        public double DefaultValue
        {
            get { return InvokeRetDouble("nkAudioParam.GetDefaultValue"); }
        }
        public double MinValue
        {
            get { return InvokeRetDouble("nkAudioParam.GetMinValue"); }
        }
        public double MaxValue
        {
            get { return InvokeRetDouble("nkAudioParam.GetMaxValue"); }
        }

        public float Value
        {
            get { return InvokeRetFloat("nkAudioParam.GetValue"); }
            set { Invoke("nkAudioParam.SetValue", value); }
        }

        public void SetValueAtTime(float value, float startTime)
        {
            Invoke("nkAudioParam.SetValueAtTime", value, startTime);
        }

        public void LinearRampToValueAtTime(float value, float endTime)
        {
            Invoke("nkAudioParam.LinearRampToValueAtTime", value, endTime);
        }

        public void ExponentialRampToValueAtTime(float value, float endTime)
        {
            Invoke("nkAudioParam.ExponentialRampToValueAtTime", value, endTime);
        }

        public void SetTargetAtTime(float target, float startTime, float timeConstant)
        {
            Invoke("nkAudioParam.SetTargetAtTime", target, startTime, timeConstant);
        }

        public void SetValueCurveAtTime(float[] values, float startTime, float duration)
        {
            Invoke("nkAudioParam.SetValueCurveAtTime", startTime, duration, values);
        }

        public void CancelScheduledValues(float startTime)
        {
            Invoke("nkAudioParam.CancelScheduledValues", startTime);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _audioNode = null;

            base.Dispose(disposing);
        }
    }
}
