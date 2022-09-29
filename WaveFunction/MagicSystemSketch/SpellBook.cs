using WaveFunction.Shared;
using WaveFunction.WaveFunc;

namespace WaveFunction.MagicSystemSketch
{
    public class SpellBook
    {
        public SpellBook()
        {
            _rand = new BaseRng();
        }

        public SpellBook(IRng rand)
        {
            _rand = rand;
        }
        public SpellBook AddSpell(Spell spell)
        {
            _spellList.Add(spell);
            return this;
        }

        public int SpellCount => _spellList.Count;

        public Spell Cast(Incantation phrase)
        {
            var options = new QuantumBag<Spell>(_rand);
            
            foreach (var spell in _spellList)
            {
                options.Add(spell, spell.CastChance(phrase));
            }

            return options.Get().CastWith(phrase);
        }

        private readonly List<Spell> _spellList = new List<Spell>();
        private readonly IRng _rand;
    }
}
