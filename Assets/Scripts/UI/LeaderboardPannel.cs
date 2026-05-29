using TMPro;
using UnityEngine;

namespace Battler.UI
{
    public class LeaderboardPannel : PopupMenu 
    {
        [SerializeField] private TextMeshProUGUI _defaultTitle;
        [SerializeField] private TextMeshProUGUI _winGameTitle;

        public void SetDefaultTitle()
        {
            gameObject.SetActive(true);
            _defaultTitle.gameObject.SetActive(true);
            _winGameTitle.gameObject.SetActive(false);
        }

        public void SetWinTitle()
        {
            gameObject.SetActive(true);
            _defaultTitle.gameObject.SetActive(false);
            _winGameTitle.gameObject.SetActive(true);
        }
    }
}
