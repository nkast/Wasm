using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace nkast.Wasm.JSInterop
{
    public partial class JSObject : IDisposable
    {
        public int Uid { get; private set; }

        public JSObject(int uid)
        {
            if (uid >= 0)
            {
                Uid = uid;
            }
            else
            {
                if (uid == -1)
                    throw new NullReferenceException();
                throw new InvalidOperationException();
            }
        }

        #region implement IDisposable

        private bool _isDisposed;


        ~JSObject()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {

                }

                Invoke("nkJSObject.DisposeObject", Uid);
                Uid = -1;

                _isDisposed = true;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("JSObject is allready disposed.");
            }
        }

        [Conditional("DEBUG")]
        protected void ThrowIfDisposed()
        {
            if (!_isDisposed)
                return;

            throw new ObjectDisposedException("Object is Disposed.");
        }

        #endregion

        public override string ToString()
        {
            return String.Format("{{Uid:{0} }}", Uid);
        }

    }
}
