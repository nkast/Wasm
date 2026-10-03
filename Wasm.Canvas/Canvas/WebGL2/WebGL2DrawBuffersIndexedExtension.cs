namespace nkast.Wasm.Canvas.WebGL
{
    public class WebGL2DrawBuffersIndexedExtension : WebGLExtension
    {
        private readonly int _fid_Enablei;
        private readonly int _fid_Disablei;
        private readonly int _fid_BlendEquationSeparatei;
        private readonly int _fid_BlendFuncSeparatei;
        private readonly int _fid_ColorMaski;

        internal WebGL2DrawBuffersIndexedExtension(int uid) : base(uid)
        {
            _fid_Enablei = RegisterFunction("nkCanvasDrawBuffersIndexedExtension.Enablei");
            _fid_Disablei = RegisterFunction("nkCanvasDrawBuffersIndexedExtension.Disablei");
            _fid_BlendEquationSeparatei = RegisterFunction("nkCanvasDrawBuffersIndexedExtension.BlendEquationSeparatei");
            _fid_BlendFuncSeparatei = RegisterFunction("nkCanvasDrawBuffersIndexedExtension.BlendFuncSeparatei");
            _fid_ColorMaski = RegisterFunction("nkCanvasDrawBuffersIndexedExtension.ColorMaski");
        }

        public void Enablei(WebGLCapability cap, int index)
        {
            Invoke(_fid_Enablei, (int)cap, index);
        }

        public void Disablei(WebGLCapability cap, int index)
        {
            Invoke(_fid_Disablei, (int)cap, index);
        }

        public void BlendEquationSeparatei(int buffer, WebGLEquationFunc modeRGB, WebGLEquationFunc modeAlpha)
        {
            Invoke(_fid_BlendEquationSeparatei, buffer, (int)modeRGB, (int)modeAlpha);
        }

        public void BlendFuncSeparatei(int buffer, WebGLBlendFunc srcRGB, WebGLBlendFunc dstRGB, WebGLBlendFunc srcAlpha, WebGLBlendFunc dstAlpha)
        {
            Invoke(_fid_BlendFuncSeparatei, buffer, (int)srcRGB, (int)dstRGB, (int)srcAlpha, (int)dstAlpha);
        }

        public void ColorMaski(int buffer, bool red, bool green, bool blue, bool alpha)
        {
            Invoke(_fid_ColorMaski, buffer, red ? 1 : 0, green ? 1 : 0, blue ? 1 : 0, alpha ? 1 : 0);
        }
    }
}
