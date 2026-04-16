using TMPro;
using UnityEngine;

namespace Battler.UI
{
    public class LeaderboardPannel : PopupMenu 
    {
        private readonly string LeaderBoardTitle = "Leaderboard";
        private readonly string WinGameTitle = "Congratulations, you win the game! Check your scrores";

        [SerializeField] private TextMeshProUGUI _title;

        public void SetDefaultTitle()
        {
            gameObject.SetActive(true);
            _title.text = LeaderBoardTitle;
        }

        public void SetWinTitle()
        {
            gameObject.SetActive(true);
            _title.text = WinGameTitle;
        }
    }
}
