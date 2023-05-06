using UnityEngine;
using UnityEngine.Events;

namespace ArcherNPC_TestTask.Common
{
    public class SelfDestroyTimer : MonoBehaviour
    {
        [Min(0)]
        [SerializeField] private float _destroyTime = 1f;

        public UnityEvent Destroyed;

        private float _time = 0;

        protected void Update()
        {
            _time += Time.deltaTime;
            if (_time > _destroyTime)
            {
                Destroyed?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
