﻿using System;
using nkast.Wasm.JSInterop;

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
                    int uid = InvokeRetInt("nkAudioStereoPannerNode.GetPan");
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