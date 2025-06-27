using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using nkast.Wasm.Dom;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.WebClipboard
{
    public class Clipboard : CachedJSObject<Clipboard>
    {

        public static Clipboard FromNavigator(Navigator navigator)
        {
            int uid = JSObject.StaticInvokeRetInt("nkClipboard.Create", navigator.Uid);
            if (uid == -1)
                return null;

            Clipboard clipboard = Clipboard.FromUid(uid);
            if (clipboard != null)
                return clipboard;

            return new Clipboard(navigator, uid);
        }

        internal Clipboard(Navigator navigator, int uid) : base(uid)
        {
            //_navigator = navigator;
        }

        public Task<string> ReadText()
        {
            int uid = InvokeRetInt("nkClipboard.ReadText");

            PromiseString promise = new PromiseString(uid);
            return promise.GetTask();
        }

        public Task WriteText(string newClipText)
        {
            int uid = InvokeRetInt<string>("nkClipboard.WriteText", newClipText);

            PromiseVoid promise = new PromiseVoid(uid);
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
