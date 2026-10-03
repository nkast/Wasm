using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioParam : JSObject
    {
        private readonly int _fid_GetDefaultValue;
        private readonly int _fid_GetMinValue;
        private readonly int _fid_GetMaxValue;
        private readonly int _fid_GetValue;
        private readonly int _fid_SetValue;
        private readonly int _fid_SetValueAtTime;
        private readonly int _fid_LinearRampToValueAtTime;
        private readonly int _fid_ExponentialRampToValueAtTime;
        private readonly int _fid_SetTargetAtTime;
        private readonly int _fid_SetValueCurveAtTime;
        private readonly int _fid_CancelScheduledValues;

        AudioNode _audioNode;

        internal AudioParam(int uid, AudioNode audioNode) : base(uid)
        {
            _fid_GetDefaultValue = RegisterFunction("nkAudioParam.GetDefaultValue");
            _fid_GetMinValue = RegisterFunction("nkAudioParam.GetMinValue");
            _fid_GetMaxValue = RegisterFunction("nkAudioParam.GetMaxValue");
            _fid_GetValue = RegisterFunction("nkAudioParam.GetValue");
            _fid_SetValue = RegisterFunction("nkAudioParam.SetValue");
            _fid_SetValueAtTime = RegisterFunction("nkAudioParam.SetValueAtTime");
            _fid_LinearRampToValueAtTime = RegisterFunction("nkAudioParam.LinearRampToValueAtTime");
            _fid_ExponentialRampToValueAtTime = RegisterFunction("nkAudioParam.ExponentialRampToValueAtTime");
            _fid_SetTargetAtTime = RegisterFunction("nkAudioParam.SetTargetAtTime");
            _fid_SetValueCurveAtTime = RegisterFunction("nkAudioParam.SetValueCurveAtTime");
            _fid_CancelScheduledValues = RegisterFunction("nkAudioParam.CancelScheduledValues");
            _audioNode = audioNode;
        }

        public double DefaultValue
        {
            get { return InvokeRetDouble(_fid_GetDefaultValue); }
        }
        public double MinValue
        {
            get { return InvokeRetDouble(_fid_GetMinValue); }
        }
        public double MaxValue
        {
            get { return InvokeRetDouble(_fid_GetMaxValue); }
        }

        public float Value
        {
            get { return InvokeRetFloat(_fid_GetValue); }
            set { Invoke(_fid_SetValue, value); }
        }

        public void SetValueAtTime(float value, float startTime)
        {
            Invoke(_fid_SetValueAtTime, value, startTime);
        }

        public void LinearRampToValueAtTime(float value, float endTime)
        {
            Invoke(_fid_LinearRampToValueAtTime, value, endTime);
        }

        public void ExponentialRampToValueAtTime(float value, float endTime)
        {
            Invoke(_fid_ExponentialRampToValueAtTime, value, endTime);
        }

        public void SetTargetAtTime(float target, float startTime, float timeConstant)
        {
            Invoke(_fid_SetTargetAtTime, target, startTime, timeConstant);
        }

        public unsafe void SetValueCurveAtTime(float[] values, float startTime, float duration)
        {
            fixed (float* pValues = values)
            {
                Invoke(_fid_SetValueCurveAtTime, startTime, duration, (int)pValues, values.Length);
            }
        }

        public unsafe void SetValueCurveAtTime(Span<float> values, float startTime, float duration)
        {
            fixed (float* pValues = values)
            {
                Invoke(_fid_SetValueCurveAtTime, startTime, duration, (int)pValues, values.Length);
            }
        }

        public void CancelScheduledValues(float startTime)
        {
            Invoke(_fid_CancelScheduledValues, startTime);
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
