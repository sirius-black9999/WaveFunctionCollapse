using System.Numerics;
using Cairo;
using WaveFunction.MagicSystemSketch;
using WaveFunction.Shared;
using WaveFunction.Voronoi;
using WaveFunction.WaveFunc;

namespace WaveFunctionCollapse.Scenes
{
    public class WaveFuncScene
    {
        private static readonly IRng Rand = new BaseRng();

        private static Locus[] Points => Generator<Locus>.Make(15, Rand, static _ =>
        {
            return new Locus()
            {
                Position = new Vector2((float)(Rand.Next() * 256), (float)(Rand.Next() * 256)),
                Effect = new Signature(Generator<float>.Make(8, Rand, static rand => (float)rand.Next() * 2 - 1))
            };
        });

        private readonly Voronoi _v = new Voronoi(LengthFuncs.ManhattanDistance, Points);
        private TileMap tiles;
        WaveMapping map;

        private readonly Vector2 _gridSize = new Vector2(128, 128);

        public WaveFuncScene()
        {
            tiles = new TileMap(Rand);
            map = new WaveMapping(_v, Rand, tiles.Pick(1000));
        }

        public void Render(Context cr, Vector2 size)
        {
            if (cr == null) throw new ArgumentNullException(nameof(cr));

            _gridSize.Foreach(RenderTile, cr);
        }

        private void RenderTile(Vector2 pos, Context cr)
        {
            var col = _v[pos].Color;
            cr.SetSourceRGB(col.X, col.Y, col.Z);
            cr.Rectangle(pos.X * 2, pos.Y * 2, 2, 2);
            cr.Fill();
        }

        public void Update(int frameCount, Vector2 pos)
        {
            //Unused
        }

        public void MouseClick(Vector2 pos)
        {
            //Unused
        }

        public void MouseRelease(Vector2 pos)
        {
            //Unused
        }

        public void MouseDrag(Vector2 pos)
        {
            //Unused
        }

        public void MouseScroll(bool up)
        {
            //Unused
        }
    }

    internal class TileMap
    {
        QuantumBag<TileDef> tiles;

        public TileMap(IRng rand)
        {
            tiles = new QuantumBag<TileDef>(rand);
        }

        public void Add(TileDef tile)
        {
            tiles.Add(tile);
        }

        public TileDef[] Pick(int count)
        {
            TileDef[] ret = new TileDef[count];
            for (int i = 0; i < count; i++)
            {
                tiles.Get();
            }

            return ret;
        }
    }
}
