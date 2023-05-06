using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

namespace ArcherNPC_TestTask.Weapons
{
    public class Bow : Weapon
    {
        [SerializeField] private Arrow _arrowPrefab;
        [Header("Animation")]
        [SerializeField] private Animator _animator;
        [SerializeField] private string _isHaveArrowsParameterName = "isHaveArrows";
        [Header("Bow view")]
        [SerializeField] private SpriteRenderer _arrowQuiver;
        [SerializeField] private SpriteRenderer _arrowsBelt;
        [SerializeField] private SpriteRenderer _bow;
        [Header("Arrow stash view")]
        [SerializeField] private SpriteRenderer _arrowsStashes;
        [SerializeField] private SpriteResolver _arrowsStashesSpriteResolver;
        [SerializeField] private string _arrowsStashesCategory = "Arrows_Stashes";

        private int? _isHaveArrowsParameterHash = null;

        public Arrow ArrowPrefab { get => _arrowPrefab; }
        private int IsHaveArrowsParameterHash
        {
            get
            {
                _isHaveArrowsParameterHash
                    = _isHaveArrowsParameterHash ?? Animator.StringToHash(_isHaveArrowsParameterName);
                return (int)_isHaveArrowsParameterHash;
            }
        }

        /// <summary>
        /// If the object is activated, it will set the arrow prefab instantly. 
        /// If the object is deactivated, it will add the arrow prefab to the 
        /// installation queue, each new installation request
        /// will DELETE the previous one.
        /// </summary>
        public void SetArrowPrefabLazy(Arrow arrowPrefab)
        {
            if (IsActivated)
            {
                SetArrowPrefab(arrowPrefab);
            }
            else
            {
                _arrowPrefab = arrowPrefab;
            }
        }

        public void SetArrowPrefab(Arrow arrowPrefab)
        {
            CheckIsActivated();
            _arrowsStashes.enabled = arrowPrefab != null;
            _animator.SetBool(IsHaveArrowsParameterHash, arrowPrefab != null);
            _arrowPrefab = arrowPrefab;
            if (arrowPrefab != null)
            {
                _arrowsStashesSpriteResolver.SetCategoryAndLabel(
                    _arrowsStashesCategory, _arrowPrefab.ArrowsStashLabelName);
            }
            Debug.Log(_arrowsStashes.enabled);
        }

        protected override void OnActivate()
        {
            _arrowsBelt.enabled = true;
            _arrowQuiver.enabled = true;
            _bow.enabled = true;
            SetArrowPrefab(_arrowPrefab);
        }

        protected override void OnDeactivate()
        {
            _arrowsBelt.enabled = false;
            _arrowQuiver.enabled = false;
            _arrowsStashes.enabled = false;
            _bow.enabled = false; 
        }
    }
}
