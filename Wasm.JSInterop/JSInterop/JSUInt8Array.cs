using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.ChannelMessaging
{
    public class JSUInt8Array : JSObject
    {
        public int Count
        {
            get { return InvokeRetInt("nkJSUInt8Array.GetLength"); }
        }

        public JSUInt8Array(int uid) : base(uid)
        {
        }

        public void CopyTo(byte[] bytes, int destinationIndex, int count)
        {
            CopyTo(0, bytes, destinationIndex, count);
        }

        public void CopyTo(int sourceIndex, byte[] bytes, int destinationIndex, int count)
        {
            InvokeRetInt("nkJSUInt8Array.CopyTo", sourceIndex, destinationIndex, count, bytes);
        }

        public byte[] ToArray()
        {
            byte[] bytes = new byte[Count];
            this.CopyTo(0, bytes, 0, Count);
            return bytes;
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
