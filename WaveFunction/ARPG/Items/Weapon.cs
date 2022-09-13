
namespace WaveFunction.ARPG.Characters.Items
{
    public class Weapon : Equipment
    {
        public Weapon(WeaponMaker maker)
        {
        }

        public EquipSlots targetSlot() => EquipSlots.Head;
    }

    public class WeaponMaker
    {
        public Weapon Done => new Weapon(this);

        public WeaponMaker WithStat(StatUtil.CharacterStats armorStat, double mod)
        {
            return this;
        }
    }
}
