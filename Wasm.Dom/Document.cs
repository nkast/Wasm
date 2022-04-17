using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Microsoft.JSInterop.WebAssembly;

namespace nkast.Wasm.Dom
{
    public class Document : JSObject
    {
        private readonly Window _window;
        private readonly Dictionary<string, WeakReference<JSObject>> _elementsCache = new Dictionary<string, WeakReference<JSObject>>();

        public Window DefaultView { get { return _window; } }

        public string Title
        {
            get { return InvokeRet<string>("nkDocument.GetTitle"); }
            set { Invoke("nkDocument.SetTitle", value); }
        }

        internal Document(Window window, int uid) : base(uid)
        {
            _window = window;
        }

        public TElement GetElementById<TElement>(string id)
            where TElement : JSObject
        {
            JSObject element = null;
            WeakReference<JSObject> refElement;
            if (_elementsCache.TryGetValue(id, out refElement))
            {
                if (!refElement.TryGetTarget(out element))
                    _elementsCache.Remove(id);
            }
            
            if (element == null)
            {
                int uid = InvokeRet<string, int>("nkDocument.GetElementById", id);
                if (uid != -1)
                {
                    element = CreateInstance<TElement>(uid);
                    _elementsCache.Add(id, new WeakReference<JSObject>(element));
                }
            }

            return (TElement)element;
        }

        protected static JSObject CreateInstance<TElement>(int uid)
            where TElement : JSObject
        {   
            return (TElement)Activator.CreateInstance(
                typeof(TElement),
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new object[] { uid },
                null);
        }
    }

    public class Div : JSObject
    {
        internal Div(int uid) : base(uid)
        {
        }
    }

}
