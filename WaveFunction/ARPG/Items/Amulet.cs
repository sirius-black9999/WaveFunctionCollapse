using WaveFunction.ARPG.Chars;
using WaveFunction.MagicSystemSketch;

namespace WaveFunction.ARPG.Items
{
    public class Amulet : IEquipment
    {
        public Amulet(AmuletMaker maker)
        {
            Fundament = maker._fund;
        }

        public EquipSlots TargetSlot() => EquipSlots.Head;
        public Signature Fundament { get; }
    }

    public class AmuletMaker
    {
        public Amulet Done => new Amulet(this);

        public AmuletMaker WithStat(StatUtil.CharacterStats armorStat, double mod) => this;
        public Signature _fund = new Signature(0,0,0,0,0,0,0,0);
        
        public AmuletMaker WithSignature(Signature signature)
        {
            _fund = signature;
            return this;
        }

        public AmuletMaker WithElement(Element e, float value)
        {
            _fund = _fund.SetElement(e, value);
            return this;
        }
    }
}
