using System;
using System.Collections.Generic;
using NUnit.Framework;
using WaveFunction.ARPG.Characters;
using WaveFunction.ARPG.Characters.Battle;

namespace WaveFunctionTest.ARPG
{
    public class EncounterTest
    {
        [Test]
        public void An_Encounter_Will_Start_On_Turn_Zero()
        {
            //Arrange
            var c1 = Character.Make.Player;
            var c2 = Character.Make.Player;
            //Act
            var e = new Encounter(c1, c2);
            //Assert
            Assert.That(e.Turn, Is.EqualTo(0));
        }

        [Test]
        public void Running_A_Turn_Will_Increase_Turn_Count()
        {
            //Arrange
            var c1 = Character.Make.Player;
            var c2 = Character.Make.Player;
            var e = new Encounter(c1, c2);
            //Act
            e.RunTurn();
            //Assert
            Assert.That(e.Turn, Is.EqualTo(1));
        }

        [Test]
        public void A_Character_May_Have_Controls()
        {
            //Arrange
            var ctrl = new PRomControl(t => new ActionReport());
            var c1 = Character.Make.Player.WithController(ctrl);
            var c2 = Character.Make.Player;
            var e = new Encounter(c1, c2);
            //Act
            e.RunTurn();
            //Assert
            Assert.That(e.Turn, Is.EqualTo(1));
        }

        [Test]
        public void A_Turn_Will_Follow_Fixed_Sequence_By_Default()
        {
            //Arrange
            var phases = new List<string>();

            var ctrl1 = new PRomControl
            (phase =>
            {
                phases.Add($"Player 1 {phase}");
                return new ActionReport();
            });
            var ctrl2 = new PRomControl
            (phase =>
            {
                phases.Add($"Player 2 {phase}");
                return new ActionReport();
            });

            var p1 = Character.Make.Player.WithController(ctrl1);
            var p2 = Character.Make.Player.WithController(ctrl2);

            var e = new Encounter(p1, p2);
            //Act
            for (int i = 0; i < 7; i++)
            {
                e.RunTurn();
            }

            //Assert
            Assert.That(phases.Count, Is.EqualTo(7));
            Assert.That(phases[0], Is.EqualTo("Player 1 Action"));
            Assert.That(phases[1], Is.EqualTo("Player 1 BonusAction"));
            Assert.That(phases[2], Is.EqualTo("Player 1 Movement"));
            Assert.That(phases[3], Is.EqualTo("Player 2 Action"));
            Assert.That(phases[4], Is.EqualTo("Player 2 BonusAction"));
            Assert.That(phases[5], Is.EqualTo("Player 2 Movement"));
            Assert.That(phases[6], Is.EqualTo("Player 1 Action"));
        }

        [Test]
        public void Detailed_Handler_May_Respond_By_Type()
        {
            //Arrange
            var action = false;
            var bonus = false;
            var movement = false;
            var ctrl = new DetailControl()
                .WithHandler(TurnPhase.Action, () =>
                {
                    action = true;
                    return new ActionReport();
                })
                .WithHandler(TurnPhase.BonusAction, () =>
                {
                    bonus = true;
                    return new ActionReport();
                })
                .WithHandler(TurnPhase.Movement, () =>
                {
                    movement = true;
                    return new ActionReport();
                });
            var e = new Encounter(Character.Make.Player.WithController(ctrl));
            //Act
            Assert.That(action, Is.False);
            Assert.That(bonus, Is.False);
            Assert.That(movement, Is.False);
            
            e.RunTurn();
            
            Assert.That(action, Is.True);
            Assert.That(bonus, Is.False);
            Assert.That(movement, Is.False);
            
            e.RunTurn();
            
            Assert.That(action, Is.True);
            Assert.That(bonus, Is.True);
            Assert.That(movement, Is.False);
            
            e.RunTurn();
            
            Assert.That(action, Is.True);
            Assert.That(bonus, Is.True);
            Assert.That(movement, Is.True);
            //Assert
            //Act
        }

        [Test]
        public void A_Char_That_Attacks_On_Their_Turn_Will_Damage_Opponent()
        {
            //Arrange
            //Act
            //Assert
        }
    }
}
