using System;
using System.Numerics;
using WaveFunction.ARPG.Characters;

namespace WaveFunctionTest.ARPG
{
    public enum ActionType
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

    public enum ActionTarget
    {
        Self,
        Ground,
        Character,
        Object
    }

    public class ActionReportTest
    {
        public ActionType Chosen;
        public ActionTarget target;
        public Action<Character> PerformAction;
        public Vector2 RelativeTarget;
    }
}
