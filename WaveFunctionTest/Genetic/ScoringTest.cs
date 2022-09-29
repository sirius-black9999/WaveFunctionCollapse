using NUnit.Framework;
using WaveFunction.Genetic;

namespace WaveFunctionTest.Genetic
{
    public class ScoringTest
    {
        [Test]
        public void Genome_Can_Be_Scored_Directly()
        {
            //Arrange
            var genome = Genome
                .Make
                .SwapRow(1, 2, 3)
                .SwapCol(2, 3, 4)
                .Bump(5, 5, true)
                .Bump(5, 4, false)
                .Result;
            var start = GridOpsTest.SourceData;
            //Act
            var end = genome.Transform(start);
            //Assert
            Assert.That(start.GetScore(), Is.EqualTo(0.143).Within(0.001f));
            Assert.That(end.GetScore(), Is.EqualTo(0.189).Within(0.001f));
        }
    }
}
