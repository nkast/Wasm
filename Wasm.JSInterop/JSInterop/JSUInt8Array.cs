using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.ChannelMessaging
{
    public class JSUInt8Array : JSObject
    {
        private readonly int _fid_GetLength;
        private readonly int _fid_CopyTo;

        public int Count
        {
            get { return InvokeRetInt(_fid_GetLength); }
        }

        public JSUInt8Array(int uid) : base(uid)
        {
            _fid_GetLength = RegisterFunction("nkJSUInt8Array.GetLength");
            _fid_CopyTo = _fid_CopyTo;
        }

        public void CopyTo(byte[] bytes, int destinationIndex, int count)
        {
            CopyTo(0, bytes, destinationIndex, count);
        }

        public unsafe void CopyTo(int sourceIndex, byte[] bytes, int destinationIndex, int count)
        {
            fixed (byte* pBytes = bytes)
            {
                InvokeRetInt(_fid_CopyTo, sourceIndex, (int)pBytes, destinationIndex, count);
            }
        }

        public unsafe void CopyTo(Span<byte> bytes)
        {
            CopyTo(0, bytes);
        }

        public unsafe void CopyTo(int sourceIndex, Span<byte> bytes)
        {
            fixed (byte* pBytes = bytes)
            {
                InvokeRetInt(_fid_CopyTo, sourceIndex, (int)pBytes, 0, bytes.Length);
            }
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
