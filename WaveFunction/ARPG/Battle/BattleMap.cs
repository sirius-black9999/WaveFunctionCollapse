using System.Numerics;
using WaveFunction.Shared;

namespace WaveFunction.ARPG.Battle
{
    public class BattleMap
    {
        public BattleMap(BattleMapMaker m)
        {
            _tiles = m.Tiles;
            Size = m.Size;
        }

        private static Vector2 BaseSize => new Vector2(256, 256);
        public static BattleMapMaker Make => new BattleMapMaker(BaseSize);
        public Vector2 Size { get; }

        public Vector3 GetCol(Vector2 pos) => _tiles[Size.IndexOf(pos)].Col;
        public Vector3 GetCol(int x, int y) => _tiles[Size.IndexOf(x, y)].Col;
        private readonly Tile[] _tiles;
    }

    public class BattleMapMaker
    {
        public BattleMap Result => new BattleMap(this);

        public BattleMapMaker(Vector2 size)
        {
            Tiles = new Tile[(int)(size.X * size.Y)];
            Size = size;
            FillDefault();
        }

        private BattleMapMaker(Vector2 size, Vector2 oldSize, Tile[] oldTiles)
        {
            Tiles = new Tile[(int)(size.X * size.Y)];
            Size = size;
            FillDefault();
            oldSize.Foreach(Position =>
            {
                if (size.Contains(Position))
                {
                    Tiles[size.IndexOf(Position)] = oldTiles[oldSize.IndexOf(Position)];
                }
            });
        }

        private void FillDefault()
        {
            for (int i = 0; i < Tiles.Length; i++)
            {
                Tiles[i] = Tile.Make.Result;
            }
        }

        public BattleMapMaker Randomized(RNG rng)
        {
            Size.Foreach(pos =>
                Tiles[Size.IndexOf(pos)].SetCol(rng.next(), rng.next(), rng.next()));
            return this;
        }

        public Vector2 Size { get; }
        public Tile[] Tiles { get; }
        public BattleMapMaker DoubleWidth => new BattleMapMaker(Size with { X = Size.X * 2 }, Size, Tiles);
    }
}
