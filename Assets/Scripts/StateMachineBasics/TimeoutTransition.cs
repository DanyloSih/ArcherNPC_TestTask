using System;

namespace ArcherNPC_TestTask.StateMachineBasics
{
    public class TimeoutTransition : ITransition
    {
        private IState _nextState;
        private float _waitSecconds;
        private DateTime _transitionTime;

        public TimeoutTransition(IState nextState, float waitSecconds)
        {
            _waitSecconds = waitSecconds;
            _nextState = nextState;
        }

        public void Enter()
        {
            _transitionTime = DateTime.Now + TimeSpan.FromSeconds(_waitSecconds);
        }

        public IState GetTransitedState()
            => DateTime.Now >= _transitionTime ? _nextState : null;
    }
}
