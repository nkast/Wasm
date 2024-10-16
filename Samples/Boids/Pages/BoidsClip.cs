using System;
using System.Numerics;
using nkast.Aether.Physics2D.Collision;
using nkast.Wasm.Canvas;
using Boids.Engine;
using aeVector2 = nkast.Aether.Physics2D.Common.Vector2;

namespace Boids.Pages
{
    public class BoidsClip : Clip
    {
        const float maxDistanceFlock = 75;
        const float maxDistanceAlign = 75;
        const float maxDistanceAvoid = 30;
        const float maxDistancePredator = 100;

        const float flockForce = .0004f;
        const float AlignForce = .004f;
        const float AvoidForce = .003f;
        const float PredatorForce = .001f;

        float minSpeed = 1.5f;
        float maxSpeed = 5;

#if DEBUG
        const int BoidCount = 64;
#else
        const int BoidCount = 128;
#endif
        // Boids
        public readonly Vector2[] _pos = new Vector2[BoidCount];
        public readonly Vector2[] _vel = new Vector2[BoidCount];
        public readonly Vector2[] _acc = new Vector2[BoidCount];
        public readonly int[] _pidfl = new int[BoidCount];
        public readonly int[] _pidal = new int[BoidCount];
        public readonly int[] _pidav = new int[BoidCount];

        IBroadPhase<int> bpfl = new DynamicTreeBroadPhase<int>();
        IBroadPhase<int> bpal = new DynamicTreeBroadPhase<int>();
        IBroadPhase<int> bpav = new DynamicTreeBroadPhase<int>();
        private readonly Random Rand = new Random();


        public BoidsClip() : base()
        {
            base.size = new Size(800, 480);

            for (int i = 0; i < BoidCount; i++)
            {
                _pos[i] = new Vector2(
                    Rand.NextSingle() * size.w,
                    Rand.NextSingle() * size.h);
                _vel[i] = new Vector2(
                    Rand.NextSingle() - 0.5f,
                    Rand.NextSingle() - 0.5f);

                aeVector2 boidPosition = new aeVector2(_pos[i].X, _pos[i].Y);

                AABB aabb = new AABB(boidPosition, maxDistanceFlock * 2, maxDistanceFlock * 2);
                _pidfl[i] = bpfl.AddProxy(ref aabb);
                bpfl.SetProxy(_pidfl[i], ref i);

                aabb = new AABB(boidPosition, maxDistanceAlign * 2, maxDistanceAlign * 2);
                _pidal[i] = bpal.AddProxy(ref aabb);
                bpal.SetProxy(_pidal[i], ref i);

                aabb = new AABB(boidPosition, maxDistanceAvoid * 2, maxDistanceAvoid * 2);
                _pidav[i] = bpav.AddProxy(ref aabb);
                bpav.SetProxy(_pidav[i], ref i);
            }

        }


        public override void Update(UpdateContext uc)
        {
            float dt = (float)uc.dt.TotalSeconds;

            Advance(uc);
            
            float minSpeedsq = (float)Math.Pow(minSpeed, 2);
            float maxSpeedsq = (float)Math.Pow(maxSpeed, 2);

            for (int i = 0; i < BoidCount; i+=1)
            {
                // apply force
                _vel[i] += _acc[i];
                // apply velocity
                _pos[i] += _vel[i] * 30f * dt;
            }

            for (int i = 0; i < BoidCount; i++)
            {
                aeVector2 boidPosition = new aeVector2(_pos[i].X, _pos[i].Y);
                aeVector2 boidVelocity = new aeVector2(_vel[i].X, _vel[i].Y);

                //update aabb
                AABB aabb = new AABB(boidPosition, maxDistanceFlock * 2, maxDistanceFlock * 2);
                bpfl.MoveProxy(_pidfl[i], ref aabb, boidVelocity);

                aabb = new AABB(boidPosition, maxDistanceAlign * 2, maxDistanceAlign * 2);
                bpal.MoveProxy(_pidal[i], ref aabb, boidVelocity);

                aabb = new AABB(boidPosition, maxDistanceAvoid * 2, maxDistanceAvoid * 2);
                bpav.MoveProxy(_pidav[i], ref aabb, boidVelocity);


                SpeedLimit(i, minSpeed, maxSpeed, minSpeedsq, maxSpeedsq);
            }

            bool bounceOffWalls = true;
            bool wrapAroundEdges = false;
            if (bounceOffWalls)
            {
                for (int i = 0; i < BoidCount; i++)

                    BounceOffWalls(i);
            }
            else if (wrapAroundEdges)
            {
                for (int i = 0; i < BoidCount; i++)
                    WrapAround(i);
            }

            base.Update(uc);
        }
       
        public void Advance(UpdateContext uc)
        {
            Vector2 mousePos = uc.CurrMouseState.Position;
            if (uc.CurrTouchState.IsPressed)
                mousePos = uc.CurrTouchState.Position;
            Vector2 localPos = uc.ToLocal(mousePos);

            // reset forces
            for (int i = 0; i < BoidCount; i++)
                _acc[i] = Vector2.Zero;

            Flock();
            Align();
            Avoid();
            Predator(localPos);

        }

        private void Flock()
        {
            float frc = flockForce;
            float maxDistancesq = (float)Math.Pow(maxDistanceFlock, 2);

            bpfl.UpdatePairs((pid, otherpid) =>
            {
                int i = bpfl.GetProxy(pid);
                int otheri = bpfl.GetProxy(otherpid);
                Vector2 d = _pos[otheri] - _pos[i];
                float distancesq = d.X * d.X + d.Y * d.Y;
                if (distancesq < maxDistancesq)
                {
                    _acc[i] += d * frc;
                    _acc[otheri] += -d * frc;
                }
            });
        }

        private void Align()
        {
            float frc = AlignForce;
            float maxDistancesq = (float)Math.Pow(maxDistanceAlign, 2);

            bpal.UpdatePairs((pid, otherpid) =>
            {
                int i = bpfl.GetProxy(pid);
                int otheri = bpal.GetProxy(otherpid);
                Vector2 dp = _pos[otheri] - _pos[i];
                float distancesq = dp.X * dp.X + dp.Y * dp.Y;
                if (distancesq < maxDistancesq)
                {
                    Vector2 dv = _vel[otheri] - _vel[i];
                    _acc[i] += dv * frc;
                    _acc[otheri] += -dv * frc;
                }
            });
        }

        private void Avoid()
        {
            float frc = AvoidForce;
            float maxDistance = maxDistanceAvoid;
            float maxDistancesq = (float)Math.Pow(maxDistanceAvoid, 2);

            bpav.UpdatePairs((pid, otherpid) =>
            {
                int i = bpfl.GetProxy(pid);
                int otheri = bpav.GetProxy(otherpid);
                Vector2 d = _pos[otheri] - _pos[i];
                float distancesq = d.X * d.X + d.Y * d.Y;
                if (distancesq < maxDistancesq)
                {
                    float distance = (float)Math.Sqrt(distancesq);
                    float closeness = maxDistance - distance;
                    _acc[i] += -d * closeness * frc;
                    _acc[otheri] += d * closeness * frc;
                }
            });
        }

        private void Predator(Vector2 predPos)
        {
            float frc = PredatorForce;
            float maxDistance = maxDistancePredator;
            float maxDistancesq = (float)Math.Pow(maxDistancePredator, 2);

            for (int i = 0; i < BoidCount; i++)
            {
                Vector2 d = predPos - _pos[i];
                float distancesq = d.X * d.X + d.Y * d.Y;
                if (distancesq < maxDistancesq)
                {
                    float distance = (float)Math.Sqrt(distancesq);
                    float closeness = maxDistance - distance;
                    _acc[i] += -d * closeness * frc;
                }
            }
        }

        private void BounceOffWalls(int i)
        {
            float pad = 50f;
            float turn = .5f;

            if (_pos[i].X < pad)
                _vel[i].X += turn;
            if (_pos[i].X > size.w - pad)
                _vel[i].X -= turn;
            if (_pos[i].Y < pad)
                _vel[i].Y += turn;
            if (_pos[i].Y > size.h - pad)
                _vel[i].Y -= turn;
        }

        private void WrapAround(int i)
        {
            if (_pos[i].X < 0)
                _pos[i].X += size.w;
            if (_pos[i].X > size.w)
                _pos[i].X -= size.w;
            if (_pos[i].Y < 0)
                _pos[i].Y += size.h;
            if (_pos[i].Y > size.h)
                _pos[i].Y -= size.h;
        }

        public void SpeedLimit(int i, float minSpeed, float maxSpeed, float minSpeedsq, float maxSpeedsq)
        {
            float speedsq = _vel[i].LengthSquared();

            if (speedsq > 0 && speedsq < minSpeedsq)
            {
                _vel[i] *= (minSpeed / (float)Math.Sqrt(speedsq));
            }
            else if (speedsq > 0 && speedsq > maxSpeedsq)
            {
                _vel[i] *= (maxSpeed / (float)Math.Sqrt(speedsq));
            }
        }


        public override void Draw(DrawContext dc)
        {
            var cs = dc.CanvasContext;

            if (dc.Layer == 0)
            {

                // draw image
                cs.Save();
                cs.Scale(2, 2);
                cs.GlobalAlpha = 0.2f;
                cs.DrawImage("image1", 100, 100);
                cs.Restore();
                
                cs.FillStyle = "#000000";

                // render each boid
                for (int i = 0; i < BoidCount; i++)
                    DrawBoid(i, cs);

            }

            base.Draw(dc);
        }

        private void DrawBoid(int i, ICanvasRenderingContext cs)
        {
            Vector2[] path =
            {
                new Vector2(0,0),
                new Vector2(0,3),
                new Vector2(10,0),
                new Vector2(0,-3),
                new Vector2(0, 0)
            };

            float lensq = _vel[i].LengthSquared();
            if (lensq > 0)
            {
                Vector2 orientation = _vel[i] / (float)Math.Sqrt(lensq);
                for (int p = 1; p < path.Length-1; p++)
                {
                    path[p] = Rotate(path[p], orientation.X, orientation.Y);
                }
            }

            cs.Save();
            cs.Translate(_pos[i]);
            cs.Scale(2, 2);
            cs.BeginPath();
            cs.MoveTo(path[0]);
            cs.LineTo(path[1]);
            cs.LineTo(path[2]);
            cs.LineTo(path[3]);
            cs.LineTo(path[4]);
            cs.ClosePath();
            cs.Fill();
            cs.Restore();
        }

        public static Vector2 Rotate(Vector2 pos, float cos, float sin)
        {
            Vector2 results;
            results.X = (pos.X * cos) - (pos.Y * sin);
            results.Y = (pos.X * sin) + (pos.Y * cos);
            return results;
        }
    }
}
