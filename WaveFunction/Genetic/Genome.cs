namespace WaveFunction.Genetic
{
    public class Genome
    {
        public IOperation<int[,]>[] Genes { get; }
        public static GenomeBuilder Make => new GenomeBuilder();
        public string Described => Genes.Select(static operation => operation.Described() + "\n")
            .Aggregate(static (s, s1) => s + s1);

        public Genome(GenomeBuilder builder)
        {
            Genes = builder.Genes.ToArray();
        }

        public int[,] Transform(int[,] start)
        {
            var temp = start.Clone() as int[,];

            foreach (var operation in Genes)
            {
                temp = operation.Transform(temp);
            }

            return temp;
        }
    }

    public class GenomeBuilder
    {
        public List<IOperation<int[,]>> Genes { get; set; } = new();
        public Genome Result => new Genome(this);

        public GenomeBuilder SwapRow(int y1, int y2, int x)
        {
            Genes.Add(new SwapRow(y1, y2, x));
            return this;
        }

        public GenomeBuilder SwapCol(int x1, int x2, int y)
        {
            Genes.Add(new SwapColumn(x1, x2, y));
            return this;
        }

        public GenomeBuilder Bump(int x, int y, bool up)
        {
            Genes.Add(new AlterVal(y, x, up));
            return this;
        }
    }
}
