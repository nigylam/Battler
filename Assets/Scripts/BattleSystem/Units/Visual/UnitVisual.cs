using Battler.BattleSystem.DragAndDrop;
using Battler.BattleSystem.Units;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units.Visual
{
    public abstract class UnitVisual : MonoBehaviour
    {
        private readonly int AnimatorWin = Animator.StringToHash("Win");
        private readonly int AnimatorUpgrade = Animator.StringToHash("Upgrade");
        private readonly int AnimatorHit = Animator.StringToHash("Hit");
        private readonly int AnimatorDeath = Animator.StringToHash("Death");
        private readonly int AnimatorIsMoving = Animator.StringToHash("IsMoving");
        private readonly string AnimatorHitLayer = "HitLayer";

        [SerializeField] private GameObject _body;
        [SerializeField] private UnitDragger _dragger;
        [SerializeField] private List<SkinnedMeshRenderer> _skinnedRenderers;
        [SerializeField] private List<MeshRenderer> _meshRenderers;
        [SerializeField] private SkinnedMeshRenderer _armyColorPart;
        [SerializeField] private ParticleSystem _upgradeEffect;
        [SerializeField] private Animator _animator;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private EffectSpawner _hitEffects;
        [SerializeField] private ParticleSystem _healEffect;

        private int _hitLayer;

        public UnitDragger Dragger => _dragger;
        public Transform Body => _body.transform;

        private void Awake()
        {
            if (_animator != null)
                _hitLayer = _animator.GetLayerIndex(AnimatorHitLayer);
        }

        public void Hide()
        {
            if (_skinnedRenderers.Count > 0)
                foreach (SkinnedMeshRenderer renderer in _skinnedRenderers)
                    renderer.enabled = false;

            if (_meshRenderers.Count > 0)
                foreach (MeshRenderer renderer in _meshRenderers)
                    renderer.enabled = false;

            _healthBar.gameObject.SetActive(false);
        }

        public void Show()
        {
            foreach (SkinnedMeshRenderer renderer in _skinnedRenderers)
                renderer.enabled = true;

            foreach (MeshRenderer renderer in _meshRenderers)
                renderer.enabled = true;

            _healthBar.gameObject.SetActive(true);
        }

        public void SetArmyMaterial(Material armyMaterial)
        {
            if (_armyColorPart != null)
                _armyColorPart.material = armyMaterial;
        }

        public void PlayMoveAnimation(bool isMoving)
        {
            SetBool(AnimatorIsMoving, isMoving);
        }

        public virtual void PlayDeathAnimation()
        {
            if (_animator != null)
            {
                _animator.SetLayerWeight(_hitLayer, 0);
                _animator.SetTrigger(AnimatorDeath);
            }
        }

        public void OnHit(Vector3 hitPoint)
        {
            SetTrigger(AnimatorHit);
            _hitEffects.Spawn(hitPoint);
        }

        public void PlayUpgrade()
        {
            SetTrigger(AnimatorUpgrade);
        }

        public void PlayWin()
        {
            SetTrigger(AnimatorWin);
        }

        public void Upgrade()
        {
            _healthBar.Upgrade();
        }

        public void OnDeathAnimationPlayed()
        {
            gameObject.SetActive(false);
        }

        public void OnHeal()
        {
            _healEffect.gameObject.SetActive(false);
            _healEffect.gameObject.SetActive(true);
        }

        protected void SetMaterial(Material material)
        {
            Material[] materials = new Material[] {material};

            foreach (SkinnedMeshRenderer renderer in _skinnedRenderers)
                renderer.materials = materials;

            foreach (MeshRenderer renderer in _meshRenderers)
                renderer.materials = materials;
        }

        protected void SetBool(int id, bool value)
        {
            if (_animator != null)
                _animator.SetBool(id, value);
        }

        protected void SetTrigger(int id)
        {
            if (_animator != null)
                _animator.SetTrigger(id);
        }
    }
}
