using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Audio
{
    public class AudioContext : BaseAudioContext
    {
        public AudioContext() : base(Register())
        {
        }


        private static int Register()
        {
            int uid = JSObject.StaticInvokeRetInt("nkAudioContext.Create");
            return uid;
        }

        public void Close()
        {
            Invoke("nkAudioContext.Close");
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
