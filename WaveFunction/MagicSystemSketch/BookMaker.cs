
using System.Collections.Immutable;
using WaveFunction.WaveFunc;

namespace WaveFunction.MagicSystemSketch
{
    public static class Bookmaker
    {
        public static SpellBook NewBook(ImmutableDictionary<string, Aspect[]> enchants)
        {
            var book = new SpellBook(new BaseRng());
            foreach (var ench in enchants)
            {
                book.AddSpell(NewSpell(ench));
            }

            return book;
        }

        private static Spell NewSpell(KeyValuePair<string, Aspect[]> ench)
        {
            var inc = new Incantation();

            inc.Inscribe(NewRune(ench.Value));

            return new Spell(ench.Key, inc);
        }

        private static Rune NewRune(Aspect[] rune)
        {
            var g = new Gesture();
            foreach (var aspect in rune)
            {
                g.Record(aspect);
            }

            return g.Resolve();
        }
    }
}
