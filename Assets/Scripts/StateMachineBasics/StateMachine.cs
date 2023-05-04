namespace ArcherNPC_TestTask.StateMachineBasics
{
    public class StateMachine
    {
        private IState _currentState;

        public StateMachine(IState initialState)
        {
            _currentState = initialState;
        }

        public void UpdateState()
        {
            IState newState = _currentState.GetNextState();
            if(newState != null)
            {
                _currentState = newState;
                newState.Enter();
            }
        }
    }
}
