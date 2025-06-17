using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.ChannelMessaging
{
    public class MessagePort : CachedJSObject<MessagePort>
    {
        public MessagePort(int uid) : base(uid)
        {

        }

        public void Start()
        {
            Invoke("nkMessagePort.Start");
        }

        public void close()
        {
            Invoke("nkMessagePort.Close");
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
