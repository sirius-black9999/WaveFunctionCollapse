using NUnit.Framework;
using WaveFunction.MagicSystemSketch;

namespace WaveFunctionTest.MagicSystem
{
    public class RuneTest
    {
        [Test]
        public void A_Gesture_Can_Record_Aspects_With_A_Quality()
        {
            var g = new Gesture();
            g.Record(Aspect.Ignis, 0.75);
            g.Record(Aspect.Tellus, 3);

            Assert.That(g.Aspects[Aspect.Ignis], Is.EqualTo(0.75).Within(0.1));
            Assert.That(g.Aspects[Aspect.Tellus], Is.EqualTo(3).Within(0.1));
            Assert.That(g.Aspects.ContainsKey(Aspect.Hydris), Is.False);
        }

        [TestCase(Element.Febris, 0.75)]
        [TestCase(Element.Solidum, 3)]
        [TestCase(Element.Ordinem, 0)]
        public void Gesture_May_Become_Rune(Element tested, double expected)
        {
            var g = new Gesture();
            g.Record(Aspect.Ignis, 0.75);
            g.Record(Aspect.Tellus, 3);
            var r = g.Resolve();
            Assert.That(r.PushForce(tested), Is.EqualTo(expected).Within(0.1));
        }

        [TestCase(Element.Febris, 1)]
        [TestCase(Element.Ordinem, 3)]
        [TestCase(Element.Lumines, 0)]
        [TestCase(Element.Solidum, 2)]
        public void Multiple_Runes_May_Form_An_Incantation(Element tested, double strength)
        {
            var cast = new Incantation();
            cast.Inscribe(new Gesture().Record(Aspect.Ignis).Record(Aspect.Tellus).Resolve());
            cast.Inscribe(new Gesture().Record(Aspect.Vitrio, 3).Record(Aspect.Tellus).Resolve());
            Assert.That(cast.Element(tested), Is.EqualTo(strength));
        }

        [TestCase(Element.Febris, 0.5)]
        [TestCase(Element.Solidum, -0.75)]
        public void Opposite_Aspects_Affect_Eachother(Element tested, double strength)
        {
            var cast = new Incantation();
            cast.Inscribe(new Gesture().Record(Aspect.Ignis).Record(Aspect.Tellus).Resolve());
            cast.Inscribe(new Gesture().Record(Aspect.Hydris, 0.5).Record(Aspect.Aeolis, 1.75).Resolve());
            Assert.That(cast.Element(tested), Is.EqualTo(strength));
        }

        [Test]
        public void An_Incantation_May_Be_Inversed()
        {
            //Arrange
            //Act
            //Assert
            Assert.Warn("NOT IMPLEMENTED");
        }
    }
}
