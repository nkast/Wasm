using System;
using nkast.Wasm.ChannelMessaging;

namespace nkast.Wasm.Audio
{
    public class AudioWorkletNode : AudioNode
    {
        MessagePort _messagePort;

        public MessagePort Port
        {
            get { return _messagePort; }
        }

        internal AudioWorkletNode(int uid, BaseAudioContext context) : base(uid, context)
        {
            int mpuid = InvokeRetInt("nkAudioWorkletNode.GetPort");
            _messagePort = new MessagePort(mpuid);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            _messagePort = null;

            base.Dispose(disposing);
        }
    }
}
