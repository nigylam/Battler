using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Battler.UI.BattleView
{
    public class BattleEndScreen : MonoBehaviour
    {
        private readonly string YouLoseText = "You lose";
        private readonly string YouWinText = "You win";

        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private GameObject _rewardBlock;
        [SerializeField] private GameObject _squadRewardBlock;
        [SerializeField] private Image _squadReward;
        [SerializeField] private TextMeshProUGUI _goldReward;
        [SerializeField] private UIButton _endButton;

        public event Action End;

        private void OnEnable()
        {
            _endButton.Clicked += OnClick;
        }

        private void OnDisable()
        {
            _endButton.Clicked -= OnClick;
        }

        public void Set(bool isPlayerWin, int goldReward, SquadGoodConfig squadReward = null)
        {
            gameObject.SetActive(true);

            if (isPlayerWin == false)
            {
                SetLoseText();
                return;
            }

            SetWinText(goldReward, squadReward);
        }

        private void SetLoseText()
        {
            _rewardBlock.SetActive(false);
            _title.text = YouLoseText;
        }

        private void SetWinText(int goldReward, SquadGoodConfig squadReward = null)
        {
            if (goldReward <= 0)
                throw new ArgumentOutOfRangeException(nameof(goldReward));

            _rewardBlock.SetActive(true);
            _title.text = YouWinText;
            _goldReward.text = goldReward.ToString();

            if (squadReward == null)
                _squadRewardBlock.SetActive(false);
            else
                SetSquadReward(squadReward.Squad.UiIcon);
        }

        public void SetSquadReward(Sprite squadIcon)
        {
            _squadRewardBlock.SetActive(true);
            _squadReward.sprite = squadIcon;
        }

        private void OnClick()
        {
            _endButton.Clicked -= OnClick;
            End?.Invoke();
        }
    }
}