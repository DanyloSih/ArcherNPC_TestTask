using ArcherNPC_TestTask.StateMachineBasics;
using ArcherNPC_TestTask.Weapons;
using UnityEngine;

namespace ArcherNPC_TestTask.Creatures
{
    public class Archer : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;

        private StateMachine _stateMachine;
        private IState _initialWeaponState;
        private IState _idleState;

        protected void Start()
        {
            _idleState = new DebugState("Archer Idle");
            _initialWeaponState = _weapon.GetInitalWeaponState(_idleState);
            _idleState.SetTransitions(new TimeoutTransition(_initialWeaponState, 2f));
            _stateMachine = new StateMachine(_idleState);
        }

        protected void Update() 
        {
            _stateMachine.UpdateState();
        }
    }
}
