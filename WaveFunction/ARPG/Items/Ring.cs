using WaveFunction.ARPG.Chars;
using WaveFunction.MagicSystemSketch;

namespace WaveFunction.ARPG.Items
{
    public class Ring : IEquipment
    {
        public Ring(RingMaker maker)
        {
            Fundament = maker._fund;
        }

        public EquipSlots TargetSlot() => EquipSlots.Head;
        public Signature Fundament { get; } 
    }

    public class RingMaker
    {
        public Ring Done => new Ring(this);

        public RingMaker WithStat(StatUtil.CharacterStats armorStat, double mod) => this;
        
        public Signature _fund = new Signature(0,0,0,0,0,0,0,0);
        
        public RingMaker WithSignature(Signature signature)
        {
            _fund = signature;
            return this;
        }

        public RingMaker WithElement(Element e, float value)
        {
            _fund = _fund.SetElement(e, value);
            return this;
        }
    }
}
