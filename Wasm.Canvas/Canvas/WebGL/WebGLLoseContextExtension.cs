namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGLLoseContextExtension : WebGLExtension
    {
        public WebGLLoseContextExtension(int uid) : base(uid)
        {
        }

        public void LoseContext()
        {
            Invoke(RegisterFunction("nkCanvasLoseContextExtension.LoseContext"));
        }

        public void RestoreContext()
        {
            Invoke(RegisterFunction("nkCanvasLoseContextExtension.RestoreContext"));
        }

    }
}
