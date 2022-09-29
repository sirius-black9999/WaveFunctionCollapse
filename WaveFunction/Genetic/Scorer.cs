using System.Diagnostics;

namespace WaveFunction.Genetic
{
    public static class Scorer
    {
        private static double[][] lists = InitLists();

        private static double[][] InitLists()
        {
            var ret = new double[6 + 6 + 2][];
            for (var index = 0; index < ret.Length; index++)
            {
                ret[index] = new double[6];
            }

            return ret;
        }

        static double[] linear = new double[6 * 6];

        public static double GetScore(this int[,] data)
        {
            Debug.Assert(data.GetLength(0) == 6, "data.GetLength(0) != 6");
            Debug.Assert(data.GetLength(1) == 6, "data.GetLength(1) != 6");
            
            for (int i = 0; i < 6; i++)
            {
                lists[0][i] = data[i, i];
                lists[1][i] = data[i, 5 - i];
                for (int j = 0; j < 6; j++)
                {
                    lists[2 * j + 2][i] = data[i, j];
                    lists[2 * j + 3][i] = data[j, i];
                    linear[i * 6 + j] = data[i, j];
                }
            }

            var avg = linear.Average();
            var deltas = lists.Select(floats => Math.Abs(floats.Average() - avg)).ToArray();
            return deltas.StdDev(deltas.Average());
        }

        static double StdDev(this double[] data, double mean)
        {
            double sum = 0;
            foreach (var f in data)
            {
                var diff = f - mean;
                sum += diff * diff;
            }

            return Math.Sqrt(sum) / data.Length;
        }
    }
}
