using System.Numerics;
using Boids.Engine;

namespace Boids.Pages
{
    internal class RootClip : Clip
    {

        public static Size vres = new Size(1920, 960);

        BoidsClip _boids;


        public RootClip() : base()
        {
            size = new Size(RootClip.vres.w, RootClip.vres.h);

            _boids = new BoidsClip();
            _boids.Position = new Vector2(0, 0);
            _boids.Scale = 2;
            Add(_boids);

            FpsClip fps = new FpsClip();
            fps.Position = new Vector2(10,20);
            Add(fps);
        }

        public override void Update(UpdateContext uc)
        {
            float dt = (float)uc.dt.TotalSeconds;

            base.Update(uc);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _boids?.Dispose();
                _boids = null;
            }

            base.Dispose(disposing); 
        }
    }
}