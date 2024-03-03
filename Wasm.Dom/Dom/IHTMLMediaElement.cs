using System;
using System.Collections.Generic;

namespace nkast.Wasm.Dom
{
    public interface IHTMLMediaElement
    {
        /// <summary>
        /// The ended event is fired when playback or streaming has stopped because the end of the media was reached or because no further data is available.
        /// </summary>
        event EventHandler OnEnded;

        /// <summary>
        /// The playing event is fired after playback is first started, and whenever it is restarted. 
        /// For example it is fired when playback resumes after having been paused or delayed due to lack of data.
        /// </summary>
        event EventHandler OnPlaying;

        /// <summary>
        /// The timeupdate event is fired when the time indicated by the currentTime attribute has been updated.
        /// </summary>
        /// <remarks>The event frequency is dependent on the system load, but will be thrown between about 4Hz and 66Hz.</remarks>
        event EventHandler OnTimeUpdate;

        string CurrentSrc { get; }
        string Src { get; set; }
        bool Ended { get; }
        bool Paused { get; }
        bool Muted { get; set; }
        bool Loop { get; set; }
        float Volume { get; set; }


        void Load();
        void Play();
        void Pause();

    }
}
