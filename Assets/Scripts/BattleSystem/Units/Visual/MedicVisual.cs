using System.Collections;
using UnityEngine;

namespace Battler.BattleSystem.Units.Visual
{
    public class MedicVisual : UnitVisual
    {
        [SerializeField] private ParticleSystem _healEffect;

        private readonly int AnimatorHeal = Animator.StringToHash("Heal");
        private readonly float _healEffectDuration = 2f;

        private Coroutine _disableHealEffect;

        private void OnDisable()
        {
            StopDisableCoroutine();
        }

        public void OnHeal()
        {
            SetTrigger(AnimatorHeal);
            _healEffect.gameObject.SetActive(true);
            StopDisableCoroutine();
            _disableHealEffect = StartCoroutine(DisableHealEffect());
        }

        private void StopDisableCoroutine()
        {
            if (_disableHealEffect != null)
                StopCoroutine(_disableHealEffect);
        }

        private IEnumerator DisableHealEffect()
        {
            float time = 0;

            while (time < _healEffectDuration)
            {
                time += Time.deltaTime;
                yield return null;
            }

            _healEffect.gameObject.SetActive(false);
        }
    }
}
