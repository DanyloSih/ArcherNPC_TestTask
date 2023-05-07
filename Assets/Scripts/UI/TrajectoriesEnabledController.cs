using ArcherNPC_TestTask.Weapons;
using UnityEngine;
using UnityEngine.UI;

namespace ArcherNPC_TestTask.UI
{
    public class TrajectoriesEnabledController : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;

        private TrajectoryDrawer[] _drawers = null;

        private TrajectoryDrawer[] Drawers 
        {
            get
            {
                _drawers = _drawers ?? GameObject.FindObjectsOfType<TrajectoryDrawer>();
                return _drawers;
            }
        }

        protected void Start()
        {
            _toggle.isOn = true;
            _toggle.onValueChanged.AddListener(OnToggle);
        }

        private void OnToggle(bool value)
        {
            foreach (var trajectoryDrawer in Drawers)
            {
                trajectoryDrawer.enabled = value;
            }
        }
    }
}
