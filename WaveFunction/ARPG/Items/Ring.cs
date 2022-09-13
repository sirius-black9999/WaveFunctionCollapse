
namespace WaveFunction.ARPG.Characters.Items
{
    public class Ring : Equipment
    {
        public Ring(RingMaker maker)
        {
        }

        public EquipSlots targetSlot() => EquipSlots.Head;
    }

    public class RingMaker
    {
        public Ring Done => new Ring(this);

        public RingMaker WithStat(StatUtil.CharacterStats armorStat, double mod)
        {
            return this;
        }
    }
}
