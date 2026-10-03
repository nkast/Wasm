namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGLLoseContextExtension : WebGLExtension
    {
        private readonly int _fid_LoseContext;
        private readonly int _fid_RestoreContext;

        public WebGLLoseContextExtension(int uid) : base(uid)
        {
            _fid_LoseContext = RegisterFunction("nkCanvasLoseContextExtension.LoseContext");
            _fid_RestoreContext = RegisterFunction("nkCanvasLoseContextExtension.RestoreContext");
        }

        public void LoseContext()
        {
            Invoke(_fid_LoseContext);
        }

        public void RestoreContext()
        {
            Invoke(_fid_RestoreContext);
        }

    }
}
