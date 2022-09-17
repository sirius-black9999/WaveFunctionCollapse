
namespace WaveFunction.ARPG.Characters.Items
{
    public class Belt : IEquipment
    {
        public Belt(BeltMaker maker)
        {
        }

        public EquipSlots TargetSlot() => EquipSlots.Head;
    }

    public class BeltMaker
    {
        public Belt Done => new Belt(this);

        public BeltMaker WithStat(StatUtil.CharacterStats armorStat, double mod)
        {
            return this;
        }
    }
}
