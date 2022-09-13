
namespace WaveFunction.ARPG.Characters.Items
{
    public class Chest : Equipment
    {
        public Chest(ChestMaker maker)
        {
        }

        public EquipSlots targetSlot() => EquipSlots.Head;
    }

    public class ChestMaker
    {
        public Chest Done => new Chest(this);

        public ChestMaker WithStat(StatUtil.CharacterStats armorStat, double mod)
        {
            return this;
        }
    }
}
