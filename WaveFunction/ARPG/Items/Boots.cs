
namespace WaveFunction.ARPG.Characters.Items
{
    public class Boots : Equipment
    {
        public Boots(BootsMaker maker)
        {
        }
    
        public EquipSlots targetSlot() => EquipSlots.Head;
    }

    public class BootsMaker
    {
        public Boots Done => new Boots(this);

        public BootsMaker WithStat(StatUtil.CharacterStats armorStat, double mod)
        {
            return this;
        }
    }
}
