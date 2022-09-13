
using System.Numerics;

namespace SpellCasting
{
    public static class Colors
    {
        static float ByteToFloat(int byt, int pos)
        {
            var bits = pos * 8;
            return ((byt & 0xff << bits) >> bits) / 255f;
        }
        static Vector3 HexToColor(int col)
        {
            var r = ByteToFloat(col, 2);
            var g = ByteToFloat(col, 1);
            var b = ByteToFloat(col, 0);
            return new Vector3(r, g, b);
        }

        public static Vector3 SolidumPositive => HexToColor(0x006E33);
        public static Vector3 SolidumNeutral => new Vector3(0, 0, 0);
        public static Vector3 SolidumNegative => HexToColor(0x8de8e8);
        
        public static Vector3 FebrisPositive => HexToColor(0xea8615);
        public static Vector3 FebrisNeutral => new Vector3(0, 0, 0);
        public static Vector3 FebrisNegative => HexToColor(0x587EDE);
        
        public static Vector3 OrdinemPositive => HexToColor(0xafeeee);
        public static Vector3 OrdinemNeutral => new Vector3(0, 0, 0);
        public static Vector3 OrdinemNegative => HexToColor(0xce2029);
        
        public static Vector3 LuminesPositive => HexToColor(0xccff00);
        public static Vector3 LuminesNeutral => new Vector3(0, 0, 0);
        public static Vector3 LuminesNegative => HexToColor(0x8282ff);
        
        public static Vector3 VariasPositive => HexToColor(0xf945c0);
        public static Vector3 VariasNeutral => new Vector3(0, 0, 0);
        public static Vector3 VariasNegative =>HexToColor(0x59CBE8);
        
        public static Vector3 InertiaePositive =>HexToColor(0x8031A7);
        public static Vector3 InertiaeNeutral => new Vector3(0, 0, 0);
        public static Vector3 InertiaeNegative => HexToColor(0x44d7a8);
        
        public static Vector3 SubsidiumPositive => HexToColor(0x00B140);
        public static Vector3 SubsidiumNeutral => new Vector3(0, 0, 0);
        public static Vector3 SubsidiumNegative => HexToColor(0xE10600);
        
        public static Vector3 SpatiumPositive => HexToColor(0x00B2A9);
        public static Vector3 SpatiumNeutral => new Vector3(0, 0, 0);
        public static Vector3 SpatiumNegative => HexToColor(0xda9100);
    }
}
