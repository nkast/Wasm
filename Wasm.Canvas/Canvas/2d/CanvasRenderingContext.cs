using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace nkast.Wasm.Canvas
{
    internal class CanvasRenderingContext : RenderingContext, ICanvasRenderingContext
    {
        private readonly int _fid_GetFillStyle;
        private readonly int _fid_SetFillStyle;
        private readonly int _fid_GetStrokeStyle;
        private readonly int _fid_SetStrokeStyle;
        private readonly int _fid_GetFont;
        private readonly int _fid_SetFont;
        private readonly int _fid_GetTextAlign;
        private readonly int _fid_SetTextAlign;
        private readonly int _fid_GetTextBaseline;
        private readonly int _fid_SetTextBaseline;
        private readonly int _fid_GetLineWidth;
        private readonly int _fid_SetLineWidth;
        private readonly int _fid_GetLineCap;
        private readonly int _fid_SetLineCap;
        private readonly int _fid_GetMiterLimit;
        private readonly int _fid_SetMiterLimit;
        private readonly int _fid_GetGlobalAlpha;
        private readonly int _fid_SetGlobalAlpha;
        private readonly int _fid_GetGlobalCompositeOperation;
        private readonly int _fid_SetGlobalCompositeOperation;
        private readonly int _fid_GetImageSmoothingEnabled;
        private readonly int _fid_SetImageSmoothingEnabled;
        private readonly int _fid_GetShadowBlur;
        private readonly int _fid_SetShadowBlur;
        private readonly int _fid_GetShadowColor;
        private readonly int _fid_SetShadowColor;
        private readonly int _fid_GetShadowOffsetX;
        private readonly int _fid_SetShadowOffsetX;
        private readonly int _fid_GetShadowOffsetY;
        private readonly int _fid_SetShadowOffsetY;
        private readonly int _fid_ClearRect;
        private readonly int _fid_FillRect;
        private readonly int _fid_StrokeRect;
        private readonly int _fid_DrawImage;
        private readonly int _fid_DrawImage1;
        private readonly int _fid_DrawImage2;
        private readonly int _fid_FillText;
        private readonly int _fid_FillText1;
        private readonly int _fid_StrokeText;
        private readonly int _fid_StrokeText1;
        private readonly int _fid_MeasureText;
        private readonly int _fid_GetLineDash;
        private readonly int _fid_SetLineDash;
        private readonly int _fid_BeginPath;
        private readonly int _fid_ClosePath;
        private readonly int _fid_IsPointInPath;
        private readonly int _fid_IsPointInStroke;
        private readonly int _fid_MoveTo;
        private readonly int _fid_LineTo;
        private readonly int _fid_BezierCurveTo;
        private readonly int _fid_QuadraticCurveTo;
        private readonly int _fid_Arc;
        private readonly int _fid_ArcTo;
        private readonly int _fid_Rect;
        private readonly int _fid_Ellipse;
        private readonly int _fid_Fill;
        private readonly int _fid_Stroke;
        private readonly int _fid_Clip;
        private readonly int _fid_Rotate;
        private readonly int _fid_Scale;
        private readonly int _fid_Translate;
        private readonly int _fid_Transform;
        private readonly int _fid_SetTransform;
        private readonly int _fid_GetTransform;
        private readonly int _fid_Save;
        private readonly int _fid_Restore;
        private readonly int _fid_DisposeObject;

        internal CanvasRenderingContext(Canvas canvas, int uid) : base(canvas, uid)
        {
            _fid_GetFillStyle = RegisterFunction("nkCanvas2dContext.GetFillStyle");
            _fid_SetFillStyle = RegisterFunction("nkCanvas2dContext.SetFillStyle");
            _fid_GetStrokeStyle = RegisterFunction("nkCanvas2dContext.GetStrokeStyle");
            _fid_SetStrokeStyle = RegisterFunction("nkCanvas2dContext.SetStrokeStyle");
            _fid_GetFont = RegisterFunction("nkCanvas2dContext.GetFont");
            _fid_SetFont = RegisterFunction("nkCanvas2dContext.SetFont");
            _fid_GetTextAlign = RegisterFunction("nkCanvas2dContext.GetTextAlign");
            _fid_SetTextAlign = RegisterFunction("nkCanvas2dContext.SetTextAlign");
            _fid_GetTextBaseline = RegisterFunction("nkCanvas2dContext.GetTextBaseline");
            _fid_SetTextBaseline = RegisterFunction("nkCanvas2dContext.SetTextBaseline");
            _fid_GetLineWidth = RegisterFunction("nkCanvas2dContext.GetLineWidth");
            _fid_SetLineWidth = RegisterFunction("nkCanvas2dContext.SetLineWidth");
            _fid_GetLineCap = RegisterFunction("nkCanvas2dContext.GetLineCap");
            _fid_SetLineCap = RegisterFunction("nkCanvas2dContext.SetLineCap");
            _fid_GetMiterLimit = RegisterFunction("nkCanvas2dContext.GetMiterLimit");
            _fid_SetMiterLimit = RegisterFunction("nkCanvas2dContext.SetMiterLimit");
            _fid_GetGlobalAlpha = RegisterFunction("nkCanvas2dContext.GetGlobalAlpha");
            _fid_SetGlobalAlpha = RegisterFunction("nkCanvas2dContext.SetGlobalAlpha");
            _fid_GetGlobalCompositeOperation = RegisterFunction("nkCanvas2dContext.GetGlobalCompositeOperation");
            _fid_SetGlobalCompositeOperation = RegisterFunction("nkCanvas2dContext.SetGlobalCompositeOperation");
            _fid_GetImageSmoothingEnabled = RegisterFunction("nkCanvas2dContext.GetImageSmoothingEnabled");
            _fid_SetImageSmoothingEnabled = RegisterFunction("nkCanvas2dContext.SetImageSmoothingEnabled");
            _fid_GetShadowBlur = RegisterFunction("nkCanvas2dContext.GetShadowBlur");
            _fid_SetShadowBlur = RegisterFunction("nkCanvas2dContext.SetShadowBlur");
            _fid_GetShadowColor = RegisterFunction("nkCanvas2dContext.GetShadowColor");
            _fid_SetShadowColor = RegisterFunction("nkCanvas2dContext.SetShadowColor");
            _fid_GetShadowOffsetX = RegisterFunction("nkCanvas2dContext.GetShadowOffsetX");
            _fid_SetShadowOffsetX = RegisterFunction("nkCanvas2dContext.SetShadowOffsetX");
            _fid_GetShadowOffsetY = RegisterFunction("nkCanvas2dContext.GetShadowOffsetY");
            _fid_SetShadowOffsetY = RegisterFunction("nkCanvas2dContext.SetShadowOffsetY");
            _fid_ClearRect = RegisterFunction("nkCanvas2dContext.ClearRect");
            _fid_FillRect = RegisterFunction("nkCanvas2dContext.FillRect");
            _fid_StrokeRect = RegisterFunction("nkCanvas2dContext.StrokeRect");
            _fid_DrawImage = RegisterFunction("nkCanvas2dContext.DrawImage");
            _fid_DrawImage1 = RegisterFunction("nkCanvas2dContext.DrawImage1");
            _fid_DrawImage2 = RegisterFunction("nkCanvas2dContext.DrawImage2");
            _fid_FillText = RegisterFunction("nkCanvas2dContext.FillText");
            _fid_FillText1 = RegisterFunction("nkCanvas2dContext.FillText1");
            _fid_StrokeText = RegisterFunction("nkCanvas2dContext.StrokeText");
            _fid_StrokeText1 = RegisterFunction("nkCanvas2dContext.StrokeText1");
            _fid_MeasureText = RegisterFunction("nkCanvas2dContext.MeasureText");
            _fid_GetLineDash = RegisterFunction("nkCanvas2dContext.GetLineDash");
            _fid_SetLineDash = RegisterFunction("nkCanvas2dContext.SetLineDash");
            _fid_BeginPath = RegisterFunction("nkCanvas2dContext.BeginPath");
            _fid_ClosePath = RegisterFunction("nkCanvas2dContext.ClosePath");
            _fid_IsPointInPath = RegisterFunction("nkCanvas2dContext.IsPointInPath");
            _fid_IsPointInStroke = RegisterFunction("nkCanvas2dContext.IsPointInStroke");
            _fid_MoveTo = RegisterFunction("nkCanvas2dContext.MoveTo");
            _fid_LineTo = RegisterFunction("nkCanvas2dContext.LineTo");
            _fid_BezierCurveTo = RegisterFunction("nkCanvas2dContext.BezierCurveTo");
            _fid_QuadraticCurveTo = RegisterFunction("nkCanvas2dContext.QuadraticCurveTo");
            _fid_Arc = RegisterFunction("nkCanvas2dContext.Arc");
            _fid_ArcTo = RegisterFunction("nkCanvas2dContext.ArcTo");
            _fid_Rect = RegisterFunction("nkCanvas2dContext.Rect");
            _fid_Ellipse = RegisterFunction("nkCanvas2dContext.Ellipse");
            _fid_Fill = RegisterFunction("nkCanvas2dContext.Fill");
            _fid_Stroke = RegisterFunction("nkCanvas2dContext.Stroke");
            _fid_Clip = RegisterFunction("nkCanvas2dContext.Clip");
            _fid_Rotate = RegisterFunction("nkCanvas2dContext.Rotate");
            _fid_Scale = RegisterFunction("nkCanvas2dContext.Scale");
            _fid_Translate = RegisterFunction("nkCanvas2dContext.Translate");
            _fid_Transform = RegisterFunction("nkCanvas2dContext.Transform");
            _fid_SetTransform = RegisterFunction("nkCanvas2dContext.SetTransform");
            _fid_GetTransform = RegisterFunction("nkCanvas2dContext.GetTransform");
            _fid_Save = RegisterFunction("nkCanvas2dContext.Save");
            _fid_Restore = RegisterFunction("nkCanvas2dContext.Restore");
            _fid_DisposeObject = RegisterFunction("nkJSObject.DisposeObject");
        }

        public string FillStyle
        {
            get { return InvokeRetString(_fid_GetFillStyle); }
            set { Invoke(_fid_SetFillStyle, value); }
        }

        public string StrokeStyle
        {
            get { return InvokeRetString(_fid_GetStrokeStyle); }
            set { Invoke(_fid_SetStrokeStyle, value); }
        }

        public string Font
        {
            get { return InvokeRetString(_fid_GetFont); }
            set { Invoke(_fid_SetFont, value); }
        }

        public TextAlign TextAlign
        {
            get
            {
                string str = InvokeRetString(_fid_GetTextAlign);
                return Enum.Parse<TextAlign>(str, true);
            }
            set { Invoke(_fid_SetTextAlign, value.ToString().ToLower()); }
        }

        public TextBaseline TextBaseline
        {
            get
            {
                string str = InvokeRetString(_fid_GetTextBaseline);
                return Enum.Parse<TextBaseline>(str, true);
            }
            set { Invoke(_fid_SetTextBaseline, value.ToString().ToLower()); }
        }

        public float LineWidth
        {
            get { return InvokeRetFloat(_fid_GetLineWidth); }
            set { Invoke(_fid_SetLineWidth, value); }
        }

        public LineCap LineCap
        {
            get
            {
                string str = InvokeRetString(_fid_GetLineCap);
                return Enum.Parse<LineCap>(str, true);
            }
            set { Invoke(_fid_SetLineCap, value.ToString().ToLower()); }
        }

        public float MiterLimit
        {
            get { return InvokeRetFloat(_fid_GetMiterLimit); }
            set { Invoke(_fid_SetMiterLimit, value); }
        }

        public float GlobalAlpha
        {
            get { return InvokeRetFloat(_fid_GetGlobalAlpha); }
            set { Invoke(_fid_SetGlobalAlpha, value); }
        }

        public CompositeOperation GlobalCompositeOperation
        {
            get
            {
                string str = InvokeRetString(_fid_GetGlobalCompositeOperation)?.Replace('-', '_');
                return Enum.Parse<CompositeOperation>(str, true);
            }
            set { Invoke(_fid_SetGlobalCompositeOperation, value.ToString().ToLower().Replace('_', '-')); }
        }

        public bool ImageSmoothingEnabled
        {
            get { return InvokeRetBool(_fid_GetImageSmoothingEnabled); }
            set { Invoke(_fid_SetImageSmoothingEnabled, value ? 1 : 0); }
        }

        public float ShadowBlur
        {
            get { return InvokeRetFloat(_fid_GetShadowBlur); }
            set { Invoke(_fid_SetShadowBlur, value); }
        }

        public string ShadowColor
        {
            get { return InvokeRetString(_fid_GetShadowColor); }
            set { Invoke(_fid_SetShadowColor, value); }
        }

        public float ShadowOffsetX
        {
            get { return InvokeRetFloat(_fid_GetShadowOffsetX); }
            set { Invoke(_fid_SetShadowOffsetX, value); }
        }

        public float ShadowOffsetY
        {
            get { return InvokeRetFloat(_fid_GetShadowOffsetY); }
            set { Invoke(_fid_SetShadowOffsetY, value); }
        }

        public void ClearRect(float x, float y, float width, float height)
        {
            Invoke(_fid_ClearRect, x, y, width, height);
        }

        public void FillRect(float x, float y, float width, float height)
        {
            Invoke(_fid_FillRect, x, y, width, height);
        }

        public void StrokeRect(float x, float y, float width, float height)
        {
            Invoke(_fid_StrokeRect, x, y, width, height);
        }

        public void DrawImage(string imgid, float dx, float dy)
        {
            Invoke(_fid_DrawImage, imgid, dx, dy);
        }

        public void DrawImage(string imgid, float dx, float dy, float dwidth, float dheight)
        {
            Invoke(_fid_DrawImage1, imgid, dx, dy, dwidth, dheight);
        }

        public void DrawImage(string imgid, float sx, float sy, float swidth, float sheight, float dx, float dy, float dwidth, float dheight)
        {
            Invoke(_fid_DrawImage2, imgid, sx, sy, swidth, sheight, dx, dy, dwidth, dheight);
        }

        public void FillText(string text, float x, float y)
        {
            Invoke(_fid_FillText, text, x, y);
        }

        public void FillText(string text, float x, float y, float maxWidth)
        {
            Invoke(_fid_FillText1, text, x, y, maxWidth);
        }

        public void StrokeText(string text, float x, float y)
        {            
                Invoke(_fid_StrokeText, text, x, y);
        }

        public void StrokeText(string text, float x, float y, float maxWidth)
        {
            Invoke(_fid_StrokeText1, text, x, y, maxWidth);
        }

        public float MeasureText(string text)
        {
            return InvokeRetFloat<string>(_fid_MeasureText, text);
        }

        private static float[] _emptyLineDash = new float[0];
        public float[] GetLineDash()
        {
            string str = InvokeRetString(_fid_GetLineDash);
            if (string.IsNullOrEmpty(str))
                return _emptyLineDash;

            string[] strs = str.Split(',');
            float[] ret = new float[strs.Length];
            for(int cnt =0; cnt<strs.Length;cnt++)
            {
                ret[cnt] = float.Parse(strs[cnt]);
            }
            return ret;
        }

        public unsafe void SetLineDash(float[] segments)
        {
            fixed (float* pSegments = segments)
            {
                Invoke(_fid_SetLineDash, (int)pSegments, segments.Length);
            }
        }

        public unsafe void SetLineDash(Span<float> segments)
        {
            fixed (float* pSegments = segments)
            {
                Invoke(_fid_SetLineDash, (int)pSegments, segments.Length);
            }
        }

        public void BeginPath()
        {
            Invoke(_fid_BeginPath);
        }

        public void ClosePath()
        {
            Invoke(_fid_ClosePath);
        }

        public bool IsPointInPath(float x, float y, bool evenodd = false)
        {
            return InvokeRetBool<float, float, int>(_fid_IsPointInPath, x, y, evenodd ? 1 : 0);
        }

        public bool IsPointInStroke(float x, float y)
        {
            return InvokeRetBool<float, float>(_fid_IsPointInStroke, x, y);
        }

        public void MoveTo(float x, float y)
        {
            Invoke(_fid_MoveTo, x, y);
        }

        public void LineTo(float x, float y)
        {
            Invoke(_fid_LineTo, x, y);
        }

        public void BezierCurveTo(float cp1X, float cp1Y, float cp2X, float cp2Y, float x, float y)
        {
            Invoke(_fid_BezierCurveTo, cp1X, cp1Y, cp2X, cp2Y, x, y);
        }

        public void QuadraticCurveTo(float cpx, float cpy, float x, float y)
        {
            Invoke(_fid_QuadraticCurveTo, cpx, cpy, x, y);
        }

        public void Arc(float x, float y, float radius, float startAngle, float endAngle, bool anticlockwise = false)
        {
            Invoke(_fid_Arc, x, y, radius, startAngle, endAngle, anticlockwise ? 1 : 0);
        }

        public void ArcTo(float x1, float y1, float x2, float y2, float radius)
        {
            Invoke(_fid_ArcTo, x1, y1, x2, y2, radius);
        }

        public void Rect(float x, float y, float width, float height)
        {
            Invoke(_fid_Rect, x, y, width, height);
        }

        public void Ellipse(float x, float y, float radiusX, float radiusY, float rotation = 0f, float startAngle = 0f, float endAngle = (float)(Math.PI * 2), bool anticlockwise = false)
        {
            Invoke(_fid_Ellipse, x, y, radiusX, radiusY, rotation, startAngle, endAngle, anticlockwise ? 1 : 0);
        }

        public void Fill()
        {
            Invoke(_fid_Fill);
        }

        public void Stroke()
        {
            Invoke(_fid_Stroke);
        }

        public void Clip()
        {
            Invoke(_fid_Clip);
        }

        public void Rotate(float angle)
        {
            Invoke(_fid_Rotate, angle);
        }

        public void Scale(float x, float y)
        {
            Invoke(_fid_Scale, x, y);
        }

        public void Translate(float x, float y)
        {
            Invoke(_fid_Translate, x, y);
        }

        public void Transform(float m11, float m12, float m21, float m22, float dx, float dy)
        {
            Invoke(_fid_Transform, m11, m12, m21, m22, dx, dy);
        }

        public void SetTransform(float m11, float m12, float m21, float m22, float dx, float dy)
        {
            Invoke(_fid_SetTransform, m11, m12, m21, m22, dx, dy);
        }

        public unsafe void GetTransform(ref float m11, ref float m12, ref float m21, ref float m22, ref float dx, ref float dy)
        {
            Matrix4x4 result = default;
            Invoke<IntPtr>(_fid_GetTransform, new IntPtr(&result));

            m11 = result.M11;
            m12 = result.M12;
            m21 = result.M21;
            m22 = result.M22;
            dx  = result.M31;
            dy  = result.M32;

            return;
        }

        public void Save()
        {
            Invoke(_fid_Save);
        }

        public void Restore()
        {
            Invoke(_fid_Restore);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
           
            Invoke(_fid_DisposeObject);
            base.Dispose(disposing);
        }


    }
}
