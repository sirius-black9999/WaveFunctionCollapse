using System.Collections.Immutable;
using WaveFunction.MagicSystemSketch;
using WaveFunction.WaveFunc;

namespace SpellCasting
{
    public static class SpellBooks
    {
        public static readonly ImmutableDictionary<string, Aspect[]> Enchants = new Dictionary<string, Aspect[]>()
        {
            {
                "Fireball",
                new[]
                {
                    Aspect.Aeolis, Aspect.Ignis, Aspect.Empyrus, Aspect.Luminus, Aspect.Spatius, Aspect.Levitas,
                    Aspect.Malus, Aspect.Disis
                }
            },
            {
                "Ice shard",
                new[]
                {
                    Aspect.Tellus, Aspect.Hydris, Aspect.Vitrio, Aspect.Luminus, Aspect.Spatius, Aspect.Gravitas,
                    Aspect.Malus, Aspect.Disis
                }
            },
            {
                "Lightning",
                new[]
                {
                    Aspect.Tellus, Aspect.Ignis, Aspect.Empyrus, Aspect.Luminus, Aspect.Tempus, Aspect.Levitas,
                    Aspect.Malus, Aspect.Iuxta
                }
            },
            {
                "Slow",
                new[]
                {
                    Aspect.Tellus, Aspect.Hydris, Aspect.Vitrio, Aspect.Noctis, Aspect.Tempus, Aspect.Gravitas,
                    Aspect.Malus, Aspect.Iuxta
                }
            },
            {
                "Acid",
                new[]
                {
                    Aspect.Aeolis, Aspect.Ignis, Aspect.Empyrus, Aspect.Noctis, Aspect.Spatius, Aspect.Gravitas,
                    Aspect.Malus, Aspect.Iuxta
                }
            },
            {
                "Healing touch",
                new[]
                {
                    Aspect.Aeolis, Aspect.Hydris, Aspect.Vitrio, Aspect.Luminus, Aspect.Tempus, Aspect.Levitas,
                    Aspect.Auxillus, Aspect.Iuxta
                }
            },
            {
                "Healing word",
                new[]
                {
                    Aspect.Aeolis, Aspect.Hydris, Aspect.Vitrio, Aspect.Luminus, Aspect.Tempus, Aspect.Levitas,
                    Aspect.Auxillus, Aspect.Disis
                }
            }
        }.ToImmutableDictionary();
    }
}
