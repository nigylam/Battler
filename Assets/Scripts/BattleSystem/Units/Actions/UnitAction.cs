using UnityEngine;
using System;

namespace Battler.BattleSystem.Units.Actions
{
    public abstract class UnitAction : MonoBehaviour
    {
        [SerializeField] private float _cooldownTime;

        private readonly float _coolDownOffset = 0.6f;

        private Unit _target;

        protected LayerMask TargetLayer;

        public virtual void Initialize(LayerMask targetLayer)
        {
            TargetLayer = targetLayer;
        }

        public virtual void StartAction(Unit target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            _target = target;
        }

        public abstract void StopAction();

        public abstract void Upgrade();

        protected Vector3 GetDirectionToTarget(Vector3 startPosition)
        {
            Vector3 shotDirection = _target.transform.position - startPosition;
            shotDirection.y = 0;
            return shotDirection;
        }

        protected float GetCooldownTime()
        {
            return UnityEngine.Random.Range(_cooldownTime - _coolDownOffset, _cooldownTime + _coolDownOffset);
        }
    }
}
