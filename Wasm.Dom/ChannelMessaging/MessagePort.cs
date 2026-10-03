using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.ChannelMessaging
{
    public class MessagePort : CachedJSObject<MessagePort>
    {
        private readonly int _fid_RegisterEvents;
        private readonly int _fid_Start;
        private readonly int _fid_Close;
        private readonly int _fid_PostMessagei;
        private readonly int _fid_PostMessagef64;
        private readonly int _fid_PostMessageUInt8Array;
        private readonly int _fid_UnregisterEvents;

        public event EventHandler<MessageEventArgs> Message;

        public MessagePort(int uid) : base(uid)
        {
            _fid_RegisterEvents = RegisterFunction("nkMessagePort.RegisterEvents");
            _fid_Start = RegisterFunction("nkMessagePort.Start");
            _fid_Close = RegisterFunction("nkMessagePort.Close");
            _fid_PostMessagei = RegisterFunction("nkMessagePort.PostMessagei");
            _fid_PostMessagef64 = RegisterFunction("nkMessagePort.PostMessagef64");
            _fid_PostMessageUInt8Array = RegisterFunction("nkMessagePort.PostMessageUInt8Array");
            _fid_UnregisterEvents = RegisterFunction("nkMessagePort.UnregisterEvents");
            Invoke(_fid_RegisterEvents);
        }

        public void Start()
        {
            Invoke(_fid_Start);
        }

        public void close()
        {
            Invoke(_fid_Close);
        }

        public void PostMessage(int message)
        {
            Invoke<int>(_fid_PostMessagei, message);
        }

        public void PostMessage(double message)
        {
            Invoke<double>(_fid_PostMessagef64, message);
        }

        public void PostMessage(byte[] message)
        {
            PostMessage(message, 0, message.Length);
        }

        public unsafe void PostMessage(byte[] message, int index, int count)
        {
            fixed (byte* pMessage = message)
            {
                Invoke(_fid_PostMessageUInt8Array, (int)pMessage, index, count);
            }
        }

        public unsafe void PostMessage(Span<byte> message)
        {
            fixed (byte* pMessage = message)
            {
                Invoke(_fid_PostMessageUInt8Array, (int)pMessage, 0, message.Length);
            }
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
                var handler = mp.Message;
                if (handler != null)
                    handler(mp, new MessageEventArgs(jsarray));
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

            Invoke(_fid_UnregisterEvents);

            base.Dispose(disposing);
        }
    }
}
