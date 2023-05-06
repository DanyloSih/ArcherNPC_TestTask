using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArcherNPC_TestTask.Detection
{
    public abstract class ObjectsDetector : MonoBehaviour
    {
        private Dictionary<Type, object> _detectedObjectsCash = new Dictionary<Type, object>();

        public bool TryDetectObjects<T>(out List<T> detectedObjects)
             where T : MonoBehaviour
        {
            detectedObjects = GetDetectedObjectsCashOrCalculateNew<T>();
            return detectedObjects.Count != 0;
        }

        protected abstract List<T> GetDetectedObjects<T>();

        protected void LateUpdate()
        {
            _detectedObjectsCash.Clear();
        }

        private List<T> GetDetectedObjectsCashOrCalculateNew<T>()
        {
            if (_detectedObjectsCash.TryGetValue(typeof(T), out object value))
            {
                return (List<T>)value;
            }
            else
            {
                var result = GetDetectedObjects<T>();
                _detectedObjectsCash.Add(typeof(T), result);
                return result;
            }
        }
    }
}