namespace WaveFunction.ARPG.Items
{
    public static class ItemStore
    {
        public static HelmetMaker Helm => new HelmetMaker();
        public static ChestMaker Chest => new ChestMaker();
        public static PantsMaker Pants => new PantsMaker();
        public static BootsMaker Boots => new BootsMaker();
        public static GlovesMaker Gloves => new GlovesMaker();
        public static RingMaker Ring => new RingMaker();
        public static AmuletMaker Amulet => new AmuletMaker();
        public static WeaponMaker Weapon => new WeaponMaker();
        public static BeltMaker Belt => new BeltMaker();
        public static JewelMaker Jewel => new JewelMaker();
    }
}
