using WaveFunction.WaveFunc;

namespace WaveFunction.MagicSystemSketch
{
    public class Spellbook
    {
        public Spellbook()
        {
            _rand = new BaseRng();
        }

        public Spellbook(IRng rand)
        {
            _rand = rand;
        }

        public Spellbook AddSpell(Spell spell)
        {
            _spellList.Add(spell);
            return this;
        }

        private readonly List<Spell> _spellList = new List<Spell>();
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

        private readonly IRng _rand;
    }
}
