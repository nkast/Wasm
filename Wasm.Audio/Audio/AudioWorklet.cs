using System;
using System.Threading.Tasks;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioWorklet : CachedJSObject<AudioWorklet>
    {
        internal AudioWorklet(int uid) : base(uid)
        {
        }

        public Task AddModuleAsync(string moduleURL)
        {
            int uid = InvokeRetInt("nkAudioWorklet.AddModule", moduleURL);

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
