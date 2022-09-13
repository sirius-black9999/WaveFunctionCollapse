using WaveFunction.ARPG.Battle;

namespace WaveFunction.ARPG.Characters.Battle
{
    public class Encounter
    {
        public Pawn[] _chars;

        public Encounter(params Character[] chars)
        {
            _chars = chars.Select(static character => new Pawn(character)).ToArray();
        }
        public void RunTurn()
        {
            Turn++;
            CurrentPlayer.PerformTurnPhase(currentPhase);
            CombatConstants.TurnOrder[currentPhase](this);
        }
        
        public int Turn { get; private set; }
        public int currentPlayer;
        public TurnPhase currentPhase;
        private Pawn CurrentPlayer => _chars[currentPlayer];
    }
}
