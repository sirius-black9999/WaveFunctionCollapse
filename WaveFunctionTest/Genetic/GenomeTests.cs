
using NUnit.Framework;
using WaveFunction.Genetic;

namespace WaveFunctionTest.Genetic
{
    public class GenomeTests
    {
        [Test]
        public void Genome_Builder_Makes_Genome()
        {
            //Arrange
            var genome = Genome.Make.Result;
            //Act
            //Assert
            Assert.That(genome.Genes.Length, Is.EqualTo(0));
        }

        [Test]
        public void Genome_Builder_Can_Add_Genes()
        {
            //Arrange
            var genome = Genome
                .Make
                .SwapRow(1, 2, 3)
                .SwapCol(2, 3, 4)
                .Bump(3, 4, true)
                .Result;
            //Act
            //Assert
            Assert.That(genome.Genes.Length, Is.EqualTo(3));
            Assert.That(genome.Genes[0], Is.InstanceOf<SwapRow>());
            Assert.That(genome.Genes[1], Is.InstanceOf<SwapColumn>());
            Assert.That(genome.Genes[2], Is.InstanceOf<AlterVal>());
        }

        [Test]
        public void Genome_Can_Transform_Data()
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
            Assert.That(end[3, 2], Is.EqualTo(start[3, 1]));
            Assert.That(end[3, 1], Is.EqualTo(start[3, 2]));

            Assert.That(end[2, 4], Is.EqualTo(start[3, 4]));
            Assert.That(end[3, 4], Is.EqualTo(start[2, 4]));

            Assert.That(end[5, 5], Is.EqualTo(start[5, 5] + 1));
            Assert.That(end[5, 4], Is.EqualTo(start[5, 4] - 1));
        }
    }
}
