using UnityEngine;

namespace ArcherNPC_TestTask.Creatures
{

    public class Zombie : MonoBehaviour
    {
        [SerializeField] private Vector2 _speedRange = new Vector2(3f, 7f);
        [SerializeField] private Transform _target;
        [SerializeField] private float _destroyDistance = 1f;

        private float _speed;

        public Transform Target { get => _target; set => _target = value; }

        protected void Start()
        {
            float min = Mathf.Min(_speedRange.x, _speedRange.y);
            float max = Mathf.Max(_speedRange.x, _speedRange.y);
            _speed = Random.Range(min, max);
        }

        protected void Update()
        {
            if (Target == null)
            {
                return;
            }

            if (Vector3.Distance(Target.position, transform.position) < _destroyDistance)
            {
                Destroy(gameObject);
                return;
            }

            transform.Translate((Target.position - transform.position).normalized * Time.deltaTime * _speed);
        }
    }
}
