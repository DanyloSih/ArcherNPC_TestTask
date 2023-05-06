using ArcherNPC_TestTask.Creatures;
using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    public class ArrowThrower : MonoBehaviour
    {
        [SerializeField] private float _scatter = 0.2f;
        [SerializeField] private TrajectoryDrawer _trajectoryDrawer;
        [SerializeField] private Bow _bow;
        [SerializeField] private Hog _hog;
        [SerializeField] private Transform _arrowSpawnPoint;
        [SerializeField] private Transform _arrowThrowPoint;

        private Arrow _arrowInstance;
        private Vector2 _targetPos;

        public Arrow ArrowInstance { get => _arrowInstance; }

        public void CreateArrow()
        {
            if (_arrowInstance != null)
            {
                Destroy(_arrowInstance.gameObject);
            }

            if (_bow.ArrowPrefab != null)
            {
                _arrowInstance = Instantiate(
                    _bow.ArrowPrefab, _arrowSpawnPoint.position, _arrowSpawnPoint.rotation);
                _arrowInstance.transform.parent = _arrowSpawnPoint;
            }

            GravityTrajectoryFunction drawableTrajectory = GetTragectory();
            _trajectoryDrawer.StartDrawing(
                drawableTrajectory, _hog.Target.position, () => UpdateTrajectory(drawableTrajectory));
        }

        public void RemoveArrow()
        {
            _trajectoryDrawer.StopDrawing();
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
                var trajectory = GetTragectory();

                _arrowInstance.transform.parent = null;
                _arrowInstance.Throw(trajectory);
                _arrowInstance = null;
            }
        }

        private void UpdateTrajectory(GravityTrajectoryFunction drawableTrajectory)
        {
            if (_hog.Target == null)
            {
                return;
            }

            _targetPos = _hog.Target.position;
            drawableTrajectory.OriginPosition = _arrowThrowPoint.position;
            drawableTrajectory.TargetPosition = _targetPos;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(_targetPos, 0.5f);
        }

        private GravityTrajectoryFunction GetTragectory()
        {
            return new GravityTrajectoryFunction(
                _arrowThrowPoint.position,
                _hog.Target.position);
        }
    }
}
