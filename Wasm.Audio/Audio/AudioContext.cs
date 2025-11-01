using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task ResumeAsync()
        {
            int uid = InvokeRetInt("nkAudioContext.Resume");

            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        public Task SuspendAsync()
        {
            int uid = InvokeRetInt("nkAudioContext.Suspend");

            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        public Task CloseAsync()
        {
            int uid = InvokeRetInt("nkAudioContext.Close1");

            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
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

            CloseAsync();

            base.Dispose(disposing);
        }

    }
}
