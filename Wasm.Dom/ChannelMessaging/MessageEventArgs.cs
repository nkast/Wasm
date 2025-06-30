using System;

namespace nkast.Wasm.ChannelMessaging
{
    public class MessageEventArgs
    {
        public readonly double DataFloat64;
        public readonly byte[] DataByteArray;

        internal MessageEventArgs(double data)
        {
            this.DataFloat64 = data;
        }

        internal MessageEventArgs(JSUInt8Array data)
        {
            this.DataFloat64 = double.NaN;
            this.DataByteArray = data.ToArray();
        }
    }
}
