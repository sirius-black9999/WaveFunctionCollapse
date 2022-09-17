using System.Numerics;
using Cairo;
using WaveFunction;
using WaveFunction.MagicSystemSketch;
using WaveFunction.Shared;
using WaveFunction.Voronoi;
using WaveFunction.WaveFunc;
using WaveFunctionTest.PropertyTesting.Tooling;

namespace WaveFunctionCollapse.Scenes
{
    public class VoronoiScene : IScene<Context>
    {
        private static RNG rand = new BaseRng();

        private static Locus[] points => Generator<Locus>.Make(15, rand, rng =>
        {
            return new Locus()
            {
                position = new Vector2((float)(rand.next() * 256), (float)(rand.next() * 256)),
                effect = new Signature(Generator<float>.Make(8, rand, rand => (float)rand.next()*2-1))
            };
        });

        private Voronoi v = new Voronoi(LengthFuncs.ManhattanDistance, points);

        private Vector2 gridSize = new Vector2(256, 256);

        public void Render(Context cr, Vector2 size)
        {
            gridSize.Foreach(RenderTile, cr);
        }

        private void RenderTile(Vector2 pos, Context cr)
        {
            var col = v[pos].Color;
            cr.SetSourceRGB(col.X, col.Y, col.Z);
            cr.Rectangle(pos.X * 2, pos.Y * 2, 2, 2);
            cr.Fill();
        }

        public void Update(int frameCount, Vector2 mousePos)
        {
        }

        public void MouseClick(Vector2 pos)
        {
        }

        public void MouseRelease(Vector2 pos)
        {
        }

        public void MouseDrag(Vector2 pos)
        {
        }

        public void MouseScroll(bool up)
        {
        }
    }
}
