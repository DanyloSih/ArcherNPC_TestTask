using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArcherNPC_TestTask.Detection
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class TriggerDetector : ObjectsDetector
    {
        private List<GameObject> _allContainedObjectsInTriggerArea = new List<GameObject>();

        protected override List<T> GetDetectedObjects<T>()
        {
            return _allContainedObjectsInTriggerArea
                .Where(x => x is T).Cast<T>().ToList();
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (!_allContainedObjectsInTriggerArea.Contains(other.gameObject))
            {
                _allContainedObjectsInTriggerArea.Add(other.gameObject);
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            if (_allContainedObjectsInTriggerArea.Contains(other.gameObject))
            {
                _allContainedObjectsInTriggerArea.Remove(other.gameObject);
            }
        }
    } 
}