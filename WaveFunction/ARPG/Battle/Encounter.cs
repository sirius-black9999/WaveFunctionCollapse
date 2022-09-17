using WaveFunction.ARPG.Battle;

namespace WaveFunction.ARPG.Characters.Battle
{
    public class Encounter
    {
        

        public Encounter(params Character[] chars)
        {
            _chars = chars.Select(static character => new Pawn(character)).ToArray();
        }
        public void RunTurn()
        {
            Turn++;
            CurrentPlayer.PerformTurnPhase(CurrentPhase);
            CombatConstants.TurnOrder[CurrentPhase](this);
        }
        
        public int Turn { get; private set; }
        public int CurrentPlayerIndex { get; set; }
        public TurnPhase CurrentPhase { get; set; }

        public int Combatants => _chars.Length;
        private readonly Pawn[] _chars;
        private Pawn CurrentPlayer => _chars[CurrentPlayerIndex];
    }
}
