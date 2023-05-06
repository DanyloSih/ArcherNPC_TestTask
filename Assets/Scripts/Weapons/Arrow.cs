using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    public abstract class Arrow : MonoBehaviour
    {
        [SerializeField] private float _damage = 12f;
        [Tooltip("Meters per second")]
        [SerializeField] private float _initialVelocity = 2f;
        [SerializeField] private string _arrowsStashesLabelName = "Simple_Arrow_Stash";

        public abstract void Throw(ITrajectoryFunction trajectoryFunction);

        public float Damage { get => _damage; }
        public string ArrowsStashLabelName { get => _arrowsStashesLabelName; }
        public float InitialVelocity { get => _initialVelocity; }
    }
}
