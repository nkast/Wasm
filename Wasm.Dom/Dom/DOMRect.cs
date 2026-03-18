namespace nkast.Wasm.Dom
{
    public struct DOMRect
    {
        public double X;
        public double Y;
        public double Width;
        public double Height;
        public double Top { get { return Y; } }
        public double Right { get { return X + Width; } }
        public double Bottom { get { return Y + Height; } }
        public double Left { get { return X; } }
    }
}
