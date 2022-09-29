using NUnit.Framework;
using WaveFunction.Genetic;
using WaveFunction.Shared;
using WaveFunction.WaveFunc;
using WaveFunctionTest.WaveFunction;

namespace WaveFunctionTest.Genetic
{
    public class ReproductionTest
    {
        public Genome MakeRandom(Genome g1, Genome g2, IRng rng)
        {
            return Genome
                .Make
                .SwapRow(1, 2, 3)
                .SwapRow(1, 2, 3)
                .SwapCol(2, 3, 4)
                .SwapCol(2, 3, 4)
                .Bump(5, 5, true)
                .Bump(5, 4, false)
                .Result;
        }

        [Test]
        public void Population_Can_Fill()
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
            //Act
            p.Fill(6, rng, MakeRandom);
            Assert.That(p.Count, Is.EqualTo(6));
            var tracker = 0;
            p.Fill(8, rng, (g1, g2, rng) =>
            {
                tracker++;
                return Genome.Make.Result;
            });
            //Assert
            Assert.That(p.Count, Is.EqualTo(8));
            Assert.That(tracker, Is.EqualTo(2));
        }
    }
}
