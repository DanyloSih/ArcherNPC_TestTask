using System;
using System.Collections.Generic;

namespace ArcherNPC_TestTask.StateMachineBasics
{
    public abstract class State : IState
    {
        List<ITransition> _transitions = new List<ITransition>();

        public IState GetNextState()
        {
            CheckIsTransitionsInitialized();

            foreach (var transition in _transitions)
            {
                var state = transition.GetTransitedState();
                if (state != null)
                {
                    return state;
                }
            }

            OnStateUpdated();

            return null;
        }

        public void SetTransitions(ITransition mainTransition, params ITransition[] transitions)
        {
            if (mainTransition == null || transitions == null)
            {
                throw new ArgumentNullException();
            }

            _transitions.Add(mainTransition);
            _transitions.AddRange(transitions);
        }

        public void Enter()
        {
            CheckIsTransitionsInitialized();
            foreach (var transition in _transitions)
            {
                transition.Enter();
            }
        }

        protected abstract void OnEnter();

        protected abstract void OnStateUpdated();

        private void CheckIsTransitionsInitialized()
        {
            if (_transitions.Count == 0)
            {
                throw new InvalidOperationException($"Before using {GetType().Name} state, " +
                    $"you need to initialize its transitions using method {nameof(SetTransitions)}!");
            }
        }
    }
}
