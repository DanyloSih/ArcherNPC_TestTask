using ArcherNPC_TestTask.StateMachineBasics;

namespace ArcherNPC_TestTask.Weapons
{
    public class Bow : Weapon
    {
        public override IState GetInitalWeaponState(IState callbackState)
        {
            var initialState = new DebugState("Bow using");
            initialState.SetTransitions(new TimeoutTransition(callbackState, 2f));
            return initialState;
        }
    }
}
