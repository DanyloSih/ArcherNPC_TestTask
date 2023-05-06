﻿using System.Collections.Generic;
using ArcherNPC_TestTask.Creatures;
using UnityEngine;

namespace ArcherNPC_TestTask.Leveling
{
    public class MainLevelBootstrap : MonoBehaviour
    {
        [SerializeField] private List<Transform> _hogSpawnPoints = new List<Transform>();
        [SerializeField] private HogFactory _hogFactory;
        [SerializeField] private string _initialWeaponName = "Bow";

        protected void Start()
        {
            foreach (var hogSpawnPoint in _hogSpawnPoints)
            {
                var hogInstance = _hogFactory.Create(_initialWeaponName);
                hogInstance.transform.position = hogSpawnPoint.position;
            }
        }
    }
}
