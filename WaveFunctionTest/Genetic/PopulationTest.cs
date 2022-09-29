using NUnit.Framework;
using WaveFunction.Genetic;

namespace WaveFunctionTest.Genetic
{
    public class PopulationTest
    {
        [Test]
        public void A_Population_Starts_Empty()
        {
            //Arrange
            var p = new Population();
            //Act
            //Assert
            Assert.That(p.Count, Is.EqualTo(0));
        }

        [Test]
        public void A_Population_May_Accept_Genome()
        {
            //Arrange
            var p = new Population();
            //Act
            p.Add(Genome
                    .Make
                    .SwapRow(1, 2, 3)
                    .SwapCol(2, 3, 4)
                    .Bump(5, 5, true)
                    .Bump(5, 4, false)
                    .Result)
                .Add(Genome
                    .Make
                    .Result);
            //Assert
            Assert.That(p.Count, Is.EqualTo(2));
            Assert.That(p.Score(GridOpsTest.SourceData, 0), Is.EqualTo(0.237).Within(0.001f));
            Assert.That(p.Score(GridOpsTest.SourceData, 1), Is.EqualTo(0.143).Within(0.001f));
            Assert.That(p.Score(GridOpsTest.SourceData), Is.EqualTo((0.237 + 0.143) / 2).Within(0.001f));
        }

        [Test]
        public void A_Population_Can_Be_Culled_Resulting_In_Lowest_Scores_Surviving()
        {
            //Arrange
            var p = new Population();

            p.Add(Genome
                    .Make
                    .SwapRow(1, 2, 3)
                    .SwapCol(2, 3, 4)
                    .Bump(5, 5, true)
                    .Bump(5, 4, false)
                    .Result)
                .Add(Genome
                    .Make
                    .Result);
            //Act
            p.Cull(1, GridOpsTest.SourceData, 0.001f);
            //Assert
            Assert.That(p.Count, Is.EqualTo(1));
            Assert.That(p.Score(GridOpsTest.SourceData), Is.EqualTo(0.143).Within(0.001f));
        }

        [Test]
        public void More_Operations_Lower_Score()
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
            //Act
            var before = p.Score(GridOpsTest.SourceData);
            p.Cull(2, GridOpsTest.SourceData, 0.001f);
            var after = p.Score(GridOpsTest.SourceData);

            //Assert
            Assert.That(p.Count, Is.EqualTo(2));
            Assert.That(after, Is.LessThan(before));
            //check with debugger that element 1 has lower weight than element 2
        }
    }
}
