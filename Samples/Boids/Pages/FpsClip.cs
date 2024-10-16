using System;
using System.Numerics;
using System.Diagnostics;
using nkast.Aether.Physics2D.Collision;
using nkast.Wasm.Canvas;
using Boids.Engine;
using aeVector2 = nkast.Aether.Physics2D.Common.Vector2;

namespace Boids.Pages
{
    public partial class FpsClip: Clip
    {
        Stopwatch _sw = new Stopwatch();

        public FpsClip() : base()
        {
            _sw.Start();
        }
        
        public override void Draw(DrawContext dc)
        {
            var cs = dc.CanvasContext;

            if (dc.Layer == 2)
            {
                int fps = (int)(1f / (float)_sw.Elapsed.TotalSeconds);
                _sw.Restart();

                // draw fps
                cs.Save();
                cs.FillStyle = "#603090";
                cs.Font = $"24px bold Verdana";
                cs.TextAlign = TextAlign.Left;
                cs.TextBaseline = TextBaseline.Middle;

                cs.FillText("FPS: " + fps, 0, 0);
                cs.Restore();                
            }

            base.Draw(dc);
        }
    }
}
