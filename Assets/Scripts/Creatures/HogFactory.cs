using System;
using System.Linq;
using ArcherNPC_TestTask.Detection;
using UnityEngine;

namespace ArcherNPC_TestTask.Creatures
{
    public class HogFactory : MonoBehaviour
    {
        [SerializeField] private ObjectsDetector _objectsDetector;
        [SerializeField] private Hog _hogPrefab;

        public Hog Create(string initialWeaponName = null)
        {
            var hogInstance = Instantiate(_hogPrefab);
            var targetProvider = hogInstance.gameObject.AddComponent<NearestTargetProvider>();
            targetProvider.Construct(_objectsDetector, hogInstance);
            if (initialWeaponName == null)
            {
                return hogInstance;
            }

            try
            {
                var weapon = hogInstance.GetAvailableWeapons()
                    .First(x => x.WeaponName.Equals(initialWeaponName));

                hogInstance.SetWeapon(weapon);
                return hogInstance;
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException($"The {nameof(Hog)} does not " +
                    $"have an available weapon named {initialWeaponName}!");
            }
        }
    }
}
