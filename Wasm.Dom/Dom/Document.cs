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

        private TElement FromId<TElement>(string id) where TElement : JSObject
        {
            if (_elementsCache.TryGetValue(id, out WeakReference<JSObject> elementRef))
            {
                if (elementRef.TryGetTarget(out JSObject jsObj))
                    return (TElement)jsObj;
                else
                    _elementsCache.Remove(id);
            }

            return null;
        }

        public TElement GetElementById<TElement>(string id)
            where TElement : JSObject
        {
            TElement element = FromId<TElement>(id);
            if (element != null)
                return element;

            int uid = InvokeRet<string, int>("nkDocument.GetElementById", id);
            if (uid != -1)
            {
                element = CreateInstance<TElement>(uid);
                _elementsCache.Add(id, new WeakReference<JSObject>(element));
                return (TElement)element;
            }

            return null;
        }

        protected static TElement CreateInstance<TElement>(int uid)
            where TElement : JSObject
        {   
            return (TElement)Activator.CreateInstance(
                typeof(TElement),
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new object[] { uid },
                null);
        }

        public bool HasFocus()
        {
            return InvokeRetBool("nkDocument.HasFocus");
        }
    }
}
