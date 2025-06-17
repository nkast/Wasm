using System;
using nkast.Wasm.ChannelMessaging;

namespace nkast.Wasm.Audio
{
    public class AudioWorkletNode : AudioNode
    {
        public MessagePort Port
        {
            get
            {
                int uid = InvokeRetInt("nkAudioWorkletNode.GetPort");
                MessagePort port = MessagePort.FromUid(uid);
                if (port != null)
                    return port;

                return new MessagePort(uid);
            }
        }

        internal AudioWorkletNode(int uid, BaseAudioContext context) : base(uid, context)
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
