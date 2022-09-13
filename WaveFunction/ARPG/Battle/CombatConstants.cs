using WaveFunction.ARPG.Characters;
using WaveFunction.ARPG.Characters.Battle;

namespace WaveFunction.ARPG.Battle
{
    public static class CombatConstants
    {
        public static Dictionary<TurnPhase, Action<Encounter>> TurnOrder =>
            new Dictionary<TurnPhase, Action<Encounter>>()
            {
                { TurnPhase.Action, (enc) => enc.currentPhase = TurnPhase.BonusAction },
                { TurnPhase.BonusAction, (enc) => enc.currentPhase = TurnPhase.Movement },
                {
                    TurnPhase.Movement, (enc) =>
                    {
                        enc.currentPlayer++;
                        enc.currentPlayer %= enc._chars.Length;
                        enc.currentPhase = TurnPhase.Action;
                    }
                }
            };
    }
}
