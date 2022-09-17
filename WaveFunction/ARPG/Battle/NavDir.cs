namespace WaveFunction.ARPG.Battle
{
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

    public static class NavDirExtensions
    {
        public static bool HasFlagFast(this NavDir value, NavDir flag) => (value & flag) != 0;
    }
}
