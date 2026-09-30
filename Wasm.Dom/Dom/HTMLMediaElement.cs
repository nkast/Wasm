using System;
using System.Collections.Generic;
using Microsoft.JSInterop;

namespace nkast.Wasm.Dom
{
    public abstract class HTMLMediaElement : HTMLElement<HTMLMediaElement>, IHTMLMediaElement
    {

        public event EventHandler OnEnded;
        public event EventHandler OnPlaying;
        public event EventHandler OnTimeUpdate;

        public string CurrentSrc
        {
            get { return InvokeRetString(RegisterFunction("nkMedia.GetCurrentSrc")); }
        }

        public TimeSpan CurrentTime
        {
            get
            {
                double currentTime = InvokeRetDouble(RegisterFunction("nkMedia.GetCurrentTime"));
                return TimeSpan.FromSeconds(currentTime);
            }
        }

        public string Src
        {
            get { return InvokeRetString(RegisterFunction("nkMedia.GetSrc")); }
            set { Invoke(RegisterFunction("nkMedia.SetSrc"), value); }
        }

        public bool Ended
        {
            get { return InvokeRetBool(RegisterFunction("nkMedia.GetEnded")); }
        }

        public bool Paused
        {
            get { return InvokeRetBool(RegisterFunction("nkMedia.GetPaused")); }
        }

        public bool Muted
        {
            get { return InvokeRetBool(RegisterFunction("nkMedia.GetMuted")); }
            set { Invoke(RegisterFunction("nkMedia.SetMuted"), value); }
        }

        public bool Loop
        {
            get { return InvokeRetBool(RegisterFunction("nkMedia.GetLoop")); }
            set { Invoke(RegisterFunction("nkMedia.SetLoop"), value); }
        }

        public float Volume
        {
            get { throw new NotImplementedException(); }
            set { Invoke(RegisterFunction("nkMedia.SetVolume"), value); }
        }

        internal HTMLMediaElement(int uid) : base(uid)
        {
            Invoke(RegisterFunction("nkMedia.RegisterEvents"));
        }


        [JSInvokable]
        public static void JsMediaOnEnded(int uid)
        {
            HTMLMediaElement mediaElement = HTMLMediaElement.FromUid(uid);
            if (mediaElement == null)
                return;

            var handler = mediaElement.OnEnded;
            if (handler != null)
                handler(mediaElement, EventArgs.Empty);
        }

        [JSInvokable]
        public static void JsMediaOnPlaying(int uid)
        {
            HTMLMediaElement mediaElement = HTMLMediaElement.FromUid(uid);
            if (mediaElement == null)
                return;

            var handler = mediaElement.OnPlaying;
            if (handler != null)
                handler(mediaElement, EventArgs.Empty);
        }

        [JSInvokable]
        public static void JsMediaOnOnTimeUpdate(int uid)
        {
            HTMLMediaElement mediaElement = HTMLMediaElement.FromUid(uid);
            if (mediaElement == null)
                return;

            var handler = mediaElement.OnTimeUpdate;
            if (handler != null)
                handler(mediaElement, EventArgs.Empty);
        }

        public void Load()
        {
            Invoke(RegisterFunction("nkMedia.Load"));
        }

        public void Play()
        {
            try
            {
                Invoke(RegisterFunction("nkMedia.Play"));
            }
            catch(Exception e)
            {
                //throw;
            }
        }

        public void Pause()
        {
            Invoke(RegisterFunction("nkMedia.Pause"));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Pause();
            Invoke(RegisterFunction("nkMedia.UnregisterEvents"));

            base.Dispose(disposing);
        }
    }
}
