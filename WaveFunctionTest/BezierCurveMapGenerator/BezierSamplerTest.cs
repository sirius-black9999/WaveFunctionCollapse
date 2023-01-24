using NUnit.Framework;
using WaveFunction.ARPG.Items;
using WaveFunction.ARPG.Worldgen;
using WaveFunction.MagicSystemSketch;
using WaveFunction.Shared;
using WaveFunctionTest.ARPG;

namespace WaveFunctionTest.BezierCurveMapGenerator
{
    public class BezierSamplerTest
    {
        [TestCase(0,0,0f, Element.Calor, -0.48f)]
        [TestCase(0,0,0f, Element.Densitas, 0f)]
        [TestCase(0,0,0f, Element.Entropia, 0f)]
        [TestCase(0,0,0f, Element.Harmonius, 0f)]
        [TestCase(0,0,0f, Element.Lumines, 0f)]
        [TestCase(0,0,0f, Element.Motus, 0f)]
        [TestCase(0,0,0f, Element.Natura, 0f)]
        [TestCase(0,0,0f, Element.Solidum, 0.03f)]
        
        [TestCase(0,0,0.5f, Element.Calor, -0.46f)]
        [TestCase(0,0,0.5f, Element.Densitas, 0.21f)]
        [TestCase(0,0,0.5f, Element.Entropia, 0.06f)]
        [TestCase(0,0,0.5f, Element.Harmonius, 0.26f)]
        [TestCase(0,0,0.5f, Element.Lumines, 0.11f)]
        [TestCase(0,0,0.5f, Element.Motus, 0.30f)]
        [TestCase(0,0,0.5f, Element.Natura, 0.16f)]
        [TestCase(0,0,0.5f, Element.Solidum, 0.01f)]
        
        [TestCase(0,0.5f,0, Element.Calor, -0.34f)]
        [TestCase(0,0.5f,0, Element.Densitas, 0.0f)]
        [TestCase(0,0.5f,0, Element.Entropia, 0.0f)]
        [TestCase(0,0.5f,0, Element.Harmonius, 0.0f)]
        [TestCase(0,0.5f,0, Element.Lumines, 0.0f)]
        [TestCase(0,0.5f,0, Element.Motus, 0.0f)]
        [TestCase(0,0.5f,0, Element.Natura, 0.0f)]
        [TestCase(0,0.5f,0, Element.Solidum, 0.33f)]
        
        [TestCase(0,0.5f,0.5f, Element.Calor, -0.32f)]
        [TestCase(0,0.5f,0.5f, Element.Densitas, 0.21f)]
        [TestCase(0,0.5f,0.5f, Element.Entropia, 0.06f)]
        [TestCase(0,0.5f,0.5f, Element.Harmonius, 0.26f)]
        [TestCase(0,0.5f,0.5f, Element.Lumines, 0.11f)]
        [TestCase(0,0.5f,0.5f, Element.Motus, 0.30f)]
        [TestCase(0,0.5f,0.5f, Element.Natura, 0.16f)]
        [TestCase(0,0.5f,0.5f, Element.Solidum, 0.32f)]
        
        
        [TestCase(0.5f,0,0f, Element.Calor, -0.19f)]
        [TestCase(0.5f,0,0f, Element.Densitas, 0f)]
        [TestCase(0.5f,0,0f, Element.Entropia, 0f)]
        [TestCase(0.5f,0,0f, Element.Harmonius, 0f)]
        [TestCase(0.5f,0,0f, Element.Lumines, 0f)]
        [TestCase(0.5f,0,0f, Element.Motus, 0f)]
        [TestCase(0.5f,0,0f, Element.Natura, 0f)]
        [TestCase(0.5f,0,0f, Element.Solidum, 0.65f)]
        
        [TestCase(0.5f,0,0.5f, Element.Calor, -0.17f)]
        [TestCase(0.5f,0,0.5f, Element.Densitas, 0.21f)]
        [TestCase(0.5f,0,0.5f, Element.Entropia, 0.06f)]
        [TestCase(0.5f,0,0.5f, Element.Harmonius, 0.26f)]
        [TestCase(0.5f,0,0.5f, Element.Lumines, 0.11f)]
        [TestCase(0.5f,0,0.5f, Element.Motus, 0.30f)]
        [TestCase(0.5f,0,0.5f, Element.Natura, 0.16f)]
        [TestCase(0.5f,0,0.5f, Element.Solidum, 0.62f)]
        
        [TestCase(0.5f,0.5f,0, Element.Calor, -0.34f)]
        [TestCase(0.5f,0.5f,0, Element.Densitas, 0.0f)]
        [TestCase(0.5f,0.5f,0, Element.Entropia, 0.0f)]
        [TestCase(0.5f,0.5f,0, Element.Harmonius, 0.0f)]
        [TestCase(0.5f,0.5f,0, Element.Lumines, 0.0f)]
        [TestCase(0.5f,0.5f,0, Element.Motus, 0.0f)]
        [TestCase(0.5f,0.5f,0, Element.Natura, 0.0f)]
        [TestCase(0.5f,0.5f,0, Element.Solidum, 0.33f)]
        
        [TestCase(0.5f,0.5f,0.5f, Element.Calor, -0.32f)]
        [TestCase(0.5f,0.5f,0.5f, Element.Densitas, 0.21f)]
        [TestCase(0.5f,0.5f,0.5f, Element.Entropia, 0.06f)]
        [TestCase(0.5f,0.5f,0.5f, Element.Harmonius, 0.26f)]
        [TestCase(0.5f,0.5f,0.5f, Element.Lumines, 0.11f)]
        [TestCase(0.5f,0.5f,0.5f, Element.Motus, 0.30f)]
        [TestCase(0.5f,0.5f,0.5f, Element.Natura, 0.16f)]
        [TestCase(0.5f,0.5f,0.5f, Element.Solidum, 0.32f)]
        public void Worldgen_May_Sample_Random_Signature(float x, float y, float z, Element e, float expected)
        {
            //Arrange
            var generatorKey = WorldgenPiece.Make
                .Add(ItemStore.Boots.Done)
                .Add(ItemStore.Gloves.WithElement(Element.Solidum, 1).Done)
                .Add(ItemStore.Ring.WithElement(Element.Calor, -1).Done)
                .Finish();
            var curve = generatorKey.ResultingCurve();
            IRng rng = new SeqRng();
            //Act
            var result = curve.PickVolume(x, y, z, rng);
            //Assert
            Assert.That(result[e], Is.EqualTo(expected).Within(0.01f));
        }
    }
}
