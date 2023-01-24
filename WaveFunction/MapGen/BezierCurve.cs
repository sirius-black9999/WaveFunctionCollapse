using System.Numerics;
using WaveFunction.MagicSystemSketch;
using WaveFunction.Shared;

namespace WaveFunction.MapGen
{
    public class BezierCurve
    {
        private readonly BezierCurveNode[] _nodes;

        public BezierCurve(BezierCurveNode[] curveNodes)
        {
            _nodes = curveNodes;
        }

        //it's good if these are immutable,
        //helps to separate creation time from usage time
        public BezierCurveNode this[int index] => _nodes[index];

        public Signature PickSignature(float x, float y)
        {
            float px = x % 1 * _nodes.Length;
            float pxFar = (x + 0.5f) % 1 * _nodes.Length;
            var a = PointOnCurve(px);
            var b = PointOnCurve(pxFar);
            return new Signature(
                a.Solidum * y + b.Solidum * (1 - y),
                a.Calor * y + b.Calor * (1 - y),
                a.Entropia * y + b.Entropia * (1 - y),
                a.Lumines * y + b.Lumines * (1 - y),
                a.Natura * y + b.Natura * (1 - y),
                a.Densitas * y + b.Densitas * (1 - y),
                a.Harmonius * y + b.Harmonius * (1 - y),
                a.Motus * y + b.Motus * (1 - y));
        }

        public Signature PickVolume(float x, float y, float z, IRng rand)
        {
            var rootSignature = PickSignature(x, y);
            var offset = new Vector8(
                (float)(rand.Next() * 2 - 1),
                (float)(rand.Next() * 2 - 1),
                (float)(rand.Next() * 2 - 1),
                (float)(rand.Next() * 2 - 1),
                (float)(rand.Next() * 2 - 1),
                (float)(rand.Next() * 2 - 1),
                (float)(rand.Next() * 2 - 1),
                (float)(rand.Next() * 2 - 1)).Normalized;

            return rootSignature + (offset * z);
        }

        public Signature PointOnCurve(double d)
        {
            var tFull = d * _nodes.Length;
            var values = new float[8];
            var eles = Enum.GetValues<Element>();
            int index = (int)Math.Floor(tFull);
            var t = tFull % 1;

            for (int i = 0; i < 8; i++)
            {
                var p0 = _nodes[(index + 0) % _nodes.Length].Point[eles[i]];
                var p1 = _nodes[(index + 1) % _nodes.Length].Point[eles[i]];
                var p2 = _nodes[(index + 2) % _nodes.Length].Point[eles[i]];
                var p3 = _nodes[(index + 3) % _nodes.Length].Point[eles[i]];
                var t0 = Math.Pow(t, 0) / 6;
                var t1 = Math.Pow(t, 1) / 6;
                var t2 = Math.Pow(t, 2) / 6;
                var t3 = Math.Pow(t, 3) / 6;
                values[i] =
                    (float)((-1 * t3 + 3 * t2 - 3 * t1 + 1 * t0) * p0 +
                            (03 * t3 - 6 * t2 + 0 * t1 + 4 * t0) * p1 +
                            (-3 * t3 + 3 * t2 + 3 * t1 + 1 * t0) * p2 +
                            (01 * t3 + 0 * t2 + 0 * t1 + 0 * t0) * p3);
            }

            return new Signature(values);
        }

        public float Min(Element e)
        {
            return CompareTo(e, float.MaxValue, (f, f1) => f < f1) - 0.1f;
        }

        public float Max(Element e)
        {
            return CompareTo(e, -float.MaxValue, (f, f1) => f > f1) + 0.1f;
        }

        private float CompareTo(Element e, float initial,
            Func<float, float, bool> comparison)
        {
            var max = initial;
            for (double i = 0; i < 1; i += 0.001)
            {
                var eleVal = PointOnCurve(i)[e];
                if (comparison(eleVal, max))
                    max = eleVal;
            }

            return max;
        }
    }
}
