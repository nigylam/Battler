using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Battler.BattleSystem.Armies;

namespace Battler.Core.Phases
{
    public class RoundEndPhase : IBattlePhase
    {
        private const float WinnerPanelShowDuration = 1;
        private const float LoserDisablingDuration = 2;

        public async UniTask ExecuteAsync(Battle battle)
        {
            if (battle == null)
                throw new ArgumentNullException(nameof(battle));

            if (battle.RoundWinner == null)
                throw new InvalidOperationException();

            battle.RoundWinner.Stop();
            await UniTask.WaitForSeconds(LoserDisablingDuration, cancellationToken: battle.LevelToken);
            await UniTask.WaitForSeconds(WinnerPanelShowDuration, cancellationToken: battle.LevelToken);

            if (battle.HaveLevelWinner == false)
            {
                PlayRoundEndSounds(battle);
                await PlayWinnerAnimation(battle.RoundWinner);
                battle.Player.ClearField();
                battle.Enemy.ClearField();
                await ShowWinnerPanel(battle);
            }
        }

        private void PlayRoundEndSounds(Battle battle)
        {
            if (battle.RoundWinner == battle.Enemy)
                battle.Sound.PlayLoseRoundSound();
            else
                battle.Sound.PlayWinRoundSound();
        }

        private async UniTask PlayWinnerAnimation(Side winner)
        {
            await winner.PlayWin();
        }

        private async UniTask ShowWinnerPanel(Battle battle)
        {
            if (battle.RoundWinner == battle.Enemy)
                battle.Menu.SetEnemyWinPanel();
            else
                battle.Menu.SetPlayerWinPanel();

            await UniTask.WaitForSeconds(WinnerPanelShowDuration, cancellationToken: battle.LevelToken);
        }
    }
}
