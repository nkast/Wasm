using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;
using nkast.Wasm.Media;

namespace nkast.Wasm.Audio
{
    public class AudioContext : BaseAudioContext
    {
        private static readonly int _fid_Create = RegisterFunction("nkAudioContext.Create");
        private static readonly int _fid_Create1 = RegisterFunction("nkAudioContext.Create1");
        private readonly int _fid_Resume;
        private readonly int _fid_Suspend;
        private readonly int _fid_Close1;
        private readonly int _fid_CreateMediaStreamSource;

        public AudioContext() : base(Register())
        {
            _fid_Resume = RegisterFunction("nkAudioContext.Resume");
            _fid_Suspend = RegisterFunction("nkAudioContext.Suspend");
            _fid_Close1 = RegisterFunction("nkAudioContext.Close1");
            _fid_CreateMediaStreamSource = RegisterFunction("nkAudioContext.CreateMediaStreamSource");
        }

        public AudioContext(AudioContextOptions options) : base(Register(options))
        {
            _fid_Resume = RegisterFunction("nkAudioContext.Resume");
            _fid_Suspend = RegisterFunction("nkAudioContext.Suspend");
            _fid_Close1 = RegisterFunction("nkAudioContext.Close1");
            _fid_CreateMediaStreamSource = RegisterFunction("nkAudioContext.CreateMediaStreamSource");
        }

        private static int Register()
        {
            int uid = JSObject.StaticInvokeRetInt(_fid_Create);
            return uid;
        }

        private static int Register(AudioContextOptions options)
        {
            if (options.SampleRate.HasValue && options.SampleRate.Value == 0)
                throw new ArgumentException("SampleRate cannot be zero.", nameof(options.SampleRate));

            int sampleRate = options.SampleRate ?? 0;
            int uid = JSObject.StaticInvokeRetInt(_fid_Create1, sampleRate);
            return uid;
        }

        public Task ResumeAsync()
        {
            int uid = InvokeRetInt(_fid_Resume);

            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        public Task SuspendAsync()
        {
            int uid = InvokeRetInt(_fid_Suspend);

            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        public Task CloseAsync()
        {
            int uid = InvokeRetInt(_fid_Close1);

            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        public MediaStreamSourceNode CreateMediaStreamSource(MediaStream stream)
        {
            int uid = InvokeRetInt<int>(_fid_CreateMediaStreamSource, ((JSObject)stream).Uid);
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
