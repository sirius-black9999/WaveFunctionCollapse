using NUnit.Framework;
using NUnit.Framework.Internal;
using WaveFunction.ARPG.Items;
using WaveFunction.ARPG.Worldgen;
using WaveFunction.MagicSystemSketch;
using WaveFunction.MapGen;

namespace WaveFunctionTest.BezierCurveMapGenerator
{
    public class BezierUnfolding
    {
        [Test]
        public void XY_Lerp_Is_Inside_Bounding_Box(
            [Random(0, 1f, 3)] float X,
            [Random(0, 1f, 3)] float Y,
            [Random(1, 50, 3)] int points)
        {
            //Arrange
            var curvePts = new BezierCurveNode[points];
            for (int i = 0; i < points; i++)
            {
                curvePts[i] = new BezierCurveNode(new Signature(
                    Randomizer.Shared.NextSingle() * 2 - 1,
                    Randomizer.Shared.NextSingle() * 2 - 1,
                    Randomizer.Shared.NextSingle() * 2 - 1,
                    Randomizer.Shared.NextSingle() * 2 - 1,
                    Randomizer.Shared.NextSingle() * 2 - 1,
                    Randomizer.Shared.NextSingle() * 2 - 1,
                    Randomizer.Shared.NextSingle() * 2 - 1,
                    Randomizer.Shared.NextSingle() * 2 - 1));
            }
            var curve = new BezierCurve(curvePts);
            //Act
            var end = curve.PickSignature(X, Y);
            //Assert
            Assert.That(end.Solidum,
                Is.GreaterThanOrEqualTo(curve.Min(Element.Solidum)));
            Assert.That(end.Solidum,
                Is.LessThanOrEqualTo(curve.Max(Element.Solidum)));

            Assert.That(end.Calor,
                Is.GreaterThanOrEqualTo(curve.Min(Element.Calor)));
            Assert.That(end.Calor,
                Is.LessThanOrEqualTo(curve.Max(Element.Calor)));

            Assert.That(end.Entropia,
                Is.GreaterThanOrEqualTo(curve.Min(Element.Entropia)));
            Assert.That(end.Entropia,
                Is.LessThanOrEqualTo(curve.Max(Element.Entropia)));

            Assert.That(end.Lumines,
                Is.GreaterThanOrEqualTo(curve.Min(Element.Lumines)));
            Assert.That(end.Lumines,
                Is.LessThanOrEqualTo(curve.Max(Element.Lumines)));

            Assert.That(end.Natura,
                Is.GreaterThanOrEqualTo(curve.Min(Element.Natura)));
            Assert.That(end.Natura,
                Is.LessThanOrEqualTo(curve.Max(Element.Natura)));

            Assert.That(end.Densitas,
                Is.GreaterThanOrEqualTo(curve.Min(Element.Densitas)));
            Assert.That(end.Densitas,
                Is.LessThanOrEqualTo(curve.Max(Element.Densitas)));

            Assert.That(end.Harmonius,
                Is.GreaterThanOrEqualTo(curve.Min(Element.Harmonius)));
            Assert.That(end.Harmonius,
                Is.LessThanOrEqualTo(curve.Max(Element.Harmonius)));

            Assert.That(end.Motus,
                Is.GreaterThanOrEqualTo(curve.Min(Element.Motus)));
            Assert.That(end.Motus,
                Is.LessThanOrEqualTo(curve.Max(Element.Motus)));
        }
        [Test]
        public void X_Loops_Over_Whole_Curve_For_Any_Number_Of_Points(
            [Random(0, 1f, 3)] float X,
            [Random(0, 1f, 3)] float Y,
            [Random(0, 50, 3)] int points)
        {
            //Arrange
            var curvePts = new BezierCurveNode[points];
            for (int i = 0; i < points; i++)
            {
                curvePts[i] = new BezierCurveNode(new Signature(
                    Randomizer.Shared.NextSingle() * 2 - 1,
                    Randomizer.Shared.NextSingle() * 2 - 1,
                    Randomizer.Shared.NextSingle() * 2 - 1,
                    Randomizer.Shared.NextSingle() * 2 - 1,
                    Randomizer.Shared.NextSingle() * 2 - 1,
                    Randomizer.Shared.NextSingle() * 2 - 1,
                    Randomizer.Shared.NextSingle() * 2 - 1,
                    Randomizer.Shared.NextSingle() * 2 - 1));
            }

            var curve = new BezierCurve(curvePts);
            //Act
            var end = curve.PickSignature(X, Y);
            var end2 = curve.PickSignature(X + 1, Y);
            //Assert
            Assert.That(end.Solidum, Is.EqualTo(end2.Solidum).Within(0.01f));
            Assert.That(end.Calor, Is.EqualTo(end2.Calor).Within(0.01f));
            Assert.That(end.Entropia, Is.EqualTo(end2.Entropia).Within(0.01f));
            Assert.That(end.Lumines, Is.EqualTo(end2.Lumines).Within(0.01f));
            Assert.That(end.Natura, Is.EqualTo(end2.Natura).Within(0.01f));
            Assert.That(end.Densitas, Is.EqualTo(end2.Densitas).Within(0.01f));
            Assert.That(end.Harmonius, Is.EqualTo(end2.Harmonius).Within(0.01f));
            Assert.That(end.Motus, Is.EqualTo(end2.Motus).Within(0.01f));
        }
    }
}
