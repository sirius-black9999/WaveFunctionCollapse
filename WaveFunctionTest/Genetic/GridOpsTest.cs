using NUnit.Framework;
using WaveFunction.Genetic;
using WaveFunctionTest.PropertyTesting.Tooling;

namespace WaveFunctionTest.Genetic
{
    public class GridOpsTest
    {
        public static int[,] SourceData => new[,]
        {
            { 15, 7, 15, 12, 14, 10 },
            { 12, 17, 13, 14, 8, 14 },
            { 15, 7, 12, 10, 13, 13 },
            { 15, 7, 12, 11, 11, 12 },
            { 17, 14, 14, 10, 16, 8 },
            { 8, 15, 10, 13, 13, 4 }
        };

        [DatapointSource] private int[] _ => Generator<int>
            .Make(2, static r => r.Next(0, 5));

        [Theory]
        public void SwapCol_Will_Swap_Columns(int from, int to, int row)
        {
            //Arrange
            IOperation<int[,]> op = new SwapColumn(from, to, row);
            var start = SourceData;
            //Act
            var end = op.Transform(start);
            //Assert
            Assert.That(op.Cost(), Is.EqualTo(35));
            Assert.That(end[to, row], Is.EqualTo(start[from, row]));
            Assert.That(end[from, row], Is.EqualTo(start[to, row]));
        }

        [Theory]
        public void SwapRow_Will_Swap_Rows(int from, int to, int col)
        {
            //Arrange
            IOperation<int[,]> op = new SwapRow(from, to, col);
            var start = SourceData;
            //Act
            var end = op.Transform(start);
            //Assert
            Assert.That(op.Cost(), Is.EqualTo(1));
            Assert.That(end[col, to], Is.EqualTo(start[col, from]));
            Assert.That(end[col, from], Is.EqualTo(start[col, to]));
        }

        [Theory]
        public void AlterVal_May_Inc_Or_Dec_Element(int row, int col, bool up)
        {
            //Arrange
            IOperation<int[,]> op = new AlterVal(row, col, up);
            var start = SourceData;
            //Act
            var end = op.Transform(start);
            //Assert
            Assert.That(op.Cost(), Is.EqualTo(20));
            var offset = up ? 1 : -1;
            Assert.That(end[col, row], Is.EqualTo(start[col, row] + offset));
        }
    }
}
