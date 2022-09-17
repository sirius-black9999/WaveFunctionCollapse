using System.Numerics;

namespace WaveFunction.ARPG.Battle
{
    public class Tile
    {
        public static TileMaker Make => new TileMaker();

        public Tile(TileMaker m)
        {
            MayPass = m.Passable;
        }

        public bool MayPass { get; }

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
            Passable = false;
        }

        public TileMaker CanPass(bool mayPass)
        {
            Passable = mayPass;
            return this;
        }

        public Tile Result => new Tile(this);
        public bool Passable { get; private set; }
    }
}
