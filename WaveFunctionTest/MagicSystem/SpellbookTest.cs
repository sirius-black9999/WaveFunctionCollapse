
using NUnit.Framework;
using WaveFunction.MagicSystemSketch;

namespace WaveFunctionTest.MagicSystem
{
    public class SpellbookTest
    {
        [Test]
        public void Spellbook_Can_Hold_Multiple_Spells()
        {
            //Arrange
            var book = new Spellbook();
            book.AddSpell(new Spell("Fireball",
                    Aspect.Ignis, Aspect.Aeolis,
                    Aspect.Disis, Aspect.Gravitas))
                .AddSpell(new Spell("Ice shard",
                    Aspect.Tellus, Aspect.Hydris,
                    Aspect.Vitrio, Aspect.Tempus, Aspect.Disis));
            //Act
            var held = book.SpellCount;
            //Assert
            Assert.That(held, Is.EqualTo(2));
        }
        [TestCase(0, "Fireball")]
        [TestCase(0.1, "Fireball")]
        [TestCase(0.2, "Fireball")]
        [TestCase(0.3, "Fireball")]
        [TestCase(0.4, "Fireball")]
        [TestCase(0.5, "Ice shard")]
        [TestCase(0.6, "Lightning bolt")]
        [TestCase(0.7, "Lightning bolt")]
        [TestCase(0.8, "Lightning bolt")]
        [TestCase(0.9, "Lightning bolt")]
        [TestCase(1, "Lightning bolt")]
        public void Spellbook_Will_Cast_Spell_Based_On_Spell(double val, string expected)
        {
            //Arrange
            PRng rng = new PRng(val);
            var book = new Spellbook(rng);
            book.AddSpell(new Spell("Fireball",
                    Aspect.Ignis, Aspect.Aeolis,
                    Aspect.Disis, Aspect.Gravitas))
                .AddSpell(new Spell("Ice shard",
                    Aspect.Tellus, Aspect.Hydris,
                    Aspect.Vitrio, Aspect.Tempus, Aspect.Disis))
                .AddSpell(new Spell("Lightning bolt",
                    Aspect.Ignis, Aspect.Aeolis,
                    Aspect.Empyrus, Aspect.Tempus, Aspect.Luminus));
            
            var Phrase = new Incantation();
            Phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis)
                    .Record(Aspect.Aeolis)
                    .Resolve());
            Phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Iuxta)
                    .Record(Aspect.Tempus)
                    .Resolve());
            //Act
            var held = book.Cast(Phrase);
            //Assert
            Assert.That(held.Name, Is.EqualTo(expected));
        }
    }
}
