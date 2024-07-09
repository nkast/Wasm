using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Microsoft.JSInterop.WebAssembly;

namespace nkast.Wasm.Dom
{
    public class Navigator : JSObject
    {
        private readonly Window _window;

        public string UserAgent
        {
            get { return InvokeRet<string>("nkNavigator.GetUserAgent"); }
        }

        public int MaxTouchPoints
        {
            get { return InvokeRet<int>("nkNavigator.GetMaxTouchPoints"); }
        }

        internal Navigator(Window window, int uid) : base(uid)
        {
            _window = window;
        }

    }
}
