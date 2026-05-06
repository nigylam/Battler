using Battler.BattleSystem.Armies;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Battler.Core.Phases
{
    public class CombatPhase : IBattlePhase
    {
        public Side RoundWinner { get; private set; }

        public async UniTask ExecuteAsync(Battle battle)
        {
            battle.Player.StartRound();
            battle.Enemy.StartRound();

            await UniTask.WaitUntil(() => battle.Player.IsDead || battle.Enemy.IsDead, cancellationToken: battle.LevelToken);

            if (battle.Player.IsDead)
            {
                RoundWinner = battle.Enemy;
                battle.Menu.OnEnemyWinRound();
            }
            else if (battle.Enemy.IsDead)
            {
                RoundWinner = battle.Player;
                battle.Menu.OnPlayerWinRound();
            }
        }
    }
}
