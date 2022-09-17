using System;
using System.Numerics;
using WaveFunction.ARPG.Chars;

namespace WaveFunctionTest.ARPG
{
    public class ActionReportTest
    {
        public ActionType Chosen;
        public ActionTarget Target;
        public Action<Character> PerformAction = static c => { };
        public Vector2 RelativeTarget;
    }
}
