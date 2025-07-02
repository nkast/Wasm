using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using nkast.Wasm.Dom;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Media
{
    public class MediaDevices : CachedJSObject<MediaDevices>
    {

        public static MediaDevices FromNavigator(Navigator navigator)
        {
            int uid = JSObject.StaticInvokeRetInt("nkMediaDevices.Create", navigator.Uid);
            if (uid == -1)
                return null;

            MediaDevices mediaDevices = MediaDevices.FromUid(uid);
            if (mediaDevices != null)
                return mediaDevices;

            return new MediaDevices(navigator, uid);
        }

        internal MediaDevices(Navigator navigator, int uid) : base(uid)
        {
            //_navigator = navigator;
        }

        public Task<MediaStream> GetUserMediaAsync(UserMediaConstraints constraints)
        {
            int uid = InvokeRetInt<int>("nkMediaDevices.GetUserMedia", (int)constraints.ToBit());

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
