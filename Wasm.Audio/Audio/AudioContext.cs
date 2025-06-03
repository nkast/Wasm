using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Audio
{
    public class AudioContext : BaseAudioContext
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        public AudioContext(int sampleRate) : base(Register(sampleRate))
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this));
        }

        [JSInvokable]
        public static void JsAudioContextInitialized(int uid)
        {
            if (!_uidMap.TryGetValue(uid, out var jsObjRef))
                return;
            if (!_uidMap[uid].TryGetTarget(out var jsObj))
                return;

            AudioContext audioContext = (AudioContext)jsObj;
            audioContext.IsInitialized = true;
        }

        private static int Register(int sampleRate)
        {
            int uid = JSObject.StaticInvokeRetInt("nkAudioContext.Create", sampleRate);
            return uid;
        }

        public void Close()
        {
            Invoke("nkAudioContext.Close");
        }

        public bool IsInitialized { get; private set; }

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
