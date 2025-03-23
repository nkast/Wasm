using System;
using System.Collections.Generic;
using System.Globalization;

namespace nkast.Wasm.Dom
{
    public class Div : JSObject
    {
        internal Div(int uid) : base(uid)
        {
        }

        public int ClientWidth
        {
            get { return InvokeRetInt("nkElement.GetClientWidth"); }
        }

        public int ClientHeight
        {
            get { return InvokeRetInt("nkElement.GetClientHeight"); }
        }
    }
}
