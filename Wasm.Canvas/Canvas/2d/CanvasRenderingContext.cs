using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace nkast.Wasm.Canvas
{
    internal class CanvasRenderingContext : RenderingContext, ICanvasRenderingContext
    {
        internal CanvasRenderingContext(Canvas canvas, int uid) : base(canvas, uid)
        {
        }

        public string FillStyle
        {
            get { return InvokeRet<string>("nkCanvas2dContext.GetFillStyle"); }
            set { Invoke("nkCanvas2dContext.SetFillStyle", value); }
        }

        public string StrokeStyle
        {
            get { return InvokeRet<string>("nkCanvas2dContext.GetStrokeStyle"); }
            set { Invoke("nkCanvas2dContext.SetStrokeStyle", value); }
        }

        public string Font
        {
            get { return InvokeRet<string>("nkCanvas2dContext.GetFont"); }
            set { Invoke("nkCanvas2dContext.SetFont", value); }
        }

        public TextAlign TextAlign
        {
            get
            {
                string str = InvokeRet<string>("nkCanvas2dContext.GetTextAlign");
                return Enum.Parse<TextAlign>(str, true);
            }
            set { Invoke("nkCanvas2dContext.SetTextAlign", value.ToString().ToLower()); }
        }

        public TextBaseline TextBaseline
        {
            get
            {
                string str = InvokeRet<string>("nkCanvas2dContext.GetTextBaseline");
                return Enum.Parse<TextBaseline>(str, true);
            }
            set { Invoke("nkCanvas2dContext.SetTextBaseline", value.ToString().ToLower()); }
        }

        public float LineWidth
        {
            get { return InvokeRetFloat("nkCanvas2dContext.GetLineWidth"); }
            set { Invoke("nkCanvas2dContext.SetLineWidth", value); }
        }

        public LineCap LineCap
        {
            get
            {
                string str = InvokeRet<string>("nkCanvas2dContext.GetLineCap");
                return Enum.Parse<LineCap>(str, true);
            }
            set { Invoke("nkCanvas2dContext.SetLineCap", value.ToString().ToLower()); }
        }

        public float MiterLimit
        {
            get { return InvokeRetFloat("nkCanvas2dContext.GetMiterLimit"); }
            set { Invoke("nkCanvas2dContext.SetMiterLimit", value); }
        }

        public float GlobalAlpha
        {
            get { return InvokeRetFloat("nkCanvas2dContext.GetGlobalAlpha"); }
            set { Invoke("nkCanvas2dContext.SetGlobalAlpha", value); }
        }

        public CompositeOperation GlobalCompositeOperation
        {
            get
            {
                string str = InvokeRet<string>("nkCanvas2dContext.GetGlobalCompositeOperation")?.Replace('-', '_');
                return Enum.Parse<CompositeOperation>(str, true);
            }
            set { Invoke("nkCanvas2dContext.SetGlobalCompositeOperation", value.ToString().ToLower().Replace('_', '-')); }
        }

        public bool ImageSmoothingEnabled
        {
            get { return InvokeRetBool("nkCanvas2dContext.GetImageSmoothingEnabled"); }
            set { Invoke("nkCanvas2dContext.SetImageSmoothingEnabled", value ? 1 : 0); }
        }

        public float ShadowBlur
        {
            get { return InvokeRetFloat("nkCanvas2dContext.GetShadowBlur"); }
            set { Invoke("nkCanvas2dContext.SetShadowBlur", value); }
        }

        public string ShadowColor
        {
            get { return InvokeRet<string>("nkCanvas2dContext.GetShadowColor"); }
            set { Invoke("nkCanvas2dContext.SetShadowColor", value); }
        }

        public float ShadowOffsetX
        {
            get { return InvokeRetFloat("nkCanvas2dContext.GetShadowOffsetX"); }
            set { Invoke("nkCanvas2dContext.SetShadowOffsetX", value); }
        }

        public float ShadowOffsetY
        {
            get { return InvokeRetFloat("nkCanvas2dContext.GetShadowOffsetY");}
            set { Invoke("nkCanvas2dContext.SetShadowOffsetY", value); }
        }

        public void ClearRect(float x, float y, float width, float height)
        {
            Invoke("nkCanvas2dContext.ClearRect", x, y, width, height);
        }

        public void FillRect(float x, float y, float width, float height)
        {
            Invoke("nkCanvas2dContext.FillRect", x, y, width, height);
        }

        public void StrokeRect(float x, float y, float width, float height)
        {
            Invoke("nkCanvas2dContext.StrokeRect", x, y, width, height);
        }

        public void DrawImage(string imgid, float dx, float dy)
        {
            Invoke("nkCanvas2dContext.DrawImage", imgid, dx, dy);
        }

        public void DrawImage(string imgid, float dx, float dy, float dwidth, float dheight)
        {
            Invoke("nkCanvas2dContext.DrawImage1", imgid, dx, dy, dwidth, dheight);
        }

        public void DrawImage(string imgid, float sx, float sy, float swidth, float sheight, float dx, float dy, float dwidth, float dheight)
        {
            Invoke("nkCanvas2dContext.DrawImage2", imgid, sx, sy, swidth, sheight, dx, dy, dwidth, dheight);
        }

        public void FillText(string text, float x, float y)
        {
            Invoke("nkCanvas2dContext.FillText", text, x, y);
        }

        public void FillText(string text, float x, float y, float maxWidth)
        {
            Invoke("nkCanvas2dContext.FillText1", text, x, y, maxWidth);
        }

        public void StrokeText(string text, float x, float y)
        {            
                Invoke("nkCanvas2dContext.StrokeText", text, x, y);
        }

        public void StrokeText(string text, float x, float y, float maxWidth)
        {
            Invoke("nkCanvas2dContext.StrokeText1", text, x, y, maxWidth);
        }

        public float MeasureText(string text)
        {
            return InvokeRetFloat<string>("nkCanvas2dContext.MeasureText", text);
        }

        private static float[] _emptyLineDash = new float[0];
        public float[] GetLineDash()
        {
            string str = InvokeRet<string>("nkCanvas2dContext.GetLineDash");
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

        public void SetLineDash(float[] segments)
        {
            Invoke("nkCanvas2dContext.SetLineDash", segments);
        }

        public void BeginPath()
        {
            Invoke("nkCanvas2dContext.BeginPath");
        }

        public void ClosePath()
        {
            Invoke("nkCanvas2dContext.ClosePath");
        }

        public bool IsPointInPath(float x, float y, bool evenodd = false)
        {
            return InvokeRetBool<float, float, int>("nkCanvas2dContext.IsPointInPath", x, y, evenodd ? 1 : 0);
        }

        public bool IsPointInStroke(float x, float y)
        {
            return InvokeRetBool<float, float>("nkCanvas2dContext.IsPointInStroke", x, y);
        }

        public void MoveTo(float x, float y)
        {
            Invoke("nkCanvas2dContext.MoveTo", x, y);
        }

        public void LineTo(float x, float y)
        {
            Invoke("nkCanvas2dContext.LineTo", x, y);
        }

        public void BezierCurveTo(float cp1X, float cp1Y, float cp2X, float cp2Y, float x, float y)
        {
            Invoke("nkCanvas2dContext.BezierCurveTo", cp1X, cp1Y, cp2X, cp2Y, x, y);
        }

        public void QuadraticCurveTo(float cpx, float cpy, float x, float y)
        {
            Invoke("nkCanvas2dContext.QuadraticCurveTo", cpx, cpy, x, y);
        }

        public void Arc(float x, float y, float radius, float startAngle, float endAngle, bool anticlockwise = false)
        {
            Invoke("nkCanvas2dContext.Arc", x, y, radius, startAngle, endAngle, anticlockwise ? 1 : 0);
        }

        public void ArcTo(float x1, float y1, float x2, float y2, float radius)
        {
            Invoke("nkCanvas2dContext.ArcTo", x1, y1, x2, y2, radius);
        }

        public void Rect(float x, float y, float width, float height)
        {
            Invoke("nkCanvas2dContext.Rect", x, y, width, height);
        }

        public void Ellipse(float x, float y, float radiusX, float radiusY, float rotation = 0f, float startAngle = 0f, float endAngle = (float)(Math.PI * 2), bool anticlockwise = false)
        {
            Invoke("nkCanvas2dContext.Ellipse", x, y, radiusX, radiusY, rotation, startAngle, endAngle, anticlockwise ? 1 : 0);
        }

        public void Fill()
        {
            Invoke("nkCanvas2dContext.Fill");
        }

        public void Stroke()
        {
            Invoke("nkCanvas2dContext.Stroke");
        }

        public void Clip()
        {
            Invoke("nkCanvas2dContext.Clip");
        }

        public void Rotate(float angle)
        {
            Invoke("nkCanvas2dContext.Rotate", angle);
        }

        public void Scale(float x, float y)
        {
            Invoke("nkCanvas2dContext.Scale", x, y);
        }

        public void Translate(float x, float y)
        {
            Invoke("nkCanvas2dContext.Translate", x, y);
        }

        public void Transform(float m11, float m12, float m21, float m22, float dx, float dy)
        {
            Invoke("nkCanvas2dContext.Transform", m11, m12, m21, m22, dx, dy);
        }

        public void SetTransform(float m11, float m12, float m21, float m22, float dx, float dy)
        {
            Invoke("nkCanvas2dContext.SetTransform", m11, m12, m21, m22, dx, dy);
        }

        public unsafe void GetTransform(ref float m11, ref float m12, ref float m21, ref float m22, ref float dx, ref float dy)
        {
            Matrix4x4 result = default;
            Invoke<IntPtr>("nkCanvas2dContext.GetTransform", new IntPtr(&result));

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
            Invoke("nkCanvas2dContext.Save");
        }

        public void Restore()
        {
            Invoke("nkCanvas2dContext.Restore");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
           
            Invoke("nkJSObject.DisposeObject");
            base.Dispose(disposing);
        }


    }
}
