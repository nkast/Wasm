using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace nkast.Wasm.Dom
{
    [SupportedOSPlatform("browser")]
    public partial class JSInterop_NKCanvas
    {
        [JSImport("globalThis.nkCanvas.GetWidth")]
        internal static partial int GetWidth(int uid);

        [JSImport("globalThis.nkCanvas.SetWidth")]
        internal static partial int SetWidth(int uid, int width);

        [JSImport("globalThis.nkCanvas.GetHeight")]
        internal static partial int GetHeight(int uid);

        [JSImport("globalThis.nkCanvas.SetHeight")]
        internal static partial int SetHeight(int uid, int height);

        [JSImport("globalThis.nkCanvas.Create2DContext")]
        internal static partial int Create2DContext(int uid);

        [JSImport("globalThis.nkCanvas.CreateWebGLContext")]
        internal static partial int CreateWebGLContext(int uid);
    }

    [SupportedOSPlatform("browser")]
    public partial class JSInterop_NKCanvas2dContext
    {
        [JSImport("globalThis.nkCanvas2dContext.GetFillStyle")]
        internal static partial int GetFillStyle(int uid);

        [JSImport("globalThis.nkCanvas2dContext.SetFillStyle")]
        internal static partial void SetFillStyle(int uid, string fillStyle);


        [JSImport("globalThis.nkCanvas2dContext.GetStrokeStyle")]
        internal static partial int GetStrokeStyle(int uid);

        [JSImport("globalThis.nkCanvas2dContext.SetStrokeStyle")]
        internal static partial int SetStrokeStyle(int uid, string strokeStyle);


        [JSImport("globalThis.nkCanvas2dContext.GetFont")]
        internal static partial int GetFont(int uid);

        [JSImport("globalThis.nkCanvas2dContext.SetFont")]
        internal static partial int SetFont(int uid, string font);

        [JSImport("globalThis.nkCanvas2dContext.GetFillStyle")]
        internal static partial int GetFillStyle(int uid);

        [JSImport("globalThis.nkCanvas2dContext.GetFillStyle")]
        internal static partial int GetFillStyle(int uid);

        [JSImport("globalThis.nkCanvas2dContext.GetFillStyle")]
        internal static partial int GetFillStyle(int uid);

        [JSImport("globalThis.nkCanvas2dContext.GetFillStyle")]
        internal static partial int GetFillStyle(int uid);

        [JSImport("globalThis.nkCanvas2dContext.GetFillStyle")]
        internal static partial int GetFillStyle(int uid);

        [JSImport("globalThis.nkCanvas2dContext.GetFillStyle")]
        internal static partial int GetFillStyle(int uid);

        [JSImport("globalThis.nkCanvas2dContext.GetFillStyle")]
        internal static partial int GetFillStyle(int uid);

        [JSImport("globalThis.nkCanvas2dContext.GetFillStyle")]
        internal static partial int GetFillStyle(int uid);

        [JSImport("globalThis.nkCanvas2dContext.GetFillStyle")]
        internal static partial int GetFillStyle(int uid);

        [JSImport("globalThis.nkCanvas2dContext.GetFillStyle")]
        internal static partial int GetFillStyle(int uid);

        [JSImport("globalThis.nkCanvas2dContext.GetFillStyle")]
        internal static partial int GetFillStyle(int uid);
    }
}
