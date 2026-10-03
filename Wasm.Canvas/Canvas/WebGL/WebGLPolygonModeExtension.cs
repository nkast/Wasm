namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGLPolygonModeExtension : WebGLExtension
    {
        private readonly int _fid_PolygonMode;

        internal WebGLPolygonModeExtension(int uid) : base(uid)
        {
            _fid_PolygonMode = RegisterFunction("nkCanvasPolygonModeExtension.PolygonMode");
        }

        public void PolygonMode(WebGLCullFaceMode face, WebGLPolygonMode mode)
        {
            Invoke(_fid_PolygonMode, (int)face, (int)mode);
        }
    }
}
