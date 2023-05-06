using System.Collections.Generic;
using ArcherNPC_TestTask.Weapons;
using UnityEngine;

namespace ArcherNPC_TestTask.Creatures
{
    public class Hog : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private WeaponsProvider _weaponsProvider;
        [Header("Animator parameters")]
        [SerializeField] private string _isEnemyDetectedParameterName = "isEnemyDetected";
        [SerializeField] private string _weaponIdParameterName = "weaponIdParameterName";

        private int _isEnemyDetectedParameterHash;
        private int? _weaponIdParameterHash;

        public Transform Target { get; private set; }
        public Weapon Weapon { get; private set; }

        public List<Weapon> GetAvailableWeapons()
            => _weaponsProvider.GetWeapons();

        public void SetTarget(Transform target)
        {
            Target = target;    
            _animator.SetBool(_isEnemyDetectedParameterHash, Target != null);
        }

        public void SetWeapon(Weapon weapon)
        {
            Weapon?.Deactivate();
            Weapon = weapon;
            if(weapon != null)
            {
                _weaponIdParameterHash = _weaponIdParameterHash??Animator.StringToHash(_weaponIdParameterName);
                _animator.SetInteger((int)_weaponIdParameterHash, Weapon.WeaponId);
                Weapon.Activate();
            }
        }

        protected void Start()
        {
            _isEnemyDetectedParameterHash = Animator.StringToHash(_isEnemyDetectedParameterName);
            _weaponIdParameterHash = _weaponIdParameterHash??Animator.StringToHash(_weaponIdParameterName);
            SetTarget(Target);
            SetWeapon(Weapon);
        }
    }
}
