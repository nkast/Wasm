using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace nkast.Wasm.Dom
{
    [SupportedOSPlatform("browser")]
    public partial class JSInterop_NKJSObject
    {
        [JSImport("globalThis.nkJSObject.GetWindow")]
        internal static partial int GetWindow();

        [JSImport("globalThis.nkJSObject.DisposeObject")]
        internal static partial void DisposeObject(int uid);
    }

    [SupportedOSPlatform("browser")]
    public partial class JSInterop_NKWindow
    {
        [JSImport("window.nkWindow.GetDocument")]
        internal static partial int GetDocument(int uid);

        [JSImport("window.nkWindow.GetInnerWidth")]
        internal static partial int GetInnerWidth(int uid);

        [JSImport("window.nkWindow.GetInnerHeight")]
        internal static partial int GetInnerHeight(int uid);

        [JSImport("window.nkWindow.RegisterEvents")]
        internal static partial void RegisterEvents();
    }

    [SupportedOSPlatform("browser")]
    public partial class JSInterop_NkDocument
    {
        [JSImport("globalThis.nkDocument.GetElementById")]
        internal static partial int GetElementById(int uid, string elementId);

        [JSImport("globalThis.nkDocument.GetTitle")]
        internal static partial string GetTitle(int uid);

        [JSImport("globalThis.nkDocument.SetTitle")]
        internal static partial string SetTitle(int uid, string title);
    }
}
