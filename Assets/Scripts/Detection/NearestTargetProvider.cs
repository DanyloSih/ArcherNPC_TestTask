using System.Collections.Generic;
using ArcherNPC_TestTask.Creatures;
using UnityEngine;

namespace ArcherNPC_TestTask.Detection
{
    public class NearestTargetProvider : MonoBehaviour
    {
        private ObjectsDetector _objectsDetector;
        private Hog _hog;
        private Transform _target;

        public void Construct(ObjectsDetector objectsDetector, Hog hog)
        {
            _objectsDetector = objectsDetector;
            _hog = hog;
        }

        public bool TryGetTarget(out Transform target)
        {
            if (_objectsDetector.TryDetectObjects<Zombie>(out var targets))
            {
                target = GetNearestTarget(targets);
                return true;
            }
            else
            {
                target = null;
                return false;
            }
        }

        protected void Update()
        {
            TryGetTarget(out var target);
            if (_target != target)
            {
                _hog.SetTarget(target);
            }
            _target = target;
        }

        private Transform GetNearestTarget(List<Zombie> targets)
        {
            Transform target;
            Transform resultTarget = targets[0].transform;
            float minDistance = Vector2.Distance(resultTarget.position, transform.position);
            for (int i = 1; i < targets.Count; i++)
            {
                Transform tmpTarget = targets[i].transform;
                if (Vector2.Distance(tmpTarget.position, transform.position) < minDistance)
                {
                    resultTarget = tmpTarget;
                }
            }
            target = resultTarget;
            return target;
        }
    }
}