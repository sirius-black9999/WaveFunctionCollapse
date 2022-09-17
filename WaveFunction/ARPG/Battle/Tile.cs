using System.Numerics;

namespace WaveFunction.ARPG.Battle
{
    [Flags]
    public enum NavDir
    {
        Central = 0x0000b,
        
        North = 0x0001b,
        South = 0x0010b,
        East = 0x0100b,
        West = 0x1000b,

        NorthEast = 0x0101b,
        NorthWest = 0x1001b,
        SouthEast = 0x0110b,
        SouthWest = 0x1010b
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
