using System.Collections.Generic;
using System.Numerics;


namespace WebXR.Engine
{
    public class Clip : IDisposable
    {
        private Vector2 _position;
        private float _rotation;
        private Size _size;
        private float _scale = 1f;

        public Clip Parent { get; private set; }
        private readonly List<Clip> _children = new List<Clip>();

        protected IEnumerable<Clip> Children { get { return _children;  } }

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Size size
        {
            get { return _size; }
            set { _size = value; }
        }
        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        public Clip()
        {

        }

        internal void Add(Clip clip)
        {
            _children.Add(clip);
            clip.Parent = this;
        }



        public virtual void Update(UpdateContext uc)
        {
            Matrix4x4 tx;
            foreach (Clip clip in _children)
            {
                tx = uc.tx; // save
                uc.tx = Matrix4x4.CreateTranslation(clip._position.X, clip._position.Y, 0f)* uc.tx;
                uc.tx = Matrix4x4.CreateScale(clip.Scale, clip.Scale, 1) * uc.tx;
                clip.Update(uc);
                uc.tx = tx; // restore
            }
        }

        public virtual void Draw(DrawContext dc)
        {
            foreach (Clip clip in _children)
            {
                clip.Draw(dc);
            }
        }

        ~Clip()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

            }

        }
    }
}
