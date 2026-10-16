using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public class Document : JSObject
    {
        private readonly int _fid_GetTitle;
        private readonly int _fid_SetTitle;
        private readonly int _fid_GetElementById;
        private readonly int _fid_HasFocus;

        private readonly Window _window;
        private readonly Dictionary<string, WeakReference<JSObject>> _elementsCache = new Dictionary<string, WeakReference<JSObject>>();

        public Window DefaultView { get { return _window; } }

        public string Title
        {
            get { return InvokeRetString(_fid_GetTitle); }
            set { Invoke(_fid_SetTitle, value); }
        }

        internal Document(Window window, int uid) : base(uid)
        {
            _fid_GetTitle = RegisterFunction("nkDocument.GetTitle");
            _fid_SetTitle = RegisterFunction("nkDocument.SetTitle");
            _fid_GetElementById = RegisterFunction("nkDocument.GetElementById");
            _fid_HasFocus = RegisterFunction("nkDocument.HasFocus");
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

            int uid = InvokeRetInt<string>(_fid_GetElementById, id);
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
            return InvokeRetBool(_fid_HasFocus);
        }
    }
}
