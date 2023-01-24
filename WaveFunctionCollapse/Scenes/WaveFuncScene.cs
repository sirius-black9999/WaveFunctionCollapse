using System.Diagnostics;
using System.Numerics;
using Cairo;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using WaveFunction.MagicSystemSketch;
using WaveFunction.Shared;
using WaveFunction.Voronoi;
using WaveFunction.WaveFunc;

namespace WaveFunctionCollapse.Scenes
{
    public class WaveFuncScene : IScene<Context>
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
        private readonly TileMap _tiles;
        private WaveMapping _map;

        private readonly Vector2 _gridSize = new Vector2(128, 128);

        public WaveFuncScene()
        {
            _tiles = new TileMap(Rand);
            using var image = Image.Load<Argb32>("../../../../Assets/FullMap.png");

            var tilePix = new Vector3[8, 8];
            for (int y = 0; y < 512; y++)
            {
                for (int x = 0; x < 512; x++)
                {
                    for (int px = 0; px < 8; px++)
                    {
                        for (int py = 0; py < 8; py++)
                        {
                            var pix = image[x + px, y + py];
                            tilePix[px, py] = new Vector3(pix.R, pix.G, pix.B);
                        }
                    }

                    TileDef.MakeFrom(new Vector2(x, y), tilePix);
                }
            }

            _map = new WaveMapping(_v, Rand, _tiles.Pick(1000));
        }

        public void Render(Context cr, Vector2 size)
        {
            if (cr == null) throw new ArgumentNullException(nameof(cr));

            _gridSize.Foreach(RenderTile, cr);
        }

        private void RenderTile(Vector2 pos, Context cr)
        {
            var col = _v[pos].SignatureColor;
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
        readonly QuantumBag<TileDef> _tiles;

        public TileMap(IRng rand)
        {
            _tiles = new QuantumBag<TileDef>(rand);
        }

        public void Add(TileDef tile)
        {
            _tiles.Add(tile);
        }

        public TileDef[] Pick(int count)
        {
            TileDef[] ret = new TileDef[count];
            for (int i = 0; i < count; i++)
            {
                _tiles.Get();
            }

            return ret;
        }
    }
}
