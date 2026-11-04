using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using nkast.Wasm.JSInterop;

namespace nkast.Wasm.Dom
{
    public class Document : JSObject
    {
        private readonly int _fid_GetTitle;
        private readonly int _fid_SetTitle;
        private readonly int _fid_GetFullscreenEnabled;
        private readonly int _fid_GetFullscreenElementUid;
        private readonly int _fid_GetElementById;
        private readonly int _fid_RegisterEvents;
        private readonly int _fid_HasFocus;
        private readonly int _fid_ExitFullscreen;
        private readonly int _fid_UnregisterEvents;

        private readonly Window _window;
        private readonly Dictionary<string, WeakReference<JSObject>> _elementsCache = new Dictionary<string, WeakReference<JSObject>>();

        public delegate void OnFullscreenChangeDelegate(object sender);

        public OnFullscreenChangeDelegate OnFullscreenChange;
        public OnFullscreenChangeDelegate OnFullscreenError;

        public Window DefaultView { get { return _window; } }

        public string Title
        {
            get { return InvokeRetString(_fid_GetTitle); }
            set { Invoke(_fid_SetTitle, value); }
        }

        public bool FullscreenEnabled
        {
            get { return InvokeRetBool(_fid_GetFullscreenEnabled); }
        }

        public int FullscreenElementUid
        {
            get { return InvokeRetInt(_fid_GetFullscreenElementUid); }
        }

        internal Document(Window window, int uid) : base(uid)
        {
            _fid_GetTitle = RegisterFunction("nkDocument.GetTitle");
            _fid_SetTitle = RegisterFunction("nkDocument.SetTitle");
            _fid_GetFullscreenEnabled = RegisterFunction("nkDocument.GetFullscreenEnabled");
            _fid_GetFullscreenElementUid = RegisterFunction("nkDocument.GetFullscreenElementUid");
            _fid_GetElementById = RegisterFunction("nkDocument.GetElementById");
            _fid_RegisterEvents = RegisterFunction("nkDocument.RegisterEvents");
            _fid_HasFocus = RegisterFunction("nkDocument.HasFocus");
            _fid_ExitFullscreen = RegisterFunction("nkDocument.ExitFullscreen");
            _fid_UnregisterEvents = RegisterFunction("nkDocument.UnregisterEvents");
            _window = window;
            Invoke(_fid_RegisterEvents);
        }

        private static Document DocumentFromUid(int uid)
        {
            if (Window.Current.Document.Uid != uid)
                throw new InvalidOperationException("Invalid uid");
            return Window.Current.Document;
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

        public Task ExitFullscreenAsync()
        {
            int uid = InvokeRetInt(_fid_ExitFullscreen);

            PromiseVoid promise = new PromiseVoid(uid);
            return promise.GetTask();
        }

        [JSInvokable]
        public static void JsDocumentOnFullscreenChange(int uid)
        {
            Document document = DocumentFromUid(uid);
            var handler = document.OnFullscreenChange;
            if (handler != null)
                handler(document);
        }

        [JSInvokable]
        public static void JsDocumentOnFullscreenError(int uid)
        {
            Document document = DocumentFromUid(uid);
            var handler = document.OnFullscreenError;
            if (handler != null)
                handler(document);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

            Invoke(_fid_UnregisterEvents);

            base.Dispose(disposing);
        }
    }
}
