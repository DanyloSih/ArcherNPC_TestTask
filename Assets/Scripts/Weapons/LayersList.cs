using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArcherNPC_TestTask.Weapons
{
    [CreateAssetMenu(fileName = "LayersList", menuName = "Physics/LayersList")]
    public class LayersList : ScriptableObject
    {
        [SerializeField] private List<string> _layers = new List<string>();

        public List<int> GetLayers()
            => _layers.Select(x => LayerMask.NameToLayer(x)).ToList();
    }
}
