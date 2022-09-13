
namespace WaveFunction.ARPG.Characters.Items
{
    public class Amulet : Equipment
    {
        public Amulet(AmuletMaker maker)
        {
        }

        public EquipSlots targetSlot() => EquipSlots.Head;
    }

    public class AmuletMaker
    {
        public Amulet Done => new Amulet(this);

        public AmuletMaker WithStat(StatUtil.CharacterStats armorStat, double mod)
        {
            return this;
        }
    }
}
