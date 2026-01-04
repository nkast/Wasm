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
            get { return InvokeRetString("nkMedia.GetCurrentSrc"); }
        }

        public TimeSpan CurrentTime
        {
            get
            {
                double currentTime = InvokeRetDouble("nkMedia.GetCurrentTime");
                return TimeSpan.FromSeconds(currentTime);
            }
        }

        public string Src
        {
            get { return InvokeRetString("nkMedia.GetSrc"); }
            set { Invoke("nkMedia.SetSrc", value); }
        }

        public bool Ended
        {
            get { return InvokeRetBool("nkMedia.GetEnded"); }
        }

        public bool Paused
        {
            get { return InvokeRetBool("nkMedia.GetPaused"); }
        }

        public bool Muted
        {
            get { return InvokeRetBool("nkMedia.GetMuted"); }
            set { Invoke("nkMedia.SetMuted", value); }
        }

        public bool Loop
        {
            get { return InvokeRetBool("nkMedia.GetLoop"); }
            set { Invoke("nkMedia.SetLoop", value); }
        }

        public float Volume
        {
            get { throw new NotImplementedException(); }
            set { Invoke("nkMedia.SetVolume", value); }
        }

        internal HTMLMediaElement(int uid) : base(uid)
        {
            Invoke("nkMedia.RegisterEvents");
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
            Invoke("nkMedia.Load");
        }

        public void Play()
        {
            try
            {
                Invoke("nkMedia.Play");
            }
            catch(Exception e)
            {
                //throw;
            }
        }

        public void Pause()
        {
            Invoke("nkMedia.Pause");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Pause();
            Invoke("nkMedia.UnregisterEvents");

            base.Dispose(disposing);
        }
    }
}
