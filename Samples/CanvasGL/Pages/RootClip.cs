using System.Numerics;
using CanvasGL.Engine;

namespace CanvasGL.Pages
{
    internal class RootClip : Clip
    {

        public static Size vres = new Size(1920, 960);

        TriangleClip _tri;


        public RootClip() : base()
        {
            size = new Size(RootClip.vres.w, RootClip.vres.h);

            _tri = new TriangleClip();
            _tri.Position = new Vector2(0, 0);
            _tri.Scale = 2;
            Add(_tri);

        }

        public override void Update(UpdateContext uc)
        {
            float dt = (float)uc.dt.TotalSeconds;

            base.Update(uc);
        }
    }
}