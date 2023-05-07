using ArcherNPC_TestTask.Creatures;
using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    public class ArrowThrower : MonoBehaviour
    {
        [SerializeField] private float _scatter = 0.2f;
        [SerializeField] private float _trajectoryHeight = 5f;
        [SerializeField] private TrajectoryDrawer _trajectoryDrawer;
        [SerializeField] private Bow _bow;
        [SerializeField] private Hog _hog;
        [SerializeField] private Transform _arrowSpawnPoint;
        [SerializeField] private Transform _arrowThrowPoint;

        private Arrow _arrowInstance;

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

            if (_hog.Target != null && _trajectoryDrawer != null)
            {
                GravityTrajectoryFunction drawableTrajectory = GetTragectory();
                _trajectoryDrawer.StartDrawing(
                    drawableTrajectory, () => UpdateTrajectory(drawableTrajectory));
            }
        }

        public void RemoveArrow()
        {
            if (_trajectoryDrawer != null)
            {
                _trajectoryDrawer.StopDrawing();
            }

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

                if (_trajectoryDrawer != null)
                {
                    _trajectoryDrawer.StopDrawing();
                }

                _arrowInstance = null;
            }
        }

        private void UpdateTrajectory(GravityTrajectoryFunction drawableTrajectory)
        {
            if (_hog.Target == null)
            {
                return;
            }

            drawableTrajectory.OriginPosition = _arrowThrowPoint.position;
            drawableTrajectory.TargetPosition = _hog.Target.position;
        }

        private GravityTrajectoryFunction GetTragectory()
        {
            return new GravityTrajectoryFunction(
                _arrowThrowPoint.position,
                _hog.Target.position + Vector3.right * Random.Range(-_scatter, _scatter),
                _trajectoryHeight);
        }
    }
}
