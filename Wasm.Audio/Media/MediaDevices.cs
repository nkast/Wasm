using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using nkast.Wasm.Dom;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Media
{
    public class MediaDevices : CachedJSObject<MediaDevices>
    {
        private static readonly int _fid_Create = RegisterFunction("nkMediaDevices.Create");
        private readonly int _fid_GetUserMedia;


        public static MediaDevices FromNavigator(Navigator navigator)
        {
            int uid = JSObject.StaticInvokeRetInt(_fid_Create, navigator.Uid);
            if (uid == -1)
                return null;

            MediaDevices mediaDevices = MediaDevices.FromUid(uid);
            if (mediaDevices != null)
                return mediaDevices;

            return new MediaDevices(navigator, uid);
        }

        internal MediaDevices(Navigator navigator, int uid) : base(uid)
        {
            _fid_GetUserMedia = RegisterFunction("nkMediaDevices.GetUserMedia");
            //_navigator = navigator;
        }

        public Task<MediaStream> GetUserMediaAsync(UserMediaConstraints constraints)
        {
            int uid = InvokeRetInt<int>(_fid_GetUserMedia, (int)constraints.ToBit());

            PromiseJSObject<MediaStream> promise = new PromiseJSObject<MediaStream>(uid, (int newuid) => new MediaStream(newuid));
            return promise.GetTask();
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
