using System;
using System.Linq;
using FsCheck;
using FsCheck.Fluent;
using NUnit.Framework;
using WaveFunction.MagicSystemSketch;
using WaveFunctionTest.PropertyTesting.Generators;
using WaveFunctionTest.PropertyTesting.Tooling;

namespace WaveFunctionTest.PropertyTesting.MagicProperties
{
    public class IncantationProperties
    {
        
        [DatapointSource]
        public Incantation[] GenerateSpells()
        {
            var ret = new Incantation[10];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = MagicGenerators.Incantation.Value();
            }

            return ret;
        }
        [DatapointSource]
        public Rune[] GenerateRunes()
        {
            var ret = new Rune[10];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = MagicGenerators.Rune.Value();
            }

            return ret;
        }

        [Theory]
        public void A_Spell_Made_From_An_Incantation_Has_Perfect_Stability_And_Neutrality_When_Cast_With_That_Incantation(Incantation i)
        {
            var s = new Spell("Testing", i);
            var stability = s.CastChance(i);
            var cast = s.CastWith(i);
            Assert.That(cast.Hardness, Is.EqualTo(0));
            Assert.That(cast.Heat, Is.EqualTo(0));
            Assert.That(cast.Entropy, Is.EqualTo(0));
            Assert.That(cast.Luminance, Is.EqualTo(0));
            Assert.That(cast.Manifold, Is.EqualTo(0));
            Assert.That(cast.Density, Is.EqualTo(0));
            Assert.That(cast.Risk, Is.EqualTo(0));
            Assert.That(cast.Range, Is.EqualTo(0));
            Assert.That(stability, Is.EqualTo(1));
        }
    }
}
