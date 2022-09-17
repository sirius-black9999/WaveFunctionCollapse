
namespace WaveFunction.ARPG.Characters.Items
{
    
    public class Helmet : IEquipment
    {
        public Helmet(HelmetMaker maker)
        {
        }

        public EquipSlots TargetSlot() => EquipSlots.Head;
    }

    public class HelmetMaker
    {
        public Helmet Done => new Helmet(this);

        public HelmetMaker WithStat(StatUtil.CharacterStats armorStat, double mod)
        {
            return this;
        }
    }
}
