
namespace VolcanoSim
{
    [Flags]
    enum Material
    {
        Air,
        Water,
        Pumice,
        Granite,
        Fracturing,
        Ash,
        Lava,
        Hotspot,
        DissolvedGas
    }
    public class GeoTile
    {
        private Material PrimaryFill;
        private Material SurfaceCap;
        private byte humidity;
        private byte Stability;
    }
}
