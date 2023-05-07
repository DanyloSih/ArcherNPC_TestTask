using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArcherNPC_TestTask.Creatures
{
    public class HogInstancesManager : MonoBehaviour
    {
        [SerializeField] private HogFactory _hogFactory;

        private List<Hog> _hogInstances = new List<Hog>();

        public IEnumerable<Hog> Hogs 
        {
            get
            {
                for (int i = 0; i < _hogInstances.Count; i++)
                {
                    if (_hogInstances[i] == null)
                    {
                        _hogInstances.RemoveAt(i);
                    }
                }

                return _hogInstances;
            }
        }

        public Hog CreateNew(Vector3 spawnPoint = new Vector3(), string weaponName = null)
        {
            var instance = _hogFactory.Create(weaponName);
            instance.transform.position = spawnPoint;
            _hogInstances.Add(instance);
            return instance;
        }

        public void DestroyHog(Hog hog)
        {
            if (_hogInstances.Contains(hog))
            {
                _hogInstances.Remove(hog);
                Destroy(hog.gameObject);
            }
            else
            {
                throw new ArgumentException($"{nameof(HogInstancesManager)} did not create " +
                    "the passed Hog instance, so it cannot destroy it.");
            }
        }
    }
}
