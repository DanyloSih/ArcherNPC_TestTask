using ArcherNPC_TestTask.StateMachineBasics;
using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    public abstract class Weapon : MonoBehaviour 
    {
        /// <param name="callbackState">The state that will be activated if the weapon using loop ended.</param>
        public abstract IState GetInitalWeaponState(IState callbackState);
    }
}
