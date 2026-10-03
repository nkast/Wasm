namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGLPolygonModeExtension : WebGLExtension
    {
        internal WebGLPolygonModeExtension(int uid) : base(uid)
        {
        }

        public void PolygonMode(WebGLCullFaceMode face, WebGLPolygonMode mode)
        {
            Invoke("nkCanvasPolygonModeExtension.PolygonMode", (int)face, (int)mode);
        }
    }
}
