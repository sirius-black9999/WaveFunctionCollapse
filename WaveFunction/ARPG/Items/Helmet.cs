
namespace WaveFunction.ARPG.Characters.Items
{
    
    public class Helmet : Equipment
    {
        public Helmet(HelmetMaker maker)
        {
        }

        public EquipSlots targetSlot() => EquipSlots.Head;
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
