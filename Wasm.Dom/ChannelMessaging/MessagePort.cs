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

        public void PostMessage(double message)
        {
            Invoke<double>("nkMessagePort.PostMessagef64", message);
        }

        public void PostMessage(byte[] message)
        {
            Invoke<byte[]>("nkMessagePort.PostMessageUInt8Array", message);
        }


        [JSInvokable]
        public static void JsMessagePortOnMessagef64(int uid, double data)
        {
            MessagePort mp = MessagePort.FromUid(uid);
            
            var handler = mp.Message;
            if (handler != null)
                handler(mp, new MessageEventArgs(data));
        }

        [JSInvokable]
        public static void JsMessagePortOnMessageUInt8Array(int uid, int aid)
        {
            MessagePort mp = MessagePort.FromUid(uid);

            JSUInt8Array jsarray = new JSUInt8Array(aid);
            try
            {
                int count = jsarray.Count;

                byte[] bytes = new byte[count];
                jsarray.CopyTo(bytes, 0, count);

                var handler = mp.Message;
                if (handler != null)
                    handler(mp, new MessageEventArgs(bytes));
            }
            finally
            {
                jsarray.Dispose();
            }
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
