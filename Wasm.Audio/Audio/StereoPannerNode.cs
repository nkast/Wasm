using System;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Audio
{
    public class StereoPannerNode : AudioNode
    {
        private readonly int _fid_GetPan;

        AudioParam _pan;

        internal StereoPannerNode(int uid, BaseAudioContext context) : base(uid, context)
        {
            _fid_GetPan = RegisterFunction("nkAudioStereoPannerNode.GetPan");
        }

        public AudioParam Pan
        {
            get
            {
                if (_pan == null)
                {
                    int uid = InvokeRetInt(_fid_GetPan);
                    _pan = new AudioParam(uid, this);
                }

                return _pan;
            }
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