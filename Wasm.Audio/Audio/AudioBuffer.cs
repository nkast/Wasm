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
            get { return InvokeRetInt("nkAudioBuffer.GetSampleRate"); }
        }

        public int Length
        {
            get { return InvokeRetInt("nkAudioBuffer.GetLength"); }
        }

        public double Duration
        {
            get { return InvokeRetDouble("nkAudioBuffer.GetDuration"); }
        }

        public int NumberOfChannels
        {
            get { return InvokeRetInt("nkAudioBuffer.GetNumberOfChannels"); }
        }

        public unsafe void CopyToChannel(float[] source, int channelNumber)
        {
            fixed (float* pSource = source)
            {
                Invoke("nkAudioBuffer.CopyToChannel", channelNumber, (int)pSource, source.Length);
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