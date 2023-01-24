using NUnit.Framework;
using WaveFunction.ARPG.Items;
using WaveFunction.ARPG.Worldgen;
using WaveFunction.MagicSystemSketch;

namespace WaveFunctionTest.BezierCurveMapGenerator
{
    public class BezierParamTest
    {
        [TestCase(Element.Harmonius, 0.0f, 0.65f)]
        [TestCase(Element.Densitas, 0.0f, -0.17f)]
        [TestCase(Element.Harmonius, 0.2f, 0.41f)]
        [TestCase(Element.Densitas, 0.2f, -0.54f)]
        [TestCase(Element.Harmonius, 0.5f, 0.04f)]
        [TestCase(Element.Densitas, 0.5f, -0.48f)]
        [TestCase(Element.Harmonius, 0.8f, 0.41f)]
        [TestCase(Element.Densitas, 0.8f, -0.04f)]
        [TestCase(Element.Harmonius, 1f, 0.65f)]
        [TestCase(Element.Densitas, 1f, -0.17f)]
        public void Bezier_Curve_May_Be_Parameterized(Element e, float t,
            float expected)
        {
            //Arrange

            var GeneratorKey = WorldgenPiece.Make
                .Add(ItemStore.Boots.Done)
                .Add(ItemStore.Gloves.WithElement(Element.Harmonius, 1).Done)
                .Add(ItemStore.Ring.WithElement(Element.Densitas, -1).Done)
                .Finish();
            var result = GeneratorKey.ResultingCurve();
            //Act
            var pointOnCurve = result.PointOnCurve(t);
            //Assert
            Assert.That(pointOnCurve[e], Is.EqualTo(expected).Within(0.01));
        }
    }
}
