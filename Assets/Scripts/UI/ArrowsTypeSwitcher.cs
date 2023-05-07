using System;
using System.Collections.Generic;
using System.Linq;
using ArcherNPC_TestTask.Creatures;
using ArcherNPC_TestTask.Weapons;
using UnityEngine;
using UnityEngine.UI;
using OptionData = UnityEngine.UI.Dropdown.OptionData;

namespace ArcherNPC_TestTask.UI
{
    public class ArrowsTypeSwitcher : MonoBehaviour
    {
        [Serializable]
        public class NamedArrow
        {
            public Arrow ArrowPrefab;
            public Sprite Sprite;
            public string ArrowName;
        }

        [SerializeField] private Dropdown _arrowSelector;
        [SerializeField] private HogInstancesManager _hogInstancesManager;
        [SerializeField] private List<NamedArrow> _namedArrows;

        protected void Start()
        {
            _arrowSelector.options = _namedArrows
                .Select(x => new OptionData(x.ArrowName, x.Sprite)).ToList();

            _arrowSelector.onValueChanged.AddListener(OnTypeChanged);
        }

        private void OnTypeChanged(int optionId)
        {
            var newArrow = _namedArrows[optionId].ArrowPrefab;
            foreach (var hog in _hogInstancesManager.Hogs)
            {
                if (hog.Weapon is Bow)
                {
                    Bow bow = (Bow) hog.Weapon;
                    bow.SetArrowPrefab(newArrow);
                }
            }
        }
    }
}
