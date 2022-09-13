
using System.Numerics;

namespace WaveFunction.Shared
{
    public static class VectorUtils
    {
        public static bool Contains(this Vector2 Size, Vector2 position) =>
            position.X > 0 &&
            position.X < Size.X &&
            position.Y > 0 &&
            position.Y < Size.Y;

        public static int IndexOf(this Vector2 Size, Vector2 position) => 
            (int)(position.X + Size.X * position.Y);
        public static int IndexOf(this Vector2 Size, int x, int y) => 
            (int)(x + Size.X * y);

        public static void Foreach(this Vector2 Size, Action<Vector2> Action)
        {
            Vector2 pos;
            for (int y = 0; y < Size.Y; y++)
            {
            for (int x = 0; x < Size.X; x++)
            {
                    pos.X = x;
                    pos.Y = y;
                    Action(pos);
                }
            }
        }
        public static void Foreach<T>(this Vector2 Size, Action<Vector2, T> Action, T Param)
        {
            Vector2 pos;
            for (int x = 0; x < Size.X; x++)
            {
                for (int y = 0; y < Size.Y; y++)
                {
                    pos.X = x;
                    pos.Y = y;
                    Action(pos, Param);
                }
            }
        }
    }
}
