using System;
using System.Collections.Generic;
using System.Text;

namespace nkast.Wasm.Canvas
{
    public interface ICanvasRenderingContext : IRenderingContext
    {
        string FillStyle { get; set; }
        string StrokeStyle { get; set; }
        string Font { get; set; }
        TextAlign TextAlign { get; set; }
        TextBaseline TextBaseline { get; set; }
        float LineWidth { get; set; }
        LineCap LineCap { get; set; }
        float MiterLimit { get; set; }
        float GlobalAlpha { get; set; }
        CompositeOperation GlobalCompositeOperation { get; set; }
        bool ImageSmoothingEnabled { get; set; }
        float ShadowBlur { get; set; }
        string ShadowColor { get; set; }
        float ShadowOffsetX { get; set; }
        float ShadowOffsetY { get; set; }
        void ClearRect(float x, float y, float width, float height);
        void FillRect(float x, float y, float width, float height);
        void StrokeText(string text, float x, float y);
        void StrokeText(string text, float x, float y, float maxWidth);
        void DrawImage(string imgid, float dx, float dy);
        void DrawImage(string imgid, float dx, float dy, float dwidth, float dheight);
        void DrawImage(string imgid, float sx, float sy, float swidth, float sheight, float dx, float dy, float dwidth, float dheight);
        void FillText(string text, float x, float y);
        void FillText(string text, float x, float y, float maxWidth);
        float MeasureText(string text);
        float[] GetLineDash();
        void SetLineDash(float[] segments);
        void BeginPath();
        void ClosePath();
        bool IsPointInPath(float x, float y, bool evenodd = false);
        bool IsPointInStroke(float x, float y);
        void MoveTo(float x, float y);
        void LineTo(float x, float y);
        void BezierCurveTo(float cp1X, float cp1Y, float cp2X, float cp2Y, float x, float y);
        void QuadraticCurveTo(float cpx, float cpy, float x, float y);
        void Arc(float x, float y, float radius, float startAngle, float endAngle, bool anticlockwise = false);
        void ArcTo(float x1, float y1, float x2, float y2, float radius);
        void Rect(float x, float y, float width, float height);
        void Ellipse(float x, float y, float radiusX, float radiusY, float rotation = 0f, float startAngle = 0f, float endAngle = (float)(Math.PI * 2), bool anticlockwise = false);
        void Fill();
        void Stroke();
        void Clip();
        void Rotate(float angle);
        void Scale(float x, float y);
        void Translate(float x, float y);
        void Transform(float m11, float m12, float m21, float m22, float dx, float dy);
        void SetTransform(float m11, float m12, float m21, float m22, float dx, float dy);
        void GetTransform(ref float m11, ref float m12, ref float m21, ref float m22, ref float dx, ref float dy);
        void Save();
        void Restore();
    }
}
