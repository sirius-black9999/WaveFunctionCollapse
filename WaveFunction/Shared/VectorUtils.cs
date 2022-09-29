using System.Numerics;

namespace WaveFunction.Shared
{
    public static class VectorUtils
    {
        public static bool Contains(this Vector2 size, Vector2 position) =>
            position.X > 0 &&
            position.X < size.X &&
            position.Y > 0 &&
            position.Y < size.Y;

        public static int IndexOf(this Vector2 size, Vector2 position) =>
            (int)(position.X + size.X * position.Y);

        public static int IndexOf(this Vector2 size, int x, int y) =>
            (int)(x + size.X * y);

        
        public static void Foreach(this Vector2 size, Action<Vector2> action)
        {
            for (int y = 0; y < size.Y; y++)
            {
                for (int x = 0; x < size.X; x++)
                {
                    Vector2 pos;
                    pos.X = x;
                    pos.Y = y;
                    action(pos);
                }
            }
        }

        public static void Foreach<T>(this Vector2 size, Action<Vector2, T> action, T param)
        {
            for (int x = 0; x < size.X; x++)
            {
                for (int y = 0; y < size.Y; y++)
                {
                    Vector2 pos;
                    pos.X = x;
                    pos.Y = y;
                    action(pos, param);
                }
            }
        }
    }
}
