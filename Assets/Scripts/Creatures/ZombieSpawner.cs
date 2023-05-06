using System.Collections.Generic;
using UnityEngine;

namespace ArcherNPC_TestTask.Creatures
{
    public class ZombieSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Zombie _zombiePrefab;
        [SerializeField] private float _spawnCooldown = 3f;
        [SerializeField] private List<Transform> _spawnPoints;

        private float _timer = 0;

        protected void Update()
        {
            _timer += Time.deltaTime;   
            if (_timer >= _spawnCooldown)
            {
                _timer = 0;
                var zombieInstance = Instantiate(_zombiePrefab, _spawnPoints[Random.Range(0, _spawnPoints.Count)]);
                zombieInstance.Target = _target;
            }
        }
    }
}
