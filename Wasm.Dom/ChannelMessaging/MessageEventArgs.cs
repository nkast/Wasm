using System;

namespace nkast.Wasm.ChannelMessaging
{
    public class MessageEventArgs
    {
        public readonly double DataFloat64;

        internal MessageEventArgs(double data)
        {
            this.DataFloat64 = data;
        }
    }
}
