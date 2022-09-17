using WaveFunction.ARPG.Characters;
using WaveFunction.ARPG.Characters.Battle;

namespace WaveFunction.ARPG.Battle
{
    public static class CombatConstants
    {
        public static Dictionary<TurnPhase, Action<Encounter>> TurnOrder =>
            new Dictionary<TurnPhase, Action<Encounter>>()
            {
                { TurnPhase.Action, (enc) => enc.CurrentPhase = TurnPhase.BonusAction },
                { TurnPhase.BonusAction, (enc) => enc.CurrentPhase = TurnPhase.Movement },
                {
                    TurnPhase.Movement, (enc) =>
                    {
                        enc.CurrentPlayerIndex++;
                        enc.CurrentPlayerIndex %= enc.Combatants;
                        enc.CurrentPhase = TurnPhase.Action;
                    }
                }
            };
    }
}
