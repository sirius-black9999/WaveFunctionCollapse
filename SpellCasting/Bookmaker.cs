using WaveFunction;
using WaveFunction.MagicSystemSketch;
using WaveFunction.WaveFunc;

namespace SpellCasting
{
    public static class Bookmaker
    {
        private static readonly Dictionary<string, Aspect[][]> Enchants = new Dictionary<string, Aspect[][]>()
        {
            {
                "Fireball",
                new[]
                {
                    new[]
                    {
                        Aspect.Aeolis, Aspect.Ignis, Aspect.Empyrus, Aspect.Luminus, Aspect.Spatius, Aspect.Levitas,
                        Aspect.Malus, Aspect.Disis
                    }
                }
            },
            {
                "Ice shard",
                new[]
                {
                    new[]
                    {
                        Aspect.Tellus, Aspect.Hydris, Aspect.Vitrio, Aspect.Luminus, Aspect.Spatius, Aspect.Gravitas,
                        Aspect.Malus, Aspect.Disis
                    }
                }
            },
            {
                "Lightning",
                new[]
                {
                    new[]
                    {
                        Aspect.Tellus, Aspect.Ignis, Aspect.Empyrus, Aspect.Luminus, Aspect.Tempus, Aspect.Levitas,
                        Aspect.Malus, Aspect.Iuxta
                    }
                }
            },
            {
                "Slow",
                new[]
                {
                    new[]
                    {
                        Aspect.Tellus, Aspect.Hydris, Aspect.Vitrio, Aspect.Noctis, Aspect.Tempus, Aspect.Gravitas,
                        Aspect.Malus, Aspect.Iuxta
                    }
                }
            },
            {
                "Acid",
                new[]
                {
                    new[]
                    {
                        Aspect.Aeolis, Aspect.Ignis, Aspect.Empyrus, Aspect.Noctis, Aspect.Spatius, Aspect.Gravitas,
                        Aspect.Malus, Aspect.Iuxta
                    }
                }
            },
            {
                "Healing touch",
                new[]
                {
                    new[]
                    {
                        Aspect.Aeolis, Aspect.Hydris, Aspect.Vitrio, Aspect.Luminus, Aspect.Tempus, Aspect.Levitas,
                        Aspect.Auxillus, Aspect.Iuxta
                    }
                }
            },
            {
                "Healing word",
                new[]
                {
                    new[]
                    {
                        Aspect.Aeolis, Aspect.Hydris, Aspect.Vitrio, Aspect.Luminus, Aspect.Tempus, Aspect.Levitas,
                        Aspect.Auxillus, Aspect.Disis
                    }
                }
            },
        };

        public static Spellbook NewBook()
        {
            var book = new Spellbook(new BaseRng());
            foreach (var ench in Enchants)
            {
                book.AddSpell(NewSpell(ench));
            }
            return book;
        }

        private static Spell NewSpell(KeyValuePair<string, Aspect[][]> ench)
        {
            var inc = new Incantation();
            foreach (var rune in ench.Value)
            {
                inc.Inscribe(NewRune(rune));
            }
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
