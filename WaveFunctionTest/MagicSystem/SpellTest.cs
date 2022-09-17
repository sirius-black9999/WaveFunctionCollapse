using System;
using System.Collections.Generic;
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
                Aspect.Disis, Element.Varias.Positive());
            var cast = new Incantation();
            cast.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis)
                    .Record(Aspect.Aeolis)
                    .Resolve());
            cast.Inscribe(
                new Gesture()
                    .Record(Aspect.Disis)
                    .Record(Aspect.Spatius)
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
                Aspect.Ignis, Aspect.Aeolis,
                Aspect.Disis, Aspect.Gravitas);
            var cast = new Incantation();
            cast.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis)
                    .Record(Aspect.Aeolis)
                    .Resolve());
            cast.Inscribe(
                new Gesture()
                    .Record(Aspect.Disis)
                    .Record(Aspect.Tempus)
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
            var Template = new Incantation();
            Template.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis, 2)
                    .Record(Aspect.Aeolis, 3)
                    .Resolve());
            Template.Inscribe(
                new Gesture()
                    .Record(Aspect.Disis)
                    .Record(Aspect.Tempus, 0.5)
                    .Resolve());
            var spell = new Spell("Fireball", Template);


            var Phrase = new Incantation();
            Phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Tempus, 0.5)
                    .Record(Aspect.Aeolis, 3)
                    .Resolve());
            Phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis, 2)
                    .Resolve());
            //Act
            var result = spell.CastChance(Phrase);
            //Assert
            Assert.That(result, Is.EqualTo(7.0 / 8.0).Within(0.01));
        }

        [Test]
        public void Opposite_Push_Will_Double_Penalize()
        {
            //Arrange
            var Template = new Incantation();
            Template.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis, 2)
                    .Record(Aspect.Aeolis, 3)
                    .Resolve());
            Template.Inscribe(
                new Gesture()
                    .Record(Aspect.Disis)
                    .Record(Aspect.Tempus, 0.5)
                    .Resolve());
            var spell = new Spell("Fireball", Template);


            var Phrase = new Incantation();
            Phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis, 2)
                    .Record(Aspect.Aeolis, 3)
                    .Resolve());
            Phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Iuxta)
                    .Record(Aspect.Tempus, 0.5)
                    .Resolve());
            //Act
            var result = spell.CastChance(Phrase);
            //Assert
            Assert.That(result, Is.EqualTo(6.0 / 8.0).Within(0.01));
        }

        [Test]
        public void Minimum_Score_Is_Zero()
        {
            //Arrange
            var Template = new Incantation();
            Template.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis, 2)
                    .Record(Aspect.Aeolis, 3)
                    .Record(Aspect.Luminus, 3)
                    .Resolve());
            Template.Inscribe(
                new Gesture()
                    .Record(Aspect.Disis)
                    .Record(Aspect.Tempus, 0.5)
                    .Resolve());
            var spell = new Spell("Fireball", Template);


            var Phrase = new Incantation();
            Phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Hydris, 999)
                    .Record(Aspect.Tellus, 999)
                    .Record(Aspect.Noctis, 999)
                    .Resolve());
            Phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Iuxta, 999)
                    .Record(Aspect.Spatius, 999)
                    .Resolve());
            //Act
            var result = spell.CastChance(Phrase);
            //Assert
            Assert.That(result, Is.EqualTo(0).Within(0.01));
        }
    }

    
}
