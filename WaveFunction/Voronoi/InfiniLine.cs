using System.Numerics;

namespace WaveFunction.Voronoi
{
    public class LineSegment
    {
        public LineSegment(Vector2 lhs, Vector2 rhs)
        {
            P1 = lhs;
            P2 = rhs;
        }

        public Vector2 P1 { get; }
        public Vector2 P2 { get; }
    }

    public class InfiniLine
    {
        public InfiniLine(Vector2 p1, Vector2 p2)
        {
            _originPoint = (p1 + p2) / 2;
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            _direction = new Vector2(-dy, dx);
            _direction /= _direction.Length();
        }


        public Vector2 Intersect(InfiniLine other)
        {
            var p1 = _originPoint;
            var p2 = _originPoint + _direction;

            var p3 = other._originPoint;
            var p4 = other._originPoint + other._direction;

            var denominator = CalculateDenominator(p1, p2, p3, p4);
            if (Math.Abs(denominator) < 0.01f)
                return new Vector2(0, 0); //parallel, no intersection

            var px = ((p1.X * p2.Y - p1.Y * p2.X) * (p3.X - p4.X) - (p1.X - p2.X) * (p3.X * p4.Y - p3.Y * p4.X)) /
                     denominator;
            var py = ((p1.X * p2.Y - p1.Y * p2.X) * (p3.Y - p4.Y) - (p1.Y - p2.Y) * (p3.X * p4.Y - p3.Y * p4.X)) /
                     denominator;

            return new Vector2(px, py);
        }

        public bool Parallel(InfiniLine other)
        {
            var p1 = _originPoint;
            var p2 = _originPoint + _direction;

            var p3 = other._originPoint;
            var p4 = other._originPoint + other._direction;
            return CalculateDenominator(p1, p2, p3, p4) == 0;
        }

        float CalculateDenominator(Vector2 p11, Vector2 p12, Vector2 p21, Vector2 p22) =>
            (p11.X - p12.X) * (p21.Y - p22.Y) - (p11.Y - p12.Y) * (p21.X - p22.X);

        public float IntersectionDistance(Vector2 intersect)
        {
            var delta = intersect - _originPoint;
            return delta.X * _direction.X + delta.Y * _direction.Y;
        }

        public LineSegment TrimTo(params InfiniLine[] others)
        {
            var intersections = others.Select(Intersect).ToArray();
            var distances = intersections.Select(IntersectionDistance).ToArray();
            var maxNeg = -float.MaxValue;
            var negIndex = -1;
            var posIndex = -1;
            var minPos = float.MaxValue;

            for (var index = 0; index < distances.Length; index++)
            {
                var distance = distances[index];
                if (distance < 0 && distance > maxNeg)
                {
                    maxNeg = distance;
                    negIndex = index;
                }

                if (distance > 0 && distance < minPos)
                {
                    minPos = distance;
                    posIndex = index;
                }
            }

            return new LineSegment(intersections[negIndex], intersections[posIndex]);
        }

        private readonly Vector2 _originPoint;
        private readonly Vector2 _direction;
    }
}
