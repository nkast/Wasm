using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioBuffer : JSObject
    {
        BaseAudioContext _context;

        internal AudioBuffer(int uid, BaseAudioContext context) : base(uid)
        {
            _context = context;
        }

        public int SampleRate
        {
            get { return InvokeRetInt(RegisterFunction("nkAudioBuffer.GetSampleRate")); }
        }

        public int Length
        {
            get { return InvokeRetInt(RegisterFunction("nkAudioBuffer.GetLength")); }
        }

        public double Duration
        {
            get { return InvokeRetDouble(RegisterFunction("nkAudioBuffer.GetDuration")); }
        }

        public int NumberOfChannels
        {
            get { return InvokeRetInt(RegisterFunction("nkAudioBuffer.GetNumberOfChannels")); }
        }

        public unsafe void CopyToChannel(float[] source, int channelNumber)
        {
            fixed (float* pSource = source)
            {
                Invoke(RegisterFunction("nkAudioBuffer.CopyToChannel"), channelNumber, (int)pSource, source.Length);
            }
        }

        public unsafe void CopyToChannel(Span<float> source, int channelNumber)
        {
            fixed (float* pSource = source)
            {
                Invoke(RegisterFunction("nkAudioBuffer.CopyToChannel"), channelNumber, (int)pSource, source.Length);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _context = null;

            base.Dispose(disposing);
        }
    }
}