
namespace WaveFunction.ARPG.Characters.Items
{
    public class Amulet : IEquipment
    {
        public Amulet(AmuletMaker maker)
        {
        }

        public EquipSlots TargetSlot() => EquipSlots.Head;
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
