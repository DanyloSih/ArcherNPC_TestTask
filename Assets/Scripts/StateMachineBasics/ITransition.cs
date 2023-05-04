namespace ArcherNPC_TestTask.StateMachineBasics
{
    public interface ITransition
    {
        void Enter();

        IState GetTransitedState();
    }
}
