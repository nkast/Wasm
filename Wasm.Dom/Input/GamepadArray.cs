using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Input
{
    public class GamepadArray : CachedJSObject<GamepadArray>
        , IReadOnlyCollection<Gamepad>
        , IReadOnlyList<Gamepad>
    {
        private readonly int _fid_GetItem;
        private readonly int _fid_GetLength;

        internal GamepadArray(int uid) : base(uid)
        {
            _fid_GetItem = RegisterFunction("nkJSArray.GetItem");
            _fid_GetLength = RegisterFunction("nkJSArray.GetLength");
        }

        #region IReadOnlyList

        public Gamepad this[int index]
        {
            get
            {
                int uid = InvokeRetInt<int>(_fid_GetItem, index);
                Gamepad gamepad = Gamepad.FromUid(uid);
                if (gamepad != null)
                    return gamepad;

                if (uid == -1)
                    return null;

                return new Gamepad(uid);
            }
        }

        #endregion IReadOnlyList

        #region ICollection

        public int Count
        {
            get
            {
                int count = InvokeRetInt(_fid_GetLength);
                return count;
            }
        }

        #endregion ICollection


        #region IEnumerable

        IEnumerator<Gamepad> IEnumerable<Gamepad>.GetEnumerator()
        {
            return new JSArrayEnumerator<Gamepad>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Gamepad>)this).GetEnumerator();
        }

        #endregion IEnumerable


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            base.Dispose(disposing);
        }
    }
}
