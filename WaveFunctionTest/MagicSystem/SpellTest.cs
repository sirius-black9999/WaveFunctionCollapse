using NUnit.Framework;
using WaveFunction.MagicSystemSketch;

namespace WaveFunctionTest.MagicSystem
{
    public class SpellTest
    {
        [Test]
        public void A_Spell_May_Be_Compared_To_An_Incantation()
        {
            //Arrange
            var spell = new Spell("Fireball",
                Aspect.Ignis, Element.Solidum.Negative(),
                Aspect.Tardus, Element.Natura.Positive());
            var cast = new Incantation();
            cast.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis)
                    .Record(Aspect.Debilis)
                    .Resolve());
            cast.Inscribe(
                new Gesture()
                    .Record(Aspect.Tardus)
                    .Record(Aspect.Sylva)
                    .Resolve());
            //Act
            var result = spell.CastChance(cast);
            //Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Imperfect_Casts_Lower_Success_Rate()
        {
            //Arrange
            var spell = new Spell("Fireball",
                Aspect.Ignis, Aspect.Debilis,
                Aspect.Tardus, Aspect.Gravis);
            var cast = new Incantation();
            cast.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis)
                    .Record(Aspect.Debilis)
                    .Resolve());
            cast.Inscribe(
                new Gesture()
                    .Record(Aspect.Tardus)
                    .Record(Aspect.Aridus)
                    .Resolve());
            //Act
            var result = spell.CastChance(cast);
            //Assert
            //mistakes are more punished like this,
            //as only aspects provided are tested
            Assert.That(result, Is.EqualTo(3.0 / 4.0));
        }

        [Test]
        public void Spells_May_Be_Made_From_Incantations()
        {
            //Arrange
            var template = new Incantation();
            template.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis, 2)
                    .Record(Aspect.Debilis, 3)
                    .Resolve());
            template.Inscribe(
                new Gesture()
                    .Record(Aspect.Tardus)
                    .Record(Aspect.Aridus, 0.5)
                    .Resolve());
            var spell = new Spell("Fireball", template);


            var phrase = new Incantation();
            phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Aridus, 0.5)
                    .Record(Aspect.Debilis, 3)
                    .Resolve());
            phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis, 2)
                    .Resolve());
            //Act
            var result = spell.CastChance(phrase);
            //Assert
            Assert.That(result, Is.EqualTo(7.0 / 8.0).Within(0.01));
        }

        [Test]
        public void Opposite_Push_Will_Double_Penalize()
        {
            //Arrange
            var template = new Incantation();
            template.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis, 2)
                    .Record(Aspect.Debilis, 3)
                    .Resolve());
            template.Inscribe(
                new Gesture()
                    .Record(Aspect.Tardus)
                    .Record(Aspect.Aridus, 0.5)
                    .Resolve());
            var spell = new Spell("Fireball", template);


            var phrase = new Incantation();
            phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis, 2)
                    .Record(Aspect.Debilis, 3)
                    .Resolve());
            phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Velox)
                    .Record(Aspect.Aridus, 0.5)
                    .Resolve());
            //Act
            var result = spell.CastChance(phrase);
            //Assert
            Assert.That(result, Is.EqualTo(6.0 / 8.0).Within(0.01));
        }

        [Test]
        public void Minimum_Score_Is_Zero()
        {
            //Arrange
            var template = new Incantation();
            template.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis, 2)
                    .Record(Aspect.Debilis, 3)
                    .Record(Aspect.Lux, 3)
                    .Resolve());
            template.Inscribe(
                new Gesture()
                    .Record(Aspect.Tardus)
                    .Record(Aspect.Aridus, 0.5)
                    .Resolve());
            var spell = new Spell("Fireball", template);


            var phrase = new Incantation();
            phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Frigus, 999)
                    .Record(Aspect.Fortis, 999)
                    .Record(Aspect.Umbra, 999)
                    .Resolve());
            phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Velox, 999)
                    .Record(Aspect.Sylva, 999)
                    .Resolve());
            //Act
            var result = spell.CastChance(phrase);
            //Assert
            Assert.That(result, Is.EqualTo(0).Within(0.01));
        }
    }
}
