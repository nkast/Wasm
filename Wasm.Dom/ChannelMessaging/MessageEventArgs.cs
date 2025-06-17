using System;

namespace nkast.Wasm.ChannelMessaging
{
    public class MessageEventArgs
    {
        public readonly int Data;

        internal MessageEventArgs(int data)
        {
            this.Data = data;
        }
    }
}
