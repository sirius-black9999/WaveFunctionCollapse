using WaveFunction.Shared;
using WaveFunction.WaveFunc;

namespace WaveFunction.Genetic
{
    public static class EvolutionOps
    {
        public static Genome MakeRandom(Genome g1, Genome g2, IRng rng)
        {
            var numEntries = rng.Next() * 10+1;
            var builder = Genome.Make;
            for (int i = 0; i < numEntries; i++)
            {
                if (rng.Next() > 0.5)
                {
                    builder.SwapCol((int)(rng.Next() * 6), (int)(rng.Next() * 6), (int)(rng.Next() * 6));
                    continue;
                }

                if (rng.Next() > 0.5)
                {
                    builder.SwapRow((int)(rng.Next() * 6), (int)(rng.Next() * 6), (int)(rng.Next() * 6));
                    continue;
                }

                builder.Bump((int)(rng.Next() * 6), (int)(rng.Next() * 6), rng.Next() > 0.5);
            }

            return builder.Result;
        }

        public static Genome Breed(Genome g1, Genome g2, IRng rng)
        {
            var gene1 = g1.Genes;
            var gene2 = g2.Genes;

            var slice1 = rng.Next() * gene1.Length;
            var slice2 = rng.Next() * gene2.Length;

            var sliced1 = gene1.Take((int)slice1);
            var sliced2 = gene2.Take((int)slice2);
            var merged = sliced1.Union(sliced2);
            var builder = Genome.Make;
            builder.Genes = merged.ToList();
            var ret = builder.Result;
            if (ret.Genes.Length == 0) return MakeRandom(g1, g2, rng);

            return ret;
        }
    }
}
