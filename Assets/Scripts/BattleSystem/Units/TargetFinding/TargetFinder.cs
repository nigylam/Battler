using Battler.BattleSystem.Units;
using System.Collections.Generic;
using UnityEngine;

namespace Battler.BattleSystem.Units.TargetFinding
{
    public abstract class TargetFinder : MonoBehaviour
    {
        public abstract Unit GetTarget(List<Unit> enemies, List<Unit> alies);
    }
}