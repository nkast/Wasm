using System;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Audio
{
    public class StereoPannerNode : AudioNode
    {
        AudioParam _pan;

        internal StereoPannerNode(int uid, BaseAudioContext context) : base(uid, context)
        {
        }

        public AudioParam Pan
        {
            get
            {
                if (_pan == null)
                {
                    int uid = InvokeRet<int>("nkAudioStereoPannerNode.GetPan");
                    _pan = new AudioParam(uid, Context);
                }

                return _pan;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            if (disposing)
            {

            }

            base.Dispose(disposing);
        }
    }
}