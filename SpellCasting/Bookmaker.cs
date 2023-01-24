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
                    Aspect.Debilis, Aspect.Ignis, Aspect.Discordia, Aspect.Lux, Aspect.Sylva, Aspect.Levis,
                    Aspect.Malum, Aspect.Tardus
                }
            },
            {
                "Ice shard",
                new[]
                {
                    Aspect.Fortis, Aspect.Frigus, Aspect.Concordia, Aspect.Lux, Aspect.Sylva, Aspect.Gravis,
                    Aspect.Malum, Aspect.Tardus
                }
            },
            {
                "Lightning",
                new[]
                {
                    Aspect.Fortis, Aspect.Ignis, Aspect.Discordia, Aspect.Lux, Aspect.Aridus, Aspect.Levis,
                    Aspect.Malum, Aspect.Velox
                }
            },
            {
                "Slow",
                new[]
                {
                    Aspect.Fortis, Aspect.Frigus, Aspect.Concordia, Aspect.Umbra, Aspect.Aridus, Aspect.Gravis,
                    Aspect.Malum, Aspect.Velox
                }
            },
            {
                "Acid",
                new[]
                {
                    Aspect.Debilis, Aspect.Ignis, Aspect.Discordia, Aspect.Umbra, Aspect.Sylva, Aspect.Gravis,
                    Aspect.Malum, Aspect.Velox
                }
            },
            {
                "Healing touch",
                new[]
                {
                    Aspect.Debilis, Aspect.Frigus, Aspect.Concordia, Aspect.Lux, Aspect.Aridus, Aspect.Levis,
                    Aspect.Bonum, Aspect.Velox
                }
            },
            {
                "Healing word",
                new[]
                {
                    Aspect.Debilis, Aspect.Frigus, Aspect.Concordia, Aspect.Lux, Aspect.Aridus, Aspect.Levis,
                    Aspect.Bonum, Aspect.Tardus
                }
            }
        }.ToImmutableDictionary();
    }
}
