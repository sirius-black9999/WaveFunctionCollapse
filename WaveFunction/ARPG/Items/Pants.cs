using WaveFunction.ARPG.Chars;

namespace WaveFunction.ARPG.Items
{
    public class Pants : IEquipment
    {
        public Pants(PantsMaker maker)
        {
        }

        public EquipSlots TargetSlot() => EquipSlots.Head;
    }

    public class PantsMaker
    {
        public Pants Done => new Pants(this);

        public PantsMaker WithStat(StatUtil.CharacterStats armorStat, double mod) => this;
    }
}
