using WaveFunction.ARPG.Chars;

namespace WaveFunction.ARPG.Items
{
    public class Ring : IEquipment
    {
        public Ring(RingMaker maker)
        {
        }

        public EquipSlots TargetSlot() => EquipSlots.Head;
    }

    public class RingMaker
    {
        public Ring Done => new Ring(this);

        public RingMaker WithStat(StatUtil.CharacterStats armorStat, double mod) => this;
    }
}
