using NUnit.Framework;
using WaveFunction.ARPG.Items;
using WaveFunction.ARPG.Worldgen;
using WaveFunction.MagicSystemSketch;

namespace WaveFunctionTest.BezierCurveMapGenerator
{
    public class BezierCurveTransformer
    {
        private static readonly Element[] elements = new[]
        {
            Element.Solidum,
            Element.Calor,
            Element.Densitas,
            Element.Entropia,
            Element.Harmonius,
            Element.Lumines,
            Element.Motus,
            Element.Natura
        };

        [Test]
        public void Signature_May_Be_Picked_From_Bezier_Curve(
            [Range(0, 7)] int e)
        {
            //Arrange
            
            var GeneratorKey = WorldgenPiece.Make.Add(ItemStore.Boots.Done)
                .Add(ItemStore.Gloves.Done)
                .Add(ItemStore.Ring.Done).Finish();
            var curve = GeneratorKey.ResultingCurve();
            //Act
            var targeted = curve.PickSignature(0, 0);

            Assert.That(targeted[elements[e]], Is.EqualTo(0));
        }

        [TestCase(Element.Calor, 0.0f, 0, -0.48f)]
        [TestCase(Element.Calor, 0.0f, 0.2f, -0.43f)]
        [TestCase(Element.Calor, 0.0f, 0.5f, -0.33f)]
        [TestCase(Element.Calor, 0.0f, 0.8f, -0.24f)]
        
        [TestCase(Element.Calor, 0.2f, 0, -0.35f)]
        [TestCase(Element.Calor, 0.2f, 0.2f, -0.34f)]
        [TestCase(Element.Calor, 0.2f, 0.5f, -0.33f)]
        [TestCase(Element.Calor, 0.2f, 0.8f, -0.31f)]
        
        [TestCase(Element.Calor, 0.5f, 0, -0.18f)]
        [TestCase(Element.Calor, 0.5f, 0.2f, -0.24f)]
        [TestCase(Element.Calor, 0.5f, 0.5f, -0.33f)]
        [TestCase(Element.Calor, 0.5f, 0.8f, -0.42f)]
        
        [TestCase(Element.Calor, 0.8f, 0, -0.06f)]
        [TestCase(Element.Calor, 0.8f, 0.2f, -0.18f)]
        [TestCase(Element.Calor, 0.8f, 0.5f, -0.36f)]
        [TestCase(Element.Calor, 0.8f, 0.8f, -0.53f)]
        
        [TestCase(Element.Solidum, 0.0f, 0, 0.03f)]
        [TestCase(Element.Solidum, 0.0f, 0.2f, 0.16f)]
        [TestCase(Element.Solidum, 0.0f, 0.5f, 0.34f)]
        [TestCase(Element.Solidum, 0.0f, 0.8f, 0.52f)]
        
        [TestCase(Element.Solidum, 0.2f, 0, 0.56f)]
        [TestCase(Element.Solidum, 0.2f, 0.2f, 0.47f)]
        [TestCase(Element.Solidum, 0.2f, 0.5f, 0.33f)]
        [TestCase(Element.Solidum, 0.2f, 0.8f, 0.17f)]
        
        [TestCase(Element.Solidum, 0.5f, 0, 0.65f)]
        [TestCase(Element.Solidum, 0.5f, 0.2f, 0.52f)]
        [TestCase(Element.Solidum, 0.5f, 0.5f,  0.34f)]
        [TestCase(Element.Solidum, 0.5f, 0.8f, 0.16f)]
        
        [TestCase(Element.Solidum, 0.8f, 0, 0.56f)]
        [TestCase(Element.Solidum, 0.8f, 0.2f, 0.47f)]
        [TestCase(Element.Solidum, 0.8f, 0.5f, 0.33f)]
        [TestCase(Element.Solidum, 0.8f, 0.8f, 0.17f)]
        public void Pick_Position_May_Be_Offset(Element e, float x, float y, float expected)
        {
            //Arrange
            
            var generatorKey = WorldgenPiece.Make
                .Add(ItemStore.Boots.Done)
                .Add(ItemStore.Gloves.WithElement(Element.Solidum, 1).Done)
                .Add(ItemStore.Ring.WithElement(Element.Calor, -1).Done)
                .Finish();
            var curve = generatorKey.ResultingCurve();
            //Act/
            var targeted = curve.PickSignature(x, y);
            Assert.That(targeted[e], Is.EqualTo(expected).Within(0.01));
        }
        
    }
}
