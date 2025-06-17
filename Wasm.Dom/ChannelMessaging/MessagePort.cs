using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.ChannelMessaging
{
    public class MessagePort : CachedJSObject<MessagePort>
    {
        public event EventHandler<MessageEventArgs> Message;

        public MessagePort(int uid) : base(uid)
        {
            Invoke("nkMessagePort.RegisterEvents");
        }

        public void Start()
        {
            Invoke("nkMessagePort.Start");
        }

        public void close()
        {
            Invoke("nkMessagePort.Close");
        }

        public void PostMessage(int message)
        {
            Invoke<int>("nkMessagePort.PostMessagei", message);
        }

        [JSInvokable]
        public static void JsMessagePortOnMessagei(int uid, int data)
        {
            MessagePort mp = MessagePort.FromUid(uid);
            
            var handler = mp.Message;
            if (handler != null)
                handler(mp, new MessageEventArgs(data));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Invoke("nkMessagePort.UnregisterEvents");

            base.Dispose(disposing);
        }
    }
}
