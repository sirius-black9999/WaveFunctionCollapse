
namespace WaveFunction.ARPG.Characters.Items
{
    public class Gloves : Equipment
    {
        public Gloves(GlovesMaker maker)
        {
        }

        public EquipSlots targetSlot() => EquipSlots.Head;
    }

    public class GlovesMaker
    {
        public Gloves Done => new Gloves(this);

        public GlovesMaker WithStat(StatUtil.CharacterStats armorStat, double mod)
        {
            return this;
        }
    }
}
