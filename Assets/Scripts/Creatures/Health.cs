using System;
using UnityEngine;
using UnityEngine.Events;

namespace ArcherNPC_TestTask.Creatures
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float _maxHp;

        public UnityEvent Death;

        private float _hp;
        private bool _isAlive = true;

        public bool IsAlive { get => _isAlive; }
        public float Hp { get => _hp; }

        public void MakeDamage(float damage)
        {
            if (!_isAlive)
            {
                throw new InvalidOperationException("You can deal damage " +
                    "only to alive creatures!");
            }

            if (damage < 0)
            {
                throw new ArgumentException("Damage should be greater then 0!");
            }

            _hp -= damage;
            if(_hp < 0 ) 
            {
                _hp = 0;
                Death?.Invoke();
                _isAlive = false;
            }
        }

        protected void Start()
        {
            _hp = _maxHp;
            _isAlive = true;
        }
    }
}
