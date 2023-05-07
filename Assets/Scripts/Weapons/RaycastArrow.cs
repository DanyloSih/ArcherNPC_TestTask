using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    public class RaycastArrow : Arrow
    {
        [Tooltip("The speed of advance along the calculated path trajectory.")]
        [SerializeField] private Transform _raycastOriginPoint;
        [Header("Hit filters")]
        [SerializeField] private LayersList _ignoredLayers;
        [SerializeField] private ContactFilter2D _contactFilter2D;
        [SerializeField] private List<Collider2D> _ingoredColliders;

        private List<int> _ignoredLayersIds;
        private ITrajectoryFunction _trajectoryFunction;

        public sealed override void Throw(ITrajectoryFunction trajectoryFunction)
        {
            _trajectoryFunction = trajectoryFunction;
            StartCoroutine(ThrowProcess());
        }

        protected void Start()
        {
            _ignoredLayersIds = _ignoredLayers?.GetLayers() ?? new List<int>();
        }

        /// <summary>
        /// Called every time when collider is detected by Raycast. 
        /// If this method returns true, then the arrow will stop, otherwise it will continue to fly.
        /// </summary>
        protected virtual bool IsLastHit(RaycastHit2D hit)
        {
            Debug.Log($"{name} -> Hit {hit.collider.name} and deal {Damage} damage!");
            return false;
        }

        private IEnumerator ThrowProcess()
        {
            float time = 0;
            while (true)
            {
                yield return new WaitForFixedUpdate();
                time += Time.fixedDeltaTime * InitialVelocity;
                
                Vector2 currentPosition = transform.position;
                Trajectory2DInfo nextPointInfo = _trajectoryFunction.GetTrajectoryInfoByFunction(time);
                Vector2 nextPointDirection = nextPointInfo.TrajectoryPoint - currentPosition;

                List<RaycastHit2D> raycastHits = new List<RaycastHit2D>();
                Physics2D.Raycast
                    (_raycastOriginPoint.position,
                    nextPointDirection,
                    _contactFilter2D,
                    raycastHits,
                    nextPointDirection.magnitude);

                bool isLastHit = false; 
                foreach (var hit in raycastHits)
                {
                    if (hit.collider != null && 
                        !_ignoredLayersIds.Contains(hit.collider.gameObject.layer) && 
                        !_ingoredColliders.Contains(hit.collider))
                    {
                        if (IsLastHit(hit))
                        {
                            isLastHit = true;
                            Vector3 hitPoint = new Vector3(hit.point.x, hit.point.y, 0);
                            transform.position -= (_raycastOriginPoint.position - hitPoint);
                            transform.right = nextPointInfo.TangentDirection;
                            yield break;
                        }
                    }
                }

                if (isLastHit)
                {
                    yield break;
                }

                transform.position = nextPointInfo.TrajectoryPoint;
                transform.right = nextPointInfo.TangentDirection;
            }
        }
    }
}
