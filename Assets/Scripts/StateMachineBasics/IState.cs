using System.Collections.Generic;

namespace ArcherNPC_TestTask.StateMachineBasics
{
    public interface IState
    {
        void SetTransitions(ITransition mainTransition, params ITransition[] transitions);

        void Enter();

        IState GetNextState();
    }
}
