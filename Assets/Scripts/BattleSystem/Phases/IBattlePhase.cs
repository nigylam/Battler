using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Battler.Core.Phases
{
    public interface IBattlePhase
    {
        UniTask ExecuteAsync(Battle battleContext);
    }
}
