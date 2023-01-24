using WaveFunction.ARPG.Chars;
using WaveFunction.MagicSystemSketch;

namespace WaveFunction.ARPG.Items
{
    public class Boots : IEquipment
    {
        public Boots(BootsMaker maker)
        {
            Fundament = maker._fund;
        }
    
        public EquipSlots TargetSlot() => EquipSlots.Head;
        public Signature Fundament { get; }
    }

    public class BootsMaker
    {
        public Boots Done => new Boots(this);

        public BootsMaker WithStat(StatUtil.CharacterStats armorStat, double mod) => this;
        
        public Signature _fund = new Signature(0,0,0,0,0,0,0,0);
        
        public BootsMaker WithSignature(Signature signature)
        {
            _fund = signature;
            return this;
        }

        public BootsMaker WithElement(Element e, float value)
        {
            _fund = _fund.SetElement(e, value);
            return this;
        }
    }
}
