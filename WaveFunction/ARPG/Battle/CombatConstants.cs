using WaveFunction.ARPG.Chars;

namespace WaveFunction.ARPG.Battle
{
    public static class CombatConstants
    {
        public static Dictionary<TurnPhase, Action<Encounter>> TurnOrder =>
            new Dictionary<TurnPhase, Action<Encounter>>()
            {
                { TurnPhase.Action, static (enc) => enc.CurrentPhase = TurnPhase.BonusAction },
                { TurnPhase.BonusAction, static (enc) => enc.CurrentPhase = TurnPhase.Movement },
                {
                    TurnPhase.Movement, static (enc) =>
                    {
                        enc.CurrentPlayerIndex++;
                        enc.CurrentPlayerIndex %= enc.Combatants;
                        enc.CurrentPhase = TurnPhase.Action;
                    }
                }
            };
    }
}
