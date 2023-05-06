using System;
using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    public abstract class Weapon : MonoBehaviour 
    {
        [SerializeField] private int _weaponId = 0;
        [SerializeField] private string _name = "weapon";

        private bool _isActivated;

        public int WeaponId { get => _weaponId; }
        public bool IsActivated { get => _isActivated; }
        public string WeaponName { get => _name; }

        public void Activate()
        {
            _isActivated = true;
            OnActivate();
        }

        public void Deactivate()
        {
            _isActivated = false;
            OnDeactivate();
        }

        public void CheckIsActivated()
        {
            if (!IsActivated)
            {
                throw new InvalidOperationException($"Before using object {GetType().Name}," +
                    $" first activate it with method {nameof(Activate)}!");
            }
        }

        protected virtual void OnActivate() { }

        protected virtual void OnDeactivate() { }
    }
}
