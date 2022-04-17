using System;
using System.Collections.Generic;

namespace nkast.Wasm.Dom
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

        protected bool IsDisposed { get; private set; }


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
        }

        #endregion

    }
}
