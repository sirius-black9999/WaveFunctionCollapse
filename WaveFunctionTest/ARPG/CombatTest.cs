using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WaveFunction.ARPG.Characters;
using WaveFunction.ARPG.Characters.Items;
using WaveFunctionTest.PropertyTesting.Tooling;
using static System.Linq.Enumerable;
using static WaveFunction.ARPG.Characters.StatUtil;

namespace WaveFunctionTest.ARPG
{
    public class Combat
    {
        [DatapointSource] private Stat[] allStat => Generator<Stat>.MakeEnum(4);
        [DatapointSource] private AtkStats[] atkStat => Generator<AtkStats>.MakeEnum(4);
        [DatapointSource] private CharacterStats[] charStat => Generator<CharacterStats>.MakeEnum(4);
        [DatapointSource] private DamageStats[] dmgStat => Generator<DamageStats>.MakeEnum(4);

        [Theory]
        public void Base_Stats_May_Be_Set_Via_Builder([Random(10, 1000, 5)] int stat, CharacterStats s)
        {
            //Arrange
            var c = Character.Make.WithStat(s, stat).Result;
            //Act
            var hp = c[s];
            //Assert
            Assert.That(hp, Is.EqualTo(stat));
        }

        [Theory]
        public void Base_Stats_May_Be_Set_As_Function([Random(10, 1000, 5)] int stat, CharacterStats s)
        {
            //Arrange
            double temp = stat;
            var c = Character.Make.WithStat(s, () => temp).Result;
            //Act
            var hp = c[s];
            temp = 5;
            var hp2 = c[s];
            //Assert
            Assert.That(hp, Is.EqualTo(stat));
            Assert.That(hp2, Is.EqualTo(5));
        }

        [Theory]
        public void Character_May_Be_Initialized_As_Player_Default(CharacterStats stat)
        {
            //Arrange
            //Act
            var player = Character.Make.Player;
            //Assert
            Assert.That(player[stat], Is.EqualTo(100));
        }

        [Theory]
        public void Character_May_Be_Damaged_By_Attack_If_An_Attack_Stat_Is_Non_Zero(DamageStats stat,
            [Random(10, 1000, 5)] int damage)
        {
            //Arrange
            var player = Character.Make.Player;
            var attack = Attack.Make.WithStat((AtkStats)stat, damage).Result;
            //Act
            player.HitWith(attack);
            //Assert
            Assert.That(player[CharacterStats.HealthCurrent], Is.EqualTo(100 - damage));
        }

        [Theory]
        public void Multiple_Damage_Stats_Stack_Linearly(
            DamageStats stat1, [Random(10, 50, 2)] int damage1,
            DamageStats stat2, [Random(10, 50, 2)] int damage2)
        {
            //Arrange
            Assume.That(stat1 != stat2);
            Assume.That(damage1 + damage2 < 100);
            var player = Character.Make.Player;
            var attack = Attack.Make.WithStat((AtkStats)stat1, damage1).WithStat((AtkStats)stat2, damage2).Result;
            //Act
            player.HitWith(attack);
            //Assert
            Assert.That(player[CharacterStats.HealthCurrent], Is.EqualTo(100 - (damage1 + damage2)));
        }

        [Theory]
        public void Player_Gear_May_Reduce_Damage_From_Type(
            CharacterStats armorStat, [Random(-2, 2f, 2)] double mod,
            DamageStats stat1, [Random(10, 50, 2)] int damage1)
        {
            //Arrange
            var player = Character.Make.Player.WithEquipment(ItemStore.Helm.WithStat(armorStat, mod).Done);
            var attack = Attack.Make.WithStat((AtkStats)stat1, damage1).Result;
            //Act
            player.HitWith(attack);
            //Assert
            Assert.That(player[CharacterStats.HealthCurrent], Is.EqualTo(100 - (damage1)));
        }
    }
}
