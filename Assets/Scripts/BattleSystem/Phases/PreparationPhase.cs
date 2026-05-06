using Battler.BattleSystem.Armies;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Battler.Core.Phases
{
    public class PreparationPhase : IBattlePhase
    {
        public async UniTask ExecuteAsync(Battle battle)
        {
            await battle.Enemy.SpawnSquads();
            await battle.Player.SpawnSquads();
            await WaitPlayerDeployment(battle.Player, battle.LevelToken);
        }

        private async UniTask WaitPlayerDeployment(PlayerSide player, CancellationToken token)
        {
            player.EnablePlacing();
            bool playerReady = false;
            void OnPlayerReady() => playerReady = true;
            player.ReadyForRound += OnPlayerReady;

            try
            {
                await UniTask.WaitUntil(() => playerReady, cancellationToken: token);
            }
            finally
            {
                player.ReadyForRound -= OnPlayerReady;
            }
        }
    }
}
