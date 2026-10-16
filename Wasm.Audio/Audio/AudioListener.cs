using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class AudioListener : JSObject
    {
        private readonly int _fid_SetPositionX;
        private readonly int _fid_SetPositionY;
        private readonly int _fid_SetPositionZ;
        private readonly int _fid_SetForwardX;
        private readonly int _fid_SetForwardY;
        private readonly int _fid_SetForwardZ;
        private readonly int _fid_SetUpX;
        private readonly int _fid_SetUpY;
        private readonly int _fid_SetUpZ;

        BaseAudioContext _context;

        internal AudioListener(int uid, BaseAudioContext context) : base(uid)
        {
            _fid_SetPositionX = RegisterFunction("nkAudioListener.SetPositionX");
            _fid_SetPositionY = RegisterFunction("nkAudioListener.SetPositionY");
            _fid_SetPositionZ = RegisterFunction("nkAudioListener.SetPositionZ");
            _fid_SetForwardX = RegisterFunction("nkAudioListener.SetForwardX");
            _fid_SetForwardY = RegisterFunction("nkAudioListener.SetForwardY");
            _fid_SetForwardZ = RegisterFunction("nkAudioListener.SetForwardZ");
            _fid_SetUpX = RegisterFunction("nkAudioListener.SetUpX");
            _fid_SetUpY = RegisterFunction("nkAudioListener.SetUpY");
            _fid_SetUpZ = RegisterFunction("nkAudioListener.SetUpZ");
            _context = context;
        }

        public float PositionX
        {
            get { throw new NotImplementedException(); }
            set { Invoke(_fid_SetPositionX, value); }
        }

        public float PositionY
        {
            get { throw new NotImplementedException(); }
            set { Invoke(_fid_SetPositionY, value); }
        }

        public float PositionZ
        {
            get { throw new NotImplementedException(); }
            set { Invoke(_fid_SetPositionZ, value); }
        }
        
        public float ForwardX
        {
            get { throw new NotImplementedException(); }
            set { Invoke(_fid_SetForwardX, value); }
        }

        public float ForwardY
        {
            get { throw new NotImplementedException(); }
            set { Invoke(_fid_SetForwardY, value); }
        }

        public float ForwardZ
        {
            get { throw new NotImplementedException(); }
            set { Invoke(_fid_SetForwardZ, value); }
        }

        public float UpX
        {
            get { throw new NotImplementedException(); }
            set { Invoke(_fid_SetUpX, value); }
        }

        public float UpY
        {
            get { throw new NotImplementedException(); }
            set { Invoke(_fid_SetUpY, value); }
        }

        public float UpZ
        {
            get { throw new NotImplementedException(); }
            set { Invoke(_fid_SetUpZ, value); }
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
