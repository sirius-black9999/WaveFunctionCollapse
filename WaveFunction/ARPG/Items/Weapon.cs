using WaveFunction.ARPG.Chars;
using WaveFunction.MagicSystemSketch;

namespace WaveFunction.ARPG.Items
{
    public class Weapon : IEquipment
    {
        public Weapon(WeaponMaker maker)
        {
            
            Fundament = maker._fund;
        }

        public EquipSlots TargetSlot() => EquipSlots.Head;
        public Signature Fundament { get; }
    }

    public class WeaponMaker
    {
        public Weapon Done => new Weapon(this);

        public WeaponMaker WithStat(StatUtil.CharacterStats armorStat, double mod) => this;
        
        public Signature _fund = new Signature(0,0,0,0,0,0,0,0);
        
        public WeaponMaker WithSignature(Signature signature)
        {
            _fund = signature;
            return this;
        }

        public WeaponMaker WithElement(Element e, float value)
        {
            _fund = _fund.SetElement(e, value);
            return this;
        }
    }
}
