using System;
using System.Numerics;
using WaveFunction.ARPG.Characters;

namespace WaveFunctionTest.ARPG
{
    enum ActionType
    {
        Attack,
        Skill,
        Spell,

        Block,
        Dodge,
        Negate,

        Wait,
        UsePotion,
        GearSwap
    }

    enum ActionTarget
    {
        Self,
        Ground,
        Character,
        Object
    }

    public class ActionReportTest
    {
        private ActionType Chosen;
        private ActionTarget target;
        private Action<Character> PerformAction;
        private Vector2 RelativeTarget;
    }
}
