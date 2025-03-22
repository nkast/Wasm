using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Input
{
    public class GamepadArray : CachedJSObject<GamepadArray>
        , IReadOnlyCollection<Gamepad>
        , IReadOnlyList<Gamepad>
    {
        internal GamepadArray(int uid) : base(uid)
        {
        }

        #region IReadOnlyList

        public Gamepad this[int index]
        {
            get
            {
                int uid = InvokeRetInt<int>("nkJSArray.GetItem", index);
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
                int count = InvokeRetInt("nkJSArray.GetLength");
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
