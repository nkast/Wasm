using System;
using System.Collections.Generic;

namespace nkast.Wasm.XR
{
    public class InputSourcesChangedEventArgs : EventArgs
    {
        public static new readonly InputSourcesChangedEventArgs Empty = new InputSourcesChangedEventArgs();

        internal InputSourcesChangedEventArgs()
        {
        }
    }
}