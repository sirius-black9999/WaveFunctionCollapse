namespace WaveFunction.ARPG.Characters
{
    public interface IController
    {
        ActionReport PerformTurnPhase(TurnPhase phase);
    }

    public class PRomControl : IController
    {
        public PRomControl(Func<TurnPhase, ActionReport> onTurn)
        {
            _onTurn = onTurn;
        }

        public ActionReport PerformTurnPhase(TurnPhase phase) => _onTurn(phase);
        private readonly Func<TurnPhase, ActionReport> _onTurn;
    }
    
    public class DetailControl : IController
    {
        public DetailControl()
        {
            _phaseHandlers = new Dictionary<TurnPhase, Func<ActionReport>>();
            foreach (var turnPhase in Enum.GetValues<TurnPhase>())
            {
                _phaseHandlers.Add(turnPhase, () => new ActionReport());
            }
        }

        public DetailControl WithHandler(TurnPhase forPhase, Func<ActionReport> handledAs)
        {
            _phaseHandlers[forPhase] = handledAs;
            return this;
        }

        public ActionReport PerformTurnPhase(TurnPhase phase) => _phaseHandlers[phase]();
        private readonly Dictionary<TurnPhase, Func<ActionReport>> _phaseHandlers;
    }
}
