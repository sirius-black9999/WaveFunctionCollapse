using System;
using System.Threading;
using NUnit.Framework;
using WaveFunction.Genetic;
using WaveFunction.WaveFunc;

namespace WaveFunctionTest.Genetic
{
    public class EvolveTest
    {
        //generally don't run, this test takes a few hours to run
        // [Test]
        public void Evolutionary_Algorithm_Can_Find_Optimal_Swappage()
        {
            //Arrange
            var p = new Population()
                .Add(Genome
                    .Make
                    .SwapRow(1, 2, 3)
                    .SwapRow(1, 2, 3)
                    .SwapCol(2, 3, 4)
                    .SwapCol(2, 3, 4)
                    .Bump(5, 5, true)
                    .Bump(5, 4, false)
                    .Result)
                .Add(Genome
                    .Make
                    .SwapRow(1, 2, 3)
                    .SwapCol(2, 3, 4)
                    .Bump(5, 5, true)
                    .Bump(5, 4, false)
                    .Result)
                .Add(Genome
                    .Make
                    .Result);
            var rng = new BaseRng();


            int[,] result = GridOpsTest.SourceData;
            Console.WriteLine("Original: ");
            for (int i = 0; i < 6; i++)
                Console.WriteLine(
                    $"[{f(result[i, 0])}, \t{f(result[i, 1])}, \t{f(result[i, 2])}, \t{f(result[i, 3])}, \t{f(result[i, 4])}, \t{f(result[i, 5])}]");
            Console.WriteLine($"Score: {GridOpsTest.SourceData.GetScore()}\n");
            result = p.Best(GridOpsTest.SourceData).Transform(GridOpsTest.SourceData);

            var scoreStart = result.GetScore();


            Console.WriteLine("START: ");
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine(
                    $"[{f(result[i, 0])}, \t{f(result[i, 1])}, \t{f(result[i, 2])}, \t{f(result[i, 3])}, \t{f(result[i, 4])}, \t{f(result[i, 5])}]");
            }

            Console.WriteLine($"Score: {scoreStart}\n");

            double bestScore = double.MaxValue;
            Genome bestEver = null;
            int iterations = 1000000;
            for (int i = 0; i < iterations; i++)
            {
                p.Fill(1000, rng, EvolutionOps.MakeRandom);
                if (p.Min(GridOpsTest.SourceData) < bestScore)
                {
                    bestEver = p.Best(GridOpsTest.SourceData);
                    bestScore = bestEver.Transform(GridOpsTest.SourceData).GetScore();
                }
                
                var lerpVar = 1 - i / (float)iterations;
                p.Cull(500, GridOpsTest.SourceData, lerpVar * 0.5);
                p.Fill(950, rng, EvolutionOps.Breed);
                
            }

            var bestGenes = p.Best(GridOpsTest.SourceData);
            result = bestGenes.Transform(GridOpsTest.SourceData);
            var scoreEnd = result.GetScore();

            Console.WriteLine("END: ");
            for (int i = 0; i < 6; i++)
                Console.WriteLine(
                    $"[{f(result[i, 0])}, \t{f(result[i, 1])}, \t{f(result[i, 2])}, \t{f(result[i, 3])}, \t{f(result[i, 4])}, \t{f(result[i, 5])}]");
            Console.WriteLine($"Score: {scoreEnd}\n");
            Console.WriteLine($"Algorithm: {bestGenes.Described}");
            
            
            result = bestEver.Transform(GridOpsTest.SourceData);
            var scoreBest = result.GetScore();
            Console.WriteLine("Best ever: ");
            for (int i = 0; i < 6; i++)
                Console.WriteLine(
                    $"[{f(result[i, 0])}, \t{f(result[i, 1])}, \t{f(result[i, 2])}, \t{f(result[i, 3])}, \t{f(result[i, 4])}, \t{f(result[i, 5])}]");
            Console.WriteLine($"Score: {scoreBest}\n");
            Console.WriteLine($"Algorithm: {bestEver.Described}");


            Assert.That(scoreEnd, Is.LessThan(scoreStart));
            Assert.That(p.GeneLength, Is.GreaterThan(1));
        }

        string f(int d)
        {
            if (d < 10)
                return "0" + d;

            return "" + d;
        }
    }
}
