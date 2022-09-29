using System.Numerics;
using NUnit.Framework;
using WaveFunction.Voronoi;

namespace WaveFunctionTest.VoronoiTest
{
    public class InfiniLineTest
    {
        [TestCase(2, 2, 2, -2, 2, 0)]
        [TestCase(2, 2, -2, 2, 0, 2)]
        [TestCase(0, 2, 2, 0, 1, 1)]
        public void Two_Infinite_Lines_May_Intersect(params float[] positions)
        {
            //Arrange
            var l1 = new InfiniLine(new Vector2(0, 0), new Vector2(positions[0], positions[1]));
            var l2 = new InfiniLine(new Vector2(0, 0), new Vector2(positions[2], positions[3]));
            //Act
            var result = l1.Intersect(l2);
            //Assert
            Assert.That(result.X, Is.EqualTo(positions[4]).Within(0.01f));
            Assert.That(result.Y, Is.EqualTo(positions[5]).Within(0.01f));
        }
    }
}
