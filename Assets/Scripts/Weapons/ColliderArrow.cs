using System.Collections;
using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class ColliderArrow : Arrow
    {
        [SerializeField] private float _mass = 0.1f;

        private ITrajectoryFunction _trajectoryFunction;
        private Coroutine _throwProcessCoroutine;

        public override float Mass { get => _mass; }

        public override void Throw(ITrajectoryFunction trajectoryFunction)
        {
            _trajectoryFunction = trajectoryFunction;
            _throwProcessCoroutine = StartCoroutine(ThrowProcess());
        }

        protected void Start()
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<Collider2D>().isTrigger = false;
        }

        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (_throwProcessCoroutine != null)
            {
                StopCoroutine(_throwProcessCoroutine);
                OnHit(collision);
            }        
        }    

        protected virtual void OnHit(Collision2D collision)
        {
            Debug.Log($"{name} -> Hit and deal {Damage} damage!");
        }

        private IEnumerator ThrowProcess()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                var info = _trajectoryFunction.GetTrajectoryInfoByFunction(transform.position.x);
                transform.position = info.TrajectoryPoint;
                transform.forward = info.TangentDirection;
            }
        }
    }
}
