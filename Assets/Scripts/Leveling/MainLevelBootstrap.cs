using System.Collections.Generic;
using ArcherNPC_TestTask.Creatures;
using UnityEngine;

namespace ArcherNPC_TestTask.Leveling
{
    public class MainLevelBootstrap : MonoBehaviour
    {
        [SerializeField] private List<Transform> _hogSpawnPoints = new List<Transform>();
        [SerializeField] private HogInstancesManager _hogInstancesManager;
        [SerializeField] private string _initialWeaponName = "none";

        protected void Start()
        {
            if (!_initialWeaponName.Equals("none"))
            {
                foreach (var hogSpawnPoint in _hogSpawnPoints)
                {
                    _hogInstancesManager.CreateNew(hogSpawnPoint.position, _initialWeaponName);
                }
            }
        }
    }
}
