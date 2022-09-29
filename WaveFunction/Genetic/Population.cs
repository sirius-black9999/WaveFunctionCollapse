using WaveFunction.Shared;
using WaveFunction.WaveFunc;

namespace WaveFunction.Genetic
{
    public class Population
    {
        private readonly List<Genome> _genotypes = new List<Genome>();
        public int Count => _genotypes.Count;

        public Genome Best(int[,] data) =>
            _genotypes
                .First(genome => Math.Abs(genome.Transform(data).GetScore() * CostFactor(genome) - Min(data)) < 0.001f);

        public Population Add(Genome result)
        {
            _genotypes.Add(result);
            return this;
        }

        public double Score(int[,] data) =>
            _genotypes.Average(genome => genome.Transform(data).GetScore() * CostFactor(genome));

        public double Score(int[,] data, int index) =>
            _genotypes[index].Transform(data).GetScore() * CostFactor(_genotypes[index]);

        public double Max(int[,] data) =>
            _genotypes.Max(genome => genome.Transform(data).GetScore() * CostFactor(genome));

        public double Min(int[,] data) =>
            _genotypes.Min(genome => genome.Transform(data).GetScore() * CostFactor(genome));


        public double GeneLength => _genotypes.Average(static g => g.Genes.Length);

        public void Cull(int survivors, int[,] data, double blending = 0.1)
        {
            var genes = new QuantumBag<Genome>() { Erasing = true };
            var maxScore = Max(data);
            foreach (var genotype in _genotypes)
            {
                if (genotype.Genes.Length == 0)
                {
                    genes.Add(genotype, 0.0001f);
                }

                var score = genotype.Transform(data).GetScore() / maxScore;
                score = -score + 1f;
                score /= CostFactor(genotype);

                genes.Add(genotype, score + blending);
            }

            _genotypes.Clear();
            for (int i = 0; i < survivors; i++)
            {
                _genotypes.Add(genes.Get());
            }
        }

        private static float CostFactor(Genome genotype)
        {
            return genotype.Genes.Sum(static s => s.Cost()) / 300f + 1;
        }

        private Func<Tuple<T, T>, T> Unwrap<T>(Func<T, T, IRng, T> src, IRng rng)
        {
            return t => src(t.Item1, t.Item2, rng);
        }

        public void Fill(int fillUntil, IRng rng, Func<Genome, Genome, IRng, Genome> breedFunc)
        {
            var toCreate = fillUntil - Count;
            var breedPairs = new Tuple<Genome, Genome>[toCreate];
            for (int i = 0; i < toCreate; i++)
            {
                var parent1 = _genotypes[(int)Math.Floor(rng.Next() * Count)];
                var parent2 = _genotypes[(int)Math.Floor(rng.Next() * Count)];
                breedPairs[i] = new Tuple<Genome, Genome>(parent1, parent2);
            }

            _genotypes.AddRange(breedPairs.Select(Unwrap(breedFunc, rng)));
        }
    }
}
