using WaveFunction.ARPG.Chars;
using WaveFunction.MagicSystemSketch;

namespace WaveFunction.ARPG.Items
{
    public class Pants : IEquipment
    {
        public Pants(PantsMaker maker)
        {
            Fundament = maker._fund;
        }

        public EquipSlots TargetSlot() => EquipSlots.Head;
        public Signature Fundament { get; }
    }

    public class PantsMaker : IEquipmentMaker<PantsMaker>
    {
        public Pants Done => new Pants(this);
        public Signature _fund = new Signature(0,0,0,0,0,0,0,0);
        public PantsMaker WithStat(StatUtil.CharacterStats armorStat, double mod) => this;
        public PantsMaker WithSignature(Signature signature)
        {
            _fund = signature;
            return this;
        }

        public PantsMaker WithElement(Element e, float value)
        {
            _fund = _fund.SetElement(e, value);
            return this;
        }
    }
}
