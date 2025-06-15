using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioListener : JSObject
    {
        BaseAudioContext _context;

        internal AudioListener(int uid, BaseAudioContext context) : base(uid)
        {
            _context = context;
        }

        public float PositionX
        {
            get { throw new NotImplementedException(); }
            set { Invoke("nkAudioListener.SetPositionX", value); }
        }

        public float PositionY
        {
            get { throw new NotImplementedException(); }
            set { Invoke("nkAudioListener.SetPositionY", value); }
        }

        public float PositionZ
        {
            get { throw new NotImplementedException(); }
            set { Invoke("nkAudioListener.SetPositionZ", value); }
        }
        
        public float ForwardX
        {
            get { throw new NotImplementedException(); }
            set { Invoke("nkAudioListener.SetForwardX", value); }
        }

        public float ForwardY
        {
            get { throw new NotImplementedException(); }
            set { Invoke("nkAudioListener.SetForwardY", value); }
        }

        public float ForwardZ
        {
            get { throw new NotImplementedException(); }
            set { Invoke("nkAudioListener.SetForwardZ", value); }
        }

        public float UpX
        {
            get { throw new NotImplementedException(); }
            set { Invoke("nkAudioListener.SetUpX", value); }
        }

        public float UpY
        {
            get { throw new NotImplementedException(); }
            set { Invoke("nkAudioListener.SetUpY", value); }
        }

        public float UpZ
        {
            get { throw new NotImplementedException(); }
            set { Invoke("nkAudioListener.SetUpZ", value); }
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
