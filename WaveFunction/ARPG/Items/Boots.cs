using WaveFunction.ARPG.Chars;

namespace WaveFunction.ARPG.Items
{
    public class Boots : IEquipment
    {
        public Boots(BootsMaker maker)
        {
        }
    
        public EquipSlots TargetSlot() => EquipSlots.Head;
    }

    public class BootsMaker
    {
        public Boots Done => new Boots(this);

        public BootsMaker WithStat(StatUtil.CharacterStats armorStat, double mod) => this;
    }
}
