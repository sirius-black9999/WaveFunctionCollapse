using System.Numerics;
using WaveFunction.MagicSystemSketch;

namespace WaveFunction.Voronoi
{
    public class Voronoi
    {
        public Voronoi(Func<Vector2, Vector2, float> length, params Locus[] points)
        {
            _pts = points;
            _length = length;
        }

        public Signature this[int x, int y] => this[new Vector2(x, y)];

        public Signature this[Vector2 tested]
        {
            get
            {
                var locus = _pts[0];
                var dist = _length(locus.position, tested);
                for (int i = 1; i < _pts.Length; i++)
                {
                    var newDist = _length(_pts[i].position, tested);
                    if (newDist < dist)
                    {
                        locus = _pts[i];
                        dist = newDist;
                    }
                }

                return locus.effect;
            }
        }

        public int Points => _pts.Length;
        private readonly Func<Vector2, Vector2, float> _length;
        private Locus[] _pts;
    }
}
