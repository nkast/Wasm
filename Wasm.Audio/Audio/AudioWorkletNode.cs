using System;
using nkast.Wasm.ChannelMessaging;

namespace nkast.Wasm.Audio
{
    public class AudioWorkletNode : AudioNode
    {
        MessagePort _messagePort;
        AudioParamMap _parameters;

        public MessagePort Port
        {
            get { return _messagePort; }
        }

        public AudioParamMap Parameters
        {
            get { return _parameters; }
        }
        

        internal AudioWorkletNode(int uid, BaseAudioContext context) : base(uid, context)
        {
            int mpuid = InvokeRetInt("nkAudioWorkletNode.GetPort");
            _messagePort = new MessagePort(mpuid);

            int pmuid = InvokeRetInt("nkAudioWorkletNode.GetParameters");
            _parameters = new AudioParamMap(pmuid, this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_messagePort != null)
                {
                    _messagePort.Dispose();
                    _messagePort = null;
                }
                if (_parameters != null)
                {
                    _parameters.Dispose();
                    _parameters = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}
