
namespace WaveFunction.ARPG.Characters.Items
{
    public class Chest : IEquipment
    {
        public Chest(ChestMaker maker)
        {
        }

        public EquipSlots TargetSlot() => EquipSlots.Head;
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
