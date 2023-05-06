using System.Collections.Generic;
using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    public class WeaponsProvider : MonoBehaviour
    {
        [SerializeField] private List<Weapon> _weapons = new List<Weapon>();

        public List<Weapon> GetWeapons()
        {
            return _weapons;
        }

        [ContextMenu("AutoFillWeaponsList")]
        private void AutoFillWeaponsList()
        {
            _weapons = new List<Weapon>();
            _weapons.AddRange(GetComponentsInChildren<Weapon>(true));
        }
    }
}
