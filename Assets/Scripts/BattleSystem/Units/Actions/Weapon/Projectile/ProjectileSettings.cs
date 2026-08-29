using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units.Actions.Weapon
{
    public struct ProjectileSettings
    {
        public VelocityType VelocityType;
        public DamageType DamageType;
        public LayerMask AttackTargets;
        public Vector3 ShotDirection;
        public int Damage;
        public float DamageRadius;

        public ProjectileSettings(VelocityType velocityType, DamageType damageType, LayerMask attackTargets, Vector3 shotDirection, int damage, float damageRadius)
        {
            VelocityType = velocityType; 
            DamageType = damageType; 
            AttackTargets = attackTargets;
            ShotDirection = shotDirection;
            Damage = damage; 
            DamageRadius = damageRadius;
        }
    }
}
