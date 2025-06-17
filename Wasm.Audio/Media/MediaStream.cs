using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Media
{
    public class MediaStream : CachedJSObject<MediaStream>
    {

        internal MediaStream(int uid) : base(uid)
        {
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