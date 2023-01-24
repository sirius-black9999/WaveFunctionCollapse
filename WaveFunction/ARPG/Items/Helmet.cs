using WaveFunction.ARPG.Chars;
using WaveFunction.MagicSystemSketch;

namespace WaveFunction.ARPG.Items
{
    public class Helmet : IEquipment
    {
        public Helmet(HelmetMaker maker)
        {
            Fundament = maker._fund;
        }

        public EquipSlots TargetSlot() => EquipSlots.Head;
        public Signature Fundament { get; }
    }

    public class HelmetMaker
    {
        public Helmet Done => new Helmet(this);

        public HelmetMaker WithStat(StatUtil.CharacterStats armorStat, double mod) => this;
        
        public Signature _fund = new Signature(0,0,0,0,0,0,0,0);
        
        public HelmetMaker WithSignature(Signature signature)
        {
            _fund = signature;
            return this;
        }

        public HelmetMaker WithElement(Element e, float value)
        {
            _fund = _fund.SetElement(e, value);
            return this;
        }
    }
}
