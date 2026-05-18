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

        [SerializeField] private UnitDragger _dragger;
        [SerializeField] private List<SkinnedMeshRenderer> _renderers;
        [SerializeField] private GameObject _weapon;
        [SerializeField] private SkinnedMeshRenderer _armyColorPart;
        [SerializeField] private ParticleSystem _upgradeEffect;
        [SerializeField] private Animator _animator;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private EffectSpawner _hitEffects;
        [SerializeField] private ParticleSystem _healEffect;

        private int _hitLayer;

        public UnitDragger Dragger => _dragger;

        private void Awake()
        {
            _hitLayer = _animator.GetLayerIndex(AnimatorHitLayer);
        }

        public void Hide()
        {
            foreach (SkinnedMeshRenderer renderer in _renderers)
                renderer.enabled = false;

            _weapon.SetActive(false);
            _healthBar.gameObject.SetActive(false);
        }

        public void Show()
        {
            foreach (SkinnedMeshRenderer renderer in _renderers)
                renderer.enabled = true;

            _weapon.SetActive(true);
            _healthBar.gameObject.SetActive(true);
        }

        public void SetMaterial(Material armyMaterial)
        {
            _armyColorPart.material = armyMaterial;
        }

        public void PlayMoveAnimation(bool isMoving)
        {
            SetBool(AnimatorIsMoving, isMoving);
        }

        public void PlayDeathAnimation()
        {
            _animator.SetLayerWeight(_hitLayer, 0);
            _animator.SetTrigger(AnimatorDeath);
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

        protected void SetBool(int id, bool value)
        {
            _animator.SetBool(id, value);
        }

        protected void SetTrigger(int id)
        {
            _animator.SetTrigger(id);
        }
    }
}
