using ArcherNPC_TestTask.Creatures;
using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    public class HealthRemovalArrow : RaycastArrow
    {
        protected override bool IsLastHit(RaycastHit2D hit)
        {
            Health healthComponent = hit.collider.GetComponent<Health>();
            
            if (healthComponent != null)
            {
                if (healthComponent.IsAlive)
                {
                    healthComponent.MakeDamage(Damage);
                }

                transform.parent = hit.collider.transform;

                return !base.IsLastHit(hit);
            }

            return false;
        }
    }
}
