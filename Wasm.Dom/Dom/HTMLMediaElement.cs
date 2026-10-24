using System;
using System.Collections.Generic;
using Microsoft.JSInterop;

namespace nkast.Wasm.Dom
{
    public abstract class HTMLMediaElement : HTMLElement<HTMLMediaElement>, IHTMLMediaElement
    {
        private readonly int _fid_GetCurrentSrc;
        private readonly int _fid_GetCurrentTime;
        private readonly int _fid_GetSrc;
        private readonly int _fid_SetSrc;
        private readonly int _fid_GetEnded;
        private readonly int _fid_GetPaused;
        private readonly int _fid_GetMuted;
        private readonly int _fid_SetMuted;
        private readonly int _fid_GetLoop;
        private readonly int _fid_SetLoop;
        private readonly int _fid_SetVolume;
        private readonly int _fid_RegisterEvents;
        private readonly int _fid_Load;
        private readonly int _fid_Play;
        private readonly int _fid_Pause;
        private readonly int _fid_UnregisterEvents;


        public event EventHandler OnEnded;
        public event EventHandler OnPlaying;
        public event EventHandler OnTimeUpdate;

        public string CurrentSrc
        {
            get { return InvokeRetString(_fid_GetCurrentSrc); }
        }

        public TimeSpan CurrentTime
        {
            get
            {
                double currentTime = InvokeRetDouble(_fid_GetCurrentTime);
                return TimeSpan.FromSeconds(currentTime);
            }
        }

        public string Src
        {
            get { return InvokeRetString(_fid_GetSrc); }
            set { Invoke(_fid_SetSrc, value); }
        }

        public bool Ended
        {
            get { return InvokeRetBool(_fid_GetEnded); }
        }

        public bool Paused
        {
            get { return InvokeRetBool(_fid_GetPaused); }
        }

        public bool Muted
        {
            get { return InvokeRetBool(_fid_GetMuted); }
            set { Invoke(_fid_SetMuted, value); }
        }

        public bool Loop
        {
            get { return InvokeRetBool(_fid_GetLoop); }
            set { Invoke(_fid_SetLoop, value); }
        }

        public float Volume
        {
            get { throw new NotImplementedException(); }
            set { Invoke(_fid_SetVolume, value); }
        }

        internal HTMLMediaElement(int uid) : base(uid)
        {
            _fid_GetCurrentSrc = RegisterFunction("nkMedia.GetCurrentSrc");
            _fid_GetCurrentTime = RegisterFunction("nkMedia.GetCurrentTime");
            _fid_GetSrc = RegisterFunction("nkMedia.GetSrc");
            _fid_SetSrc = RegisterFunction("nkMedia.SetSrc");
            _fid_GetEnded = RegisterFunction("nkMedia.GetEnded");
            _fid_GetPaused = RegisterFunction("nkMedia.GetPaused");
            _fid_GetMuted = RegisterFunction("nkMedia.GetMuted");
            _fid_SetMuted = RegisterFunction("nkMedia.SetMuted");
            _fid_GetLoop = RegisterFunction("nkMedia.GetLoop");
            _fid_SetLoop = RegisterFunction("nkMedia.SetLoop");
            _fid_SetVolume = RegisterFunction("nkMedia.SetVolume");
            _fid_RegisterEvents = RegisterFunction("nkMedia.RegisterEvents");
            _fid_Load = RegisterFunction("nkMedia.Load");
            _fid_Play = RegisterFunction("nkMedia.Play");
            _fid_Pause = RegisterFunction("nkMedia.Pause");
            _fid_UnregisterEvents = RegisterFunction("nkMedia.UnregisterEvents");
            Invoke(_fid_RegisterEvents);
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
            Invoke(_fid_Load);
        }

        public void Play()
        {
            try
            {
                Invoke(_fid_Play);
            }
            catch(Exception e)
            {
                //throw;
            }
        }

        public void Pause()
        {
            Invoke(_fid_Pause);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Pause();
            Invoke(_fid_UnregisterEvents);

            base.Dispose(disposing);
        }
    }
}
