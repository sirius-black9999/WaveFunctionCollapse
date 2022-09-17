using WaveFunction.ARPG.Chars;

namespace WaveFunction.ARPG.Items
{
    public class Weapon : IEquipment
    {
        public Weapon(WeaponMaker maker)
        {
        }

        public EquipSlots TargetSlot() => EquipSlots.Head;
    }

    public class WeaponMaker
    {
        public Weapon Done => new Weapon(this);

        public WeaponMaker WithStat(StatUtil.CharacterStats armorStat, double mod) => this;
    }
}
