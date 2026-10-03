namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGL2DrawBuffersIndexedExtension : WebGLExtension
    {
        internal WebGL2DrawBuffersIndexedExtension(int uid) : base(uid)
        {
        }

        public void Enablei(WebGLCapability cap, int index)
        {
            Invoke("nkCanvasDrawBuffersIndexedExtension.Enablei", (int)cap, index);
        }

        public void Disablei(WebGLCapability cap, int index)
        {
            Invoke("nkCanvasDrawBuffersIndexedExtension.Disablei", (int)cap, index);
        }

        public void BlendEquationSeparatei(int buffer, WebGLEquationFunc modeRGB, WebGLEquationFunc modeAlpha)
        {
            Invoke("nkCanvasDrawBuffersIndexedExtension.BlendEquationSeparatei", buffer, (int)modeRGB, (int)modeAlpha);
        }

        public void BlendFuncSeparatei(int buffer, WebGLBlendFunc srcRGB, WebGLBlendFunc dstRGB, WebGLBlendFunc srcAlpha, WebGLBlendFunc dstAlpha)
        {
            Invoke("nkCanvasDrawBuffersIndexedExtension.BlendFuncSeparatei", buffer, (int)srcRGB, (int)dstRGB, (int)srcAlpha, (int)dstAlpha);
        }

        public void ColorMaski(int buffer, bool red, bool green, bool blue, bool alpha)
        {
            Invoke("nkCanvasDrawBuffersIndexedExtension.ColorMaski", buffer, red ? 1 : 0, green ? 1 : 0, blue ? 1 : 0, alpha ? 1 : 0);
        }
    }
}
