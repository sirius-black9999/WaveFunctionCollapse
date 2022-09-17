
namespace WaveFunction.ARPG.Characters.Items
{
    public class Jewel : IEquipment
    {
        public Jewel(JewelMaker maker)
        {
        }

        public EquipSlots TargetSlot() => EquipSlots.Head;
    }

    public class JewelMaker
    {
        public Jewel Done => new Jewel(this);

        public JewelMaker WithStat(StatUtil.CharacterStats armorStat, double mod)
        {
            return this;
        }
    }
}
