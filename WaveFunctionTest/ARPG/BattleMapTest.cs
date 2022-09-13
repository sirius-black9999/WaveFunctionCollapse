using System;
using System.Numerics;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WaveFunction;
using WaveFunction.ARPG.Battle;
using WaveFunction.ARPG.Characters;
using WaveFunction.Shared;
using WaveFunctionTest.PropertyTesting.Tooling;

namespace WaveFunctionTest.ARPG
{
    class SeqRng : RNG
    {
        public double next() => i++;
        private int i;
    }
    public class BattleMapTest
    {
        [DatapointSource] private Direction[] allStat => Enum.GetValues<Direction>();

        [DatapointSource] private int[] Directions => Generator<int>.Make(3, randomizer => randomizer.Next(0, 256));

        [Theory]
        public void Default_Passability_Is_False()
        {
            //Arrange
            var t = Tile.Make.Result;
            //Act
            var result = t.MayPass;
            //Assert
            Assert.That(result, Is.False);
        }

        [Theory]
        public void SetPassability_Makes_Passable()
        {
            //Arrange
            //Act
            var t = Tile.Make.CanPass(true).Result;
            //Assert
            Assert.That(t.MayPass, Is.True);
        }

        [Test]
        public void BattleMaps_Have_Default_Size_Of_256_By_256()
        {
            //Arrange
            var b = BattleMap.Make.Result;
            //Act
            var result = b.Size;
            //Assert
            Assert.That(result.X, Is.EqualTo(256));
            Assert.That(result.Y, Is.EqualTo(256));
        }
        [Test]
        public void BattleMap_Size_May_Be_Doubled()
        {
            //Arrange
            var b = BattleMap.Make.DoubleWidth.Result;
            //Act
            var result = b.Size;
            //Assert
            Assert.That(result.X, Is.EqualTo(512));
            Assert.That(result.Y, Is.EqualTo(256));
        }

        [Theory]
        public void Blank_Tiles_Are_Black(int x, int y)
        {
            //Arrange
            var b = BattleMap.Make.Result;
            //Act
            var result = b.GetCol(x, y);
            //Assert
            Assert.That(result.X, Is.EqualTo(0));
            Assert.That(result.Y, Is.EqualTo(0));
            Assert.That(result.Z, Is.EqualTo(0));
        }
        
        
        [Theory]
        public void Random_Will_Call_Randomize_65535_Times(int x, int y)
        {
            //Arrange

            var rng = new SeqRng();
            var b = BattleMap.Make.Randomized(rng).Result;
            //Act
            var result = b.GetCol(x, y);
            //Assert
            var expectedIndex = b.Size.IndexOf(x, y) * 3;
            
            Assert.That(result.X, Is.EqualTo(expectedIndex));
            Assert.That(result.Y, Is.EqualTo(expectedIndex+1));
            Assert.That(result.Z, Is.EqualTo(expectedIndex+2));
        }
    }
}
