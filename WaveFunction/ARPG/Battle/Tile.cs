using System.Numerics;

namespace WaveFunction.ARPG.Battle
{
    [Flags]
    public enum Direction
    {
        Left = 0x0001b,
        Right = 0x0010b,
        Up = 0x0100b,
        Down = 0x1000b,

        UpLeft = 0x0101b,
        UpRight = 0x0110b,
        DownLeft = 0x1001b,
        DownRight = 0x1010b
    }

    public class Tile
    {
        public static TileMaker Make => new TileMaker();

        public Tile(TileMaker m)
        {
            _passable = m.passable;
        }

        public bool MayPass => _passable;
        private readonly bool _passable;

        public void SetCol(double red, double green, double blue)
        {
            Col = new Vector3((float)red, (float)green, (float)blue);
        }

        public Vector3 Col { get; private set; }
    }

    public class TileMaker
    {
        private static readonly Direction[] dirs = Enum.GetValues<Direction>();
        public TileMaker()
        {
            passable = false;
        }

        public TileMaker CanPass(bool MayPass)
        {
            passable = MayPass;
            return this;
        }

        public Tile Result => new Tile(this);
        public bool passable;
    }
}
