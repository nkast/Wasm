namespace Boids.Engine
{
    public struct Size
    {
        public float w;
        public float h;

        public Size(float width, float height) : this()
        {
            this.w = width;
            this.h = height;
        }

        public float Ratio { get { return w / h; } }
    }
}
