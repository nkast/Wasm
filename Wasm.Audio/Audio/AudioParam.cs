using System;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Audio
{
    public class AudioParam : JSObject
    {
        BaseAudioContext _context;

        internal AudioParam(int uid, BaseAudioContext context) : base(uid)
        {
            _context = context;
        }

        public float Value
        {
            get { throw new NotImplementedException(); }
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
            if (IsDisposed)
                return;

            if (disposing)
            {

            }

            _context = null;

            base.Dispose(disposing);
        }
    }
}
