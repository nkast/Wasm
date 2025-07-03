using System;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;
using nkast.Wasm.Media;

namespace nkast.Wasm.Audio
{
    public class AudioContext : BaseAudioContext
    {
        public AudioContext() : base(Register())
        {
        }

        public AudioContext(AudioContextOptions options) : base(Register(options))
        {
        }

        private static int Register()
        {
            int uid = JSObject.StaticInvokeRetInt("nkAudioContext.Create");
            return uid;
        }

        private static int Register(AudioContextOptions options)
        {
            if (options.SampleRate.HasValue && options.SampleRate.Value == 0)
                throw new ArgumentException("SampleRate cannot be zero.", nameof(options.SampleRate));

            int sampleRate = options.SampleRate ?? 0;
            int uid = JSObject.StaticInvokeRetInt("nkAudioContext.Create1", sampleRate);
            return uid;
        }

        public void Close()
        {
            Invoke("nkAudioContext.Close");
        }

        public MediaStreamSourceNode CreateMediaStreamSource(MediaStream stream)
        {
            int uid = InvokeRetInt<int>("nkAudioContext.CreateMediaStreamSource", ((JSObject)stream).Uid);
            return new MediaStreamSourceNode(uid, this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Close();

            base.Dispose(disposing);
        }

    }
}
