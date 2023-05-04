namespace ArcherNPC_TestTask.StateMachineBasics
{
    /// <summary>
    /// Returns the passed state each time on the first 
    /// request without any conditions. Designed for testing.
    /// </summary>
    public class UnconditionalTransition : ITransition
    {
        private IState _nextState;

        public UnconditionalTransition(IState nextState)
        {
            _nextState = nextState;
        }

        public void Enter() { }

        public IState GetTransitedState()
        {
            return _nextState;
        }
    }
}
