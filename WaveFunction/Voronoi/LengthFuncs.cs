using System.Numerics;

namespace WaveFunction.Voronoi
{
    public static class LengthFuncs
    {
        public static float EuclideanDistanceSqr(Vector2 p1, Vector2 p2)
        {
            var delta = p2 - p1;
            return delta.X * delta.X + delta.Y * delta.Y;
        }

        public static float ManhattanDistance(Vector2 p1, Vector2 p2)
        {
            var delta = p2 - p1;
            return Math.Abs(delta.X) + Math.Abs(delta.Y);
        }
    }
}
