using ArcherNPC_TestTask.Creatures;
using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    public class ArrowThrower : MonoBehaviour
    {
        [SerializeField] private Bow _bow;
        [SerializeField] private Hog _hog;
        [SerializeField] private Transform _arrowSpawnPoint;
        [SerializeField] private Transform _arrowThrowPoint;

        private Arrow _arrowInstance;

        public Arrow ArrowInstance { get => _arrowInstance; }

        public void CreateArrow()
        {
            if(_bow.ArrowPrefab != null)
            {
                _arrowInstance = Instantiate(_bow.ArrowPrefab, _arrowSpawnPoint); 
            }
        }

        public void RemoveArrow()
        {
            if (_arrowInstance != null)
            {
                Destroy(_arrowInstance.gameObject);
                _arrowInstance = null;
            }
        }

        public void ThrowArrow()
        {
            if (_arrowInstance != null && _hog.Target != null)
            {
                var trajectory = new GravityTrajectoryFunction(
                    _arrowInstance.Speed,
                    _arrowInstance.Mass,
                    _arrowThrowPoint.position,
                    _hog.Target.position);

                _arrowInstance.transform.parent = null;
                _arrowInstance.Throw(trajectory);
                _arrowInstance = null;
            }
        }
    }
}
