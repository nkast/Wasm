using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioBuffer : JSObject
    {
        private readonly int _fid_GetSampleRate;
        private readonly int _fid_GetLength;
        private readonly int _fid_GetDuration;
        private readonly int _fid_GetNumberOfChannels;
        private readonly int _fid_CopyToChannel;

        BaseAudioContext _context;

        internal AudioBuffer(int uid, BaseAudioContext context) : base(uid)
        {
            _fid_GetSampleRate = RegisterFunction("nkAudioBuffer.GetSampleRate");
            _fid_GetLength = RegisterFunction("nkAudioBuffer.GetLength");
            _fid_GetDuration = RegisterFunction("nkAudioBuffer.GetDuration");
            _fid_GetNumberOfChannels = RegisterFunction("nkAudioBuffer.GetNumberOfChannels");
            _fid_CopyToChannel = RegisterFunction("nkAudioBuffer.CopyToChannel");
            _context = context;
        }

        public int SampleRate
        {
            get { return InvokeRetInt(_fid_GetSampleRate); }
        }

        public int Length
        {
            get { return InvokeRetInt(_fid_GetLength); }
        }

        public double Duration
        {
            get { return InvokeRetDouble(_fid_GetDuration); }
        }

        public int NumberOfChannels
        {
            get { return InvokeRetInt(_fid_GetNumberOfChannels); }
        }

        public unsafe void CopyToChannel(float[] source, int channelNumber)
        {
            fixed (float* pSource = source)
            {
                Invoke(_fid_CopyToChannel, channelNumber, (int)pSource, source.Length);
            }
        }

        public unsafe void CopyToChannel(Span<float> source, int channelNumber)
        {
            fixed (float* pSource = source)
            {
                Invoke(_fid_CopyToChannel, channelNumber, (int)pSource, source.Length);
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