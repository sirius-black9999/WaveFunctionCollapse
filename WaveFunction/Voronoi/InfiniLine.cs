using System.Numerics;

namespace WaveFunction.Voronoi
{
    public class LineSegment
    {
        public LineSegment(Vector2 lhs, Vector2 rhs)
        {
            p1 = lhs;
            p2 = rhs;
        }

        public Vector2 p1 { get; }
        public Vector2 p2 { get; }
    }

    public class InfiniLine
    {
        public InfiniLine(Vector2 p1, Vector2 p2)
        {
            OriginPoint = (p1 + p2) / 2;
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            Direction = new Vector2(-dy, dx);
            Direction /= Direction.Length();
        }

        private Vector2 OriginPoint;
        private Vector2 Direction;

        public Vector2 Intersect(InfiniLine Other)
        {
            var p1 = OriginPoint;
            var p2 = OriginPoint + Direction;

            var p3 = Other.OriginPoint;
            var p4 = Other.OriginPoint + Other.Direction;

            var denom = Denom(p1, p2, p3, p4);
            if (Math.Abs(denom) < 0.01f)
                return new Vector2(0, 0); //parallel, no intersection

            var px = ((p1.X * p2.Y - p1.Y * p2.X) * (p3.X - p4.X) - (p1.X - p2.X) * (p3.X * p4.Y - p3.Y * p4.X)) /
                     denom;
            var py = ((p1.X * p2.Y - p1.Y * p2.X) * (p3.Y - p4.Y) - (p1.Y - p2.Y) * (p3.X * p4.Y - p3.Y * p4.X)) /
                     denom;
            
            return new Vector2(px, py);
        }

        public bool Parallel(InfiniLine Other)
        {
            var p1 = OriginPoint;
            var p2 = OriginPoint + Direction;

            var p3 = Other.OriginPoint;
            var p4 = Other.OriginPoint + Other.Direction;
            return Denom(p1, p2, p3, p4) == 0;
        }

        float Denom(Vector2 p11, Vector2 p12, Vector2 p21, Vector2 p22)
        {
            return (p11.X - p12.X) * (p21.Y - p22.Y) - (p11.Y - p12.Y) * (p21.X - p22.X);
        }

        public float IntersectionDistance(Vector2 intersect)
        {
            var Delta = intersect - OriginPoint;
            return Delta.X * Direction.X + Delta.Y * Direction.Y;
        }
        public LineSegment TrimTo(params InfiniLine[] others)
        {
            Vector2[] intersections = others.Select(Intersect).ToArray();
            float[] distances = intersections.Select(IntersectionDistance).ToArray();
            float maxNeg = -float.MaxValue;
            int negIndex = -1;
            int posIndex = -1;
            float minPos = float.MaxValue;
            
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
    }
}
