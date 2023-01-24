using WaveFunction.ARPG.Chars;
using WaveFunction.MagicSystemSketch;

namespace WaveFunction.ARPG.Items
{
    public class Jewel : IEquipment
    {
        public Jewel(JewelMaker maker)
        {
            Fundament = maker._fund;
        }

        public EquipSlots TargetSlot() => EquipSlots.Head;
        public Signature Fundament { get; }
    }

    public class JewelMaker
    {
        public Jewel Done => new Jewel(this);

        public JewelMaker WithStat(StatUtil.CharacterStats armorStat, double mod) => this;
        
        public Signature _fund = new Signature(0,0,0,0,0,0,0,0);
        
        public JewelMaker WithSignature(Signature signature)
        {
            _fund = signature;
            return this;
        }

        public JewelMaker WithElement(Element e, float value)
        {
            _fund = _fund.SetElement(e, value);
            return this;
        }
    }
}
