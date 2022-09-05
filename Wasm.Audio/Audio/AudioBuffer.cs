using System;
using nkast.Wasm.Dom;

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
            get { return InvokeRet<int>("nkAudioBuffer.GetSampleRate"); }
        }

        public int Length
        {
            get { return InvokeRet<int>("nkAudioBuffer.GetLength"); }
        }

        public int NumberOfChannels
        {
            get { return InvokeRet<int>("nkAudioBuffer.GetNumberOfChannels"); }
        }

        public void CopyToChannel(float[] source, int channelNumber)
        {
            Invoke("nkAudioBuffer.CopyToChannel", channelNumber, source);
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