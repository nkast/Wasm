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
            get { return InvokeRetDouble(RegisterFunction("nkAudioParam.GetDefaultValue")); }
        }
        public double MinValue
        {
            get { return InvokeRetDouble(RegisterFunction("nkAudioParam.GetMinValue")); }
        }
        public double MaxValue
        {
            get { return InvokeRetDouble(RegisterFunction("nkAudioParam.GetMaxValue")); }
        }

        public float Value
        {
            get { return InvokeRetFloat(RegisterFunction("nkAudioParam.GetValue")); }
            set { Invoke(RegisterFunction("nkAudioParam.SetValue"), value); }
        }

        public void SetValueAtTime(float value, float startTime)
        {
            Invoke(RegisterFunction("nkAudioParam.SetValueAtTime"), value, startTime);
        }

        public void LinearRampToValueAtTime(float value, float endTime)
        {
            Invoke(RegisterFunction("nkAudioParam.LinearRampToValueAtTime"), value, endTime);
        }

        public void ExponentialRampToValueAtTime(float value, float endTime)
        {
            Invoke(RegisterFunction("nkAudioParam.ExponentialRampToValueAtTime"), value, endTime);
        }

        public void SetTargetAtTime(float target, float startTime, float timeConstant)
        {
            Invoke(RegisterFunction("nkAudioParam.SetTargetAtTime"), target, startTime, timeConstant);
        }

        public unsafe void SetValueCurveAtTime(float[] values, float startTime, float duration)
        {
            fixed (float* pValues = values)
            {
                Invoke(RegisterFunction("nkAudioParam.SetValueCurveAtTime"), startTime, duration, (int)pValues, values.Length);
            }
        }

        public unsafe void SetValueCurveAtTime(Span<float> values, float startTime, float duration)
        {
            fixed (float* pValues = values)
            {
                Invoke(RegisterFunction("nkAudioParam.SetValueCurveAtTime"), startTime, duration, (int)pValues, values.Length);
            }
        }

        public void CancelScheduledValues(float startTime)
        {
            Invoke(RegisterFunction("nkAudioParam.CancelScheduledValues"), startTime);
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
