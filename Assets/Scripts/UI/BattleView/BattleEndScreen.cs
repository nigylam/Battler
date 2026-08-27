using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Battler.UI.BattleView
{
    public class BattleEndScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleLose;
        [SerializeField] private TextMeshProUGUI _titleWin;
        [SerializeField] private GameObject _titleAllDead;
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

        public void Set(bool isPlayerWin, bool isAutoLose, int goldReward, SquadGoodConfig squadReward = null)
        {
            gameObject.SetActive(true);

            if (isPlayerWin == false)
            {
                SetLose(isAutoLose);
                return;
            }

            SetWin(goldReward, squadReward);
        }

        private void SetLose(bool isAutoLose)
        {
            _rewardBlock.SetActive(false);
            _titleLose.gameObject.SetActive(true);
            _titleWin.gameObject.SetActive(false);
            _titleAllDead.SetActive(isAutoLose);
        }

        private void SetWin(int goldReward, SquadGoodConfig squadReward = null)
        {
            if (goldReward <= 0)
                throw new ArgumentOutOfRangeException(nameof(goldReward));

            _rewardBlock.SetActive(true);
            _titleLose.gameObject.SetActive(false);
            _titleAllDead.SetActive(false);
            _titleWin.gameObject.SetActive(true);
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
            YG2.InterstitialAdvShow();
        }
    }
}