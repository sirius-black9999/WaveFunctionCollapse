using WaveFunction.ARPG.Chars;

namespace WaveFunction.ARPG.Items
{
    public interface IEquipment : IItemBase
    {
        EquipSlots TargetSlot();
    }
}
