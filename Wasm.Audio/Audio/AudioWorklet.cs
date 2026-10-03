using System;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioWorklet : CachedJSObject<AudioWorklet>
    {
        private readonly int _fid_AddModule;

        internal AudioWorklet(int uid) : base(uid)
        {
            _fid_AddModule = RegisterFunction("nkAudioWorklet.AddModule");
        }

        public Task AddModuleAsync(string moduleURL)
        {
            int uid = InvokeRetInt(_fid_AddModule, moduleURL);

            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            base.Dispose(disposing);
        }
    }
}
