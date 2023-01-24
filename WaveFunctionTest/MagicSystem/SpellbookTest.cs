using NUnit.Framework;
using WaveFunction.MagicSystemSketch;
using WaveFunctionTest.WaveFunction;

namespace WaveFunctionTest.MagicSystem
{
    public class SpellbookTest
    {
        [Test]
        public void Spellbook_Can_Hold_Multiple_Spells()
        {
            //Arrange
            var book = new SpellBook();
            book.AddSpell(new Spell("Fireball",
                    Aspect.Ignis, Aspect.Debilis,
                    Aspect.Tardus, Aspect.Gravis))
                .AddSpell(new Spell("Ice shard",
                    Aspect.Fortis, Aspect.Frigus,
                    Aspect.Concordia, Aspect.Aridus, Aspect.Tardus));
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
            var book = new SpellBook(rng);
            book.AddSpell(new Spell("Fireball",
                    Aspect.Ignis, Aspect.Debilis,
                    Aspect.Tardus, Aspect.Gravis))
                .AddSpell(new Spell("Ice shard",
                    Aspect.Fortis, Aspect.Frigus,
                    Aspect.Concordia, Aspect.Aridus, Aspect.Tardus))
                .AddSpell(new Spell("Lightning bolt",
                    Aspect.Ignis, Aspect.Debilis,
                    Aspect.Discordia, Aspect.Aridus, Aspect.Lux));

            var phrase = new Incantation();
            phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Ignis)
                    .Record(Aspect.Debilis)
                    .Resolve());
            phrase.Inscribe(
                new Gesture()
                    .Record(Aspect.Velox)
                    .Record(Aspect.Aridus)
                    .Resolve());
            //Act
            var held = book.Cast(phrase);
            //Assert
            Assert.That(held.Name, Is.EqualTo(expected));
        }
    }
}
