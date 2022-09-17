
using System.Numerics;
using WaveFunction.MagicSystemSketch;

namespace WaveFunction.Voronoi
{
    public class Locus
    {
        public Vector2 Position { get; set; }
        public Signature Effect { get; set; } = new Signature(0, 0, 0, 0, 0, 0, 0, 0);
    }
}
