using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArcherNPC_TestTask.Detection
{
    [RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
    public class TriggerDetector : ObjectsDetector
    {
        private List<GameObject> _allContainedObjectsInTriggerArea = new List<GameObject>();

        protected void Start()
        {
            GetComponent<Collider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }

        protected override List<T> GetDetectedObjects<T>()
        {
            List<T> result = new List<T>();

            foreach (var item in _allContainedObjectsInTriggerArea)
            {
                if (item.TryGetComponent<T>(out var component))
                {
                    result.Add(component);
                }
            }

            return result;
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (!_allContainedObjectsInTriggerArea.Contains(other.gameObject))
            {
                _allContainedObjectsInTriggerArea.Add(other.gameObject);
            }
        }

        protected void OnTriggerExit2D(Collider2D other)
        {
            if (_allContainedObjectsInTriggerArea.Contains(other.gameObject))
            {
                _allContainedObjectsInTriggerArea.Remove(other.gameObject);
            }
        }
    } 
}