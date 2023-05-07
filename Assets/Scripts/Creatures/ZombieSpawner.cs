using System.Collections.Generic;
using UnityEngine;

namespace ArcherNPC_TestTask.Creatures
{
    public class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Zombie _zombiePrefab;
        [SerializeField] private float _spawnCooldownMin = 1.5f;
        [SerializeField] private float _spawnCooldownMax = 4.5f;
        [SerializeField] private List<Transform> _spawnPoints;

        private float _spawnCooldown = 0;
        private float _timer = 0;

        protected void Update()
        {
            _timer += Time.deltaTime;   
            if (_timer >= _spawnCooldown)
            {
                _spawnCooldown = Random.Range(_spawnCooldownMin, _spawnCooldownMax);
                _timer = 0;
                var zombieInstance = Instantiate(
                    _zombiePrefab, _spawnPoints[Random.Range(0, _spawnPoints.Count)]);

                zombieInstance.Target = _target;
            }
        }
    }
}
