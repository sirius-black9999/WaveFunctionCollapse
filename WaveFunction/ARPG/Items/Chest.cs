using WaveFunction.ARPG.Chars;
using WaveFunction.MagicSystemSketch;

namespace WaveFunction.ARPG.Items
{
    public class Chest : IEquipment
    {
        public Chest(ChestMaker maker)
        {
            Fundament = maker._fund;
        }

        public EquipSlots TargetSlot() => EquipSlots.Head;
        public Signature Fundament { get; }
    }

    public class ChestMaker
    {
        public Chest Done => new Chest(this);

        public ChestMaker WithStat(StatUtil.CharacterStats armorStat, double mod) => this;
        
        public Signature _fund = new Signature(0,0,0,0,0,0,0,0);
        
        public ChestMaker WithSignature(Signature signature)
        {
            _fund = signature;
            return this;
        }

        public ChestMaker WithElement(Element e, float value)
        {
            _fund = _fund.SetElement(e, value);
            return this;
        }
    }
}
