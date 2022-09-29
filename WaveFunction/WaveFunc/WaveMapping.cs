using System.Numerics;
using WaveFunction.MagicSystemSketch;
using WaveFunction.Shared;

namespace WaveFunction.WaveFunc
{
    public class TileDef
    {
        private Vector2 position;
        private Signature Center;
        private Signature NorthEdge;
        private Signature SouthEdge;
        private Signature EastEdge;
        private Signature WestEdge;

        public static void MakeFrom(Vector2 pos, Vector3[,] tilePix)
        {
            TileDef ret = new TileDef();
            ret.position = pos;
        }
    }

    public class WaveMapElement
    {
        private Signature Voronoi;

        //the southern edge of the tile to the north
        public Func<Signature> NorthTileSouthEdge;
        public Func<Signature> SouthTileNorthEdge;
        public Func<Signature> EastTileWestEdge;
        public Func<Signature> WestTileEastEdge;
        private QuantumBag<TileDef> Definitions;
        private TileDef Chosen = null;

        public WaveMapElement(Signature voronoi, IRng rng, TileDef[] options)
        {
            Voronoi = voronoi;
            Definitions = new QuantumBag<TileDef>(rng);
            foreach (var tileDef in options)
            {
                Definitions.Add(tileDef);
            }
        }

        public double Entropy => Definitions.Entropy;
    }

    public class WaveMapping
    {
        private WaveMapElement[,] mapping;

        public WaveMapping(Voronoi.Voronoi voronoi, IRng rng, TileDef[] options)
        {
            mapping = new WaveMapElement[128, 128];
            for (int y = 0; y < 128; y++)
            {
                for (int x = 0; x < 128; x++)
                {
                    mapping[x, y] = new WaveMapElement(voronoi[x, y], rng, options);
                }
            }
        }
    }
}
