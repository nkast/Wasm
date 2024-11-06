using System;
using System.Collections;
using System.Collections.Generic;
using nkast.Wasm.Dom;

namespace nkast.Wasm.Input
{
    public class GamepadArray : JSObject
        , IReadOnlyCollection<Gamepad>
        , IReadOnlyList<Gamepad>
    {
        static Dictionary<int, WeakReference<JSObject>> _uidMap = new Dictionary<int, WeakReference<JSObject>>();

        internal GamepadArray(int uid) : base(uid)
        {
            _uidMap.Add(Uid, new WeakReference<JSObject>(this, true));

        }

        internal static GamepadArray FromUid(int uid)
        {
            if (GamepadArray._uidMap.TryGetValue(uid, out WeakReference<JSObject> jsObjRef))
                if (jsObjRef.TryGetTarget(out JSObject jsObj))
                    return (GamepadArray)jsObj;

            return null;
        }

        #region IReadOnlyList

        public Gamepad this[int index]
        {
            get
            {
                int uid = InvokeRet<int, int>("nkJSArray.GetItem", index);
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
                int count = InvokeRet<int>("nkJSArray.GetLength");
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

            _uidMap.Remove(Uid);

            base.Dispose(disposing);
        }
    }
}
