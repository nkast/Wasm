using System;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Audio
{
    public class MediaElementAudioSourceNode : AudioNode
    {
        private readonly IHTMLMediaElement _mediaElement;

        public IHTMLMediaElement MediaElement
        {
            get { return _mediaElement; }
        }


        internal MediaElementAudioSourceNode(int uid, BaseAudioContext context, IHTMLMediaElement media) : base(uid, context)
        {
            this._mediaElement = media;
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
