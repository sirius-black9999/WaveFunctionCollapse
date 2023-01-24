using WaveFunction.ARPG.Chars;
using WaveFunction.MagicSystemSketch;

namespace WaveFunction.ARPG.Items
{
    public interface IEquipment : IItemBase
    {
        EquipSlots TargetSlot();
        
    }
    public interface IEquipmentMaker<T> : IItemBaseMaker<T>
    {
        public T WithStat(StatUtil.CharacterStats armorStat, double mod);
    }
}
